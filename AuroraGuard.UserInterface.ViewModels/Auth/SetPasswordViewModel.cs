using System.Windows.Input;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;

namespace AuroraGuard.UserInterface.ViewModels.Auth;

public class SetPasswordViewModel : ViewModel
{
    private readonly IDialogService _dialogService;
    private readonly IAuthService _authService;
    public readonly INavigationService NavigationService;

    public SetPasswordViewModel(IDialogService dialogService, IAuthService authService, INavigationService navigationService)
    {
        _dialogService = dialogService;
        _authService = authService;
        NavigationService = navigationService;
        SavePasswordCommand = new RelayCommand(SaveMasterPassword, CanSaveMasterPassword);
    }

    #region Bindable Properties
    
    private string? _password;
    public string Password
    {
        get => _password ?? "";
        set
        {
            SetField(ref _password, value);
            SavePasswordCommand.OnCanExecuteChanged();
        }
    }

    private string? _confirmPassword;
    public string ConfirmPassword
    {
        get => _confirmPassword ?? "";
        set
        {
            SetField(ref _confirmPassword, value);
            SavePasswordCommand.OnCanExecuteChanged();
        }
    }

    #region Commands

    public RelayCommand SavePasswordCommand { get; }
    private void SaveMasterPassword(object? parameter)
    {
        var wasPasswordSaved = _authService.SaveMasterPassword(Password);

        if (!wasPasswordSaved)
        {
            _dialogService.ShowMessage("Couldn't save password");
            return;
        }
        
        if (parameter is not IHandleWindowNavigation navigationHandler)
            throw new Exception("Window Navigation is not being handled");
        
        navigationHandler.Navigate();
    }

    private bool CanSaveMasterPassword(object? _)
    {
        return Password.Length != 0 && ConfirmPassword.Length != 0 && ConfirmPassword == Password; 
    }

    #endregion

    #endregion
}