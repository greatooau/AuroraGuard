using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Models;

namespace AuroraGuard.UserInterface.ViewModels.Main;

public class CreateEditCredentialWindowViewModel : ViewModel
{
    private readonly ICredentialRepository _credentialRepository;

    public CreateEditCredentialWindowViewModel(ICredentialRepository credentialRepository)
    {
        _credentialRepository = credentialRepository;
        CreateCredentialCommand = new AsyncRelayCommand(CreateCredential, CanExecuteCreateCredential);
        EditCredentialCommand = new AsyncRelayCommand(EditCredential, CanExecuteEditCredential);
    }

    public Credential? CreatedCredential { get; private set; }
    public Credential? OriginalCredential { get; init; }

    private bool _isEdition;
    public bool IsEdition
    {
        get => _isEdition;
        set => SetField(ref _isEdition, value);
    }

    private Guid _id;
    public Guid Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    private string? _username;
    public string Username
    {
        get => _username ?? "";
        set
        {
            if (!SetField(ref _username, value)) return;

            if (IsEdition) EditCredentialCommand.OnCanExecuteChanged();
            else CreateCredentialCommand.OnCanExecuteChanged();
        }
    }

    private string? _password;
    public string Password
    {
        get => _password ?? "";
        set
        {
            if (!SetField(ref _password, value)) return;
            
            if (IsEdition) EditCredentialCommand.OnCanExecuteChanged();
            else CreateCredentialCommand.OnCanExecuteChanged();
        }
    }

    private string? _appName;
    public string AppName
    {
        get => _appName ?? "";
        set
        {
            if (!SetField(ref _appName, value)) return;

            if (IsEdition) EditCredentialCommand.OnCanExecuteChanged();
            else CreateCredentialCommand.OnCanExecuteChanged();
        }
    }

    private string? _imagePath;
    public string? ImagePath
    {
        get => _imagePath;
        set
        {
            if (!SetField(ref _imagePath, value)) return;

            if (IsEdition) EditCredentialCommand.OnCanExecuteChanged();
        }
    }

    private string? _notes;
    public string? Notes
    {
        get => _notes;
        set
        {
            if (!SetField(ref _notes, value)) return;

            if (IsEdition) EditCredentialCommand.OnCanExecuteChanged();
        }
    }

    public AsyncRelayCommand CreateCredentialCommand { get; }
    private async Task CreateCredential(object? parameter, CancellationToken token)
    {
        var dto = new CreateCredentialDto
        {
            Id = Guid.NewGuid(),
            AccessPassword = Password,
            AccessUser = Username,
            AppName = AppName,
            ImagePath = ImagePath,
            Notes = Notes
        };

        CreatedCredential = await _credentialRepository.Create(dto);
        
        var window = (IClosableWindow)parameter!;
        window.Close();
    }

    public AsyncRelayCommand EditCredentialCommand { get; }
    private async Task EditCredential(object? parameter, CancellationToken token)
    {
        var dto = new UpdateCredentialDto()
        {
            AccessPassword = Password,
            AccessUser = Username,
            AppName = AppName,
            ImagePath = ImagePath,
            Notes = Notes
        };

        await _credentialRepository.Update(Id, dto);

        var window = (IClosableWindow)parameter!;
        window.Close();
    }

    private bool CanExecuteCreateCredential(object? parameter)
    {
        return Username.Length > 0 && AppName.Length > 0 && Password.Length > 0;
    }

    private bool CanExecuteEditCredential(object? parameter)
    {
        if (OriginalCredential is null) throw new Exception("Original credentials shouldn't be null");
        var editionCounter = 0;

        if (Notes != OriginalCredential.Notes) editionCounter++;
        if (AppName != OriginalCredential.AppName) editionCounter++;
        if (ImagePath != OriginalCredential.ImagePath) editionCounter++;
        if (Username != OriginalCredential.AccessUser) editionCounter++;
        if (Password != OriginalCredential.AccessPassword) editionCounter++;

        return editionCounter != 0;
    }
}