using System.Windows.Input;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.Core.Models;

namespace AuroraGuard.UserInterface.ViewModels.Main;

public class CreateEditCredentialWindowViewModel : ViewModel
{
    private readonly ICredentialRepository _credentialRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly IDialogService _dialogService;
    private readonly IAppService _appService;

    public CreateEditCredentialWindowViewModel(ICredentialRepository credentialRepository,
        IEncryptionService encryptionService,
        IDialogService dialogService,
        IFileService fileService, 
        IAppService appService)
    {
        _credentialRepository = credentialRepository;
        _encryptionService = encryptionService;
        _dialogService = dialogService;
        _appService = appService;

        CreateCredentialCommand = new AsyncRelayCommand(CreateCredential, CanExecuteCreateCredential);
        EditCredentialCommand = new AsyncRelayCommand(EditCredential, CanExecuteEditCredential);
        SelectImageCommand = new RelayCommand(SelectImage);
        ClearImagePathCommand = new RelayCommand(ClearImagePath, CanExecuteClearImagePath);
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

            OnPropertyChanged(nameof(FileName));
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

    public string? FileName => ImagePath is null ? null : new FileInfo(ImagePath).Name;

    #region CreateCredentialCommand

    public AsyncRelayCommand CreateCredentialCommand { get; }
    private async Task CreateCredential(object? parameter, CancellationToken token)
    {
        var newImagePath = HandleImageCopying(ImagePath);

        var dto = new CreateCredentialDto
        {
            Id = Guid.NewGuid(),
            AccessPassword = _encryptionService.EncryptText(Password, _encryptionService.GetStoredKey()),
            AccessUser = Username,
            AppName = AppName,
            ImagePath = newImagePath,
            Notes = Notes
        };

        CreatedCredential = await _credentialRepository.Create(dto);

        var window = (IClosableWindow)parameter!;
        window.Close();
    }

    private bool CanExecuteCreateCredential(object? parameter) => Username.Length > 0 && AppName.Length > 0 && Password.Length > 0;

    #endregion

    #region EditCredentialCommand

    public AsyncRelayCommand EditCredentialCommand { get; }
    private async Task EditCredential(object? parameter, CancellationToken token)
    {
        var newImagePath = HandleImageCopying(ImagePath);

        var dto = new UpdateCredentialDto
        {
            AccessPassword = _encryptionService.EncryptText(Password, _encryptionService.GetStoredKey()),
            AccessUser = Username,
            AppName = AppName,
            ImagePath = newImagePath,
            Notes = Notes
        };

        await _credentialRepository.Update(Id, dto);
        
        if (newImagePath is null && OriginalCredential?.ImagePath is not null) 
            File.Delete(OriginalCredential.ImagePath);

        var window = (IClosableWindow)parameter!;
        window.Close();
    }

    private bool CanExecuteEditCredential(object? parameter)
    {
        if (OriginalCredential is null) throw new Exception("Original credentials shouldn't be null");
        var editionCounter = 0;

        if (Notes != OriginalCredential.Notes) editionCounter++;
        if (AppName != OriginalCredential.AppName) editionCounter++;
        if (ImagePath != OriginalCredential.ImagePath) editionCounter++;
        if (Username != OriginalCredential.AccessUser) editionCounter++;
        
        var password = new byte[OriginalCredential.AccessPassword.Length - 16];
        var iv = new byte[16];
        
        Buffer.BlockCopy(OriginalCredential.AccessPassword, 0, iv, 0, 16);
        Buffer.BlockCopy(OriginalCredential.AccessPassword, 16, password, 0, password.Length);

        if (Password != _encryptionService.DecryptText(password, _encryptionService.GetStoredKey(), iv)) editionCounter++;

        return editionCounter != 0;
    }

    #endregion

    #region SelectImageCommand

    public ICommand SelectImageCommand { get; }

    private void SelectImage(object? parameter)
    {
        var path = _dialogService.SelectSingleImage();

        if (path is null) return;

        ImagePath = path;
        OnPropertyChanged(FileName);
        ClearImagePathCommand.OnCanExecuteChanged();
    }

    #endregion

    #region ClearImagePathCommand

    public RelayCommand ClearImagePathCommand { get; set; }
    private void ClearImagePath(object? o)
    {
        ImagePath = null;

        OnPropertyChanged(ImagePath);
        OnPropertyChanged(FileName);
        ClearImagePathCommand.OnCanExecuteChanged();
    }
    private bool CanExecuteClearImagePath(object? arg) => ImagePath is not null;

    #endregion

    private string? HandleImageCopying(string? originalPath)
    {
        if (originalPath is null) return null;

        var fileInfo = new FileInfo(originalPath);

        var newPath = Path.Combine(_appService.GetAppImagesPath(), fileInfo.Name);

        if (newPath == originalPath) return originalPath;

        File.Copy(originalPath, newPath, true);

        return newPath;
    }
}