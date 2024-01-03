using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.Core.Security;

namespace AuroraGuard.UserInterface.ViewModels.Auth;

public class EnterPasswordViewModel : ViewModel
{
    private readonly IAuthService _authService;
    private readonly IDialogService _dialogService;
    public readonly INavigationService NavigationService;
    
    public EnterPasswordViewModel(IAuthService authService, IDialogService dialogService, INavigationService navigationService)
    {
        _authService = authService;
        _dialogService = dialogService;
        NavigationService = navigationService;
        RequestAccessCommand = new RelayCommand(ExecuteRequestAccess, CanRequestAccess);
    }
    
    #region Bindable Properties
    
    #region Commands
    
    public RelayCommand RequestAccessCommand { get; }
    private void ExecuteRequestAccess(object? parameter)
    {
        var securePassword = (parameter as IPasswordContainer)!.SecurePassword;
        
        var canAccess = _authService.CanAccess(securePassword.Unsecure()!);

        if (!canAccess)
        {
            _dialogService.ShowMessage("Cannot access due to invalid password");
            return;
        }

        if (parameter is not IHandleWindowNavigation navigationHandler)
            throw new Exception("Window Navigation is not being handled");
        
        navigationHandler.Navigate();
    }
    
    public void OnCanRequestAccessChanged() => RequestAccessCommand.OnCanExecuteChanged();
    private bool CanRequestAccess(object? parameter)
    {
        var securePassword = (parameter as IPasswordContainer)!.SecurePassword;
        
        return securePassword.Length > 0;
    }
    #endregion

    #endregion   
}