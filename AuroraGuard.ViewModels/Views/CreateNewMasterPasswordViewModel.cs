using System.Windows.Input;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces.Services;

namespace AuroraGuard.ViewModels.Views;

public class CreateNewMasterPasswordViewModel : ViewModelBase
{
	private readonly IDialogService _dialogService;
	private readonly IAuthService _authService;
	private readonly INavigationService _navigationService;
	
	private string _password = null!;
	private string _confirmPassword = null!;
	public ICommand SavePassword;

	public CreateNewMasterPasswordViewModel(IDialogService dialogService,IAuthService authService, INavigationService navigationService)
	{
		_dialogService = dialogService;
		_authService = authService;
		_navigationService = navigationService;
		SavePassword = new RelayCommand(null, ExecuteSavePassword);
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

	public string ConfirmPassword
	{
		get => _confirmPassword;
		set
		{
			_confirmPassword = value;
			OnPropertyChanged();
		}
	}

	private void ExecuteSavePassword(object? parameter)
	{
		var wasPasswordSaved = _authService.SaveMasterPassword(_password);

		if (!wasPasswordSaved)
		{
			_dialogService.ShowMessage("Couldn't save password");
			return;
		};
		
		_navigationService.NavigateTo<MainViewViewModel>();
	}
}