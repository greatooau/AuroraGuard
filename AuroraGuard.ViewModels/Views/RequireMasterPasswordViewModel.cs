using System.Windows.Input;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces.Services;

namespace AuroraGuard.ViewModels.Views;

public class RequireMasterPasswordViewModel : ViewModelBase
{
	private readonly IAuthService _authService;
	private readonly IDialogService _dialogService;
	private readonly INavigationService _navigationService;
	
	public readonly ICommand RequestAccess;
	
	private string _password = null!;

	public RequireMasterPasswordViewModel(INavigationService navigationService, IAuthService authService, IDialogService dialogService)
	{
		_navigationService = navigationService;
		_authService = authService;
		_dialogService = dialogService;
		RequestAccess = new RelayCommand(null, ExecuteRequestAccess);
	}

	public string Password
	{
		get => _password;
		set
		{
			_password = value;
			OnPropertyChanged();
		}
	}
	
	private void ExecuteRequestAccess(object? parameter)
	{
		var canAccess = _authService.CanAccess(_password);

		if (!canAccess)
		{
			_dialogService.ShowMessage("Cannot access due to invalid password");
			return;
		}
		
		_navigationService.NavigateTo<MainViewViewModel>();
	}
	
}