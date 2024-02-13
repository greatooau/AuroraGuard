using System.Windows.Input;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Interfaces.Services;
using System.Linq;
using AuroraGuard.Core.Enum;
using AuroraGuard.UserInterface.ViewModels.EventArgsTypes;

namespace AuroraGuard.UserInterface.ViewModels.Main;

public class DisplayedCredentialViewModel : ViewModel
{
    private readonly ICredentialRepository _credentialRepository;
    private readonly IDialogService _dialogService;
    private readonly IEncryptionService _encryptionService;
    private readonly IAppService _appService;
    private readonly IFileService _fileService;
    private readonly IClipboardService _clipboardService;

    public DisplayedCredentialViewModel(ICredentialRepository credentialRepository, IDialogService dialogService,
        IEncryptionService encryptionService, IAppService appService, IFileService fileService, IClipboardService clipboardService)
    {
        _credentialRepository = credentialRepository;
        _dialogService = dialogService;
        _encryptionService = encryptionService;
        _appService = appService;
        _fileService = fileService;
        _clipboardService = clipboardService;
        DeleteCommand = new AsyncRelayCommand(Delete);
        EditCredentialCommand = new AsyncRelayCommand(EditCredential);
        ShowPasswordCommand = new RelayCommand(_ => IsPasswordVisible = !IsPasswordVisible);
        CopyToClipboardCommand = new RelayCommand(CopyToClipboard);
    }

    private string? _appName;
    public string? AppName
    {
        get => _appName;
        set => SetField(ref _appName, value);
    }

    private string? _username;
    public string? Username
    {
        get => _username;
        set => SetField(ref _username, value);
    }

    private string? _password;
    public string? Password
    {
        get => _password == null || IsPasswordVisible ? _password : new string('*', _password.Length);
        set => SetField(ref _password, value);
    }

    private string? _notes;
    public string? Notes
    {
        get => _notes;
        set => SetField(ref _notes, value);
    }

    private bool _passwordVisible;
    public bool IsPasswordVisible
    {
        get => _passwordVisible;
        set
        {
            SetField(ref _passwordVisible, value);

            OnPropertyChanged(nameof(Password));
        }
    }

    public Guid Id { private get; init; }

    #region DeleteCommand

    public ICommand DeleteCommand { get; }
    private async Task Delete(object? parameter, CancellationToken token)
    {
        var canBeDeleted = _dialogService.ShowConfirmation($"¿Quieres borrar la credencial de {AppName}?", "Eliminación de credencial");

        if (!canBeDeleted) return;

        try
        {
            await _credentialRepository.Delete(Id);

            CredentialDeleted?.Invoke(this, new AlteredItemEventArgs { Id = Id });
        }
        catch (Exception e)
        {
            _dialogService.ShowError(e, "Error al borrar");
        }
    }
    public event EventHandler<AlteredItemEventArgs>? CredentialDeleted;

    #endregion

    #region EditCredentialCommand

    public ICommand EditCredentialCommand { get; }
    public async Task EditCredential(object? parameter, CancellationToken token)
    {
        var handler = (IHandleCredentialCreationEdition)parameter!;

        var selectedCredential = await _credentialRepository.GetById(Id);

        CreateEditCredentialWindowViewModel viewModel = new(_credentialRepository, _encryptionService, _dialogService, _fileService, _appService)
        {
            Id = selectedCredential.Id,
            AppName = selectedCredential.AppName,
            Notes = selectedCredential.Notes,
            Username = selectedCredential.AccessUser,
            Password = _encryptionService.DecryptPassword(selectedCredential.AccessPassword),
            ImagePath = selectedCredential.ImagePath,
            IsEdition = true,
            OriginalCredential = selectedCredential
        };

        var wasEdited = handler.EditCredential(viewModel);

        if (!wasEdited) return;

        selectedCredential = await _credentialRepository.GetById(Id);

        CredentialEdited?.Invoke(this, new AlteredItemEventArgs { Credential = selectedCredential, Id = Id });
    }
    public event EventHandler<AlteredItemEventArgs>? CredentialEdited;

    #endregion

    #region CopyToClipboardCommand

    public ICommand CopyToClipboardCommand { get; }

    private void CopyToClipboard(object? param)
    {
        switch ((string)param!)
        {
            case Fields.Password:
                _clipboardService.CopyText(_password!);
                break;
            case Fields.Username:
                _clipboardService.CopyText(_username!); 
                break;
            default:
                throw new ArgumentException($"The parameter {param} must be a string defined in Field class", nameof(param));
        }
    }

    #endregion

    #region ShowPasswordCommand

    public ICommand ShowPasswordCommand { get; }

    #endregion
}