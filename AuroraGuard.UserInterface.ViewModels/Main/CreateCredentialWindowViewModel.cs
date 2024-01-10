using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Models;

namespace AuroraGuard.UserInterface.ViewModels.Main;

public class CreateCredentialWindowViewModel : ViewModel
{
    private readonly ICredentialRepository _credentialRepository;

    public CreateCredentialWindowViewModel(ICredentialRepository credentialRepository)
    {
        _credentialRepository = credentialRepository;
        CreateCredentialCommand = new AsyncRelayCommand(CreateCredential, CanExecuteCreateCredential);
    }

    public Credential? CreatedCredential { get; private set; }

    private string? _username;
    public string Username
    {
        get => _username ?? "";
        set
        {
            if (SetField(ref _username, value))
                CreateCredentialCommand.OnCanExecuteChanged();
        }
    }

    private string? _password;
    public string Password
    {
        get => _password ?? "";
        set
        {
            if (SetField(ref _password, value))
                CreateCredentialCommand.OnCanExecuteChanged();
        }
    }

    private string? _appName;
    public string AppName
    {
        get => _appName ?? "";
        set
        {
            if (SetField(ref _appName, value))
                CreateCredentialCommand.OnCanExecuteChanged();
        }
    }

    private string? _imagePath;
    public string? ImagePath
    {
        get => _imagePath;
        set => SetField(ref _imagePath, value);
    }

    private string? _notes;
    public string? Notes
    {
        get => _notes;
        set => SetField(ref _notes, value);
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

    private bool CanExecuteCreateCredential(object? parameter)
    {
        return Username.Length > 0 && AppName.Length > 0 && Password.Length > 0;
    }
}