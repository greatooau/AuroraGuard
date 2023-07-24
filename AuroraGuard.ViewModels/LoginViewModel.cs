using System.Net;
using System.Security;
using System.Windows.Input;
using AuroraGuard.Services.Auth;

namespace AuroraGuard.ViewModels;

public class LoginViewModel : ViewModelBase
{
	private SecureString _password = new();
	private string _username = null!;
	private readonly IAuthService _authService;

	private ICommand LoginCommand { get; }

	public LoginViewModel(IAuthService authService)
	{
		_authService = authService;
		LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
	}

	public SecureString Password
	{
		get => _password;
		set
		{
			_password = value;
			OnPropertyChanged();
		}
	}

	public string Username
	{
		get => _username;
		set
		{
			_username = value;
			OnPropertyChanged();
		}
	}

	private async void ExecuteLoginCommand(object parameter)
	{
		var credentials = new NetworkCredential(_username, _password);
		var isAuthenticated = await _authService.ValidateCredentials(credentials);
	}
	private bool CanExecuteLoginCommand(object obj)
	{
		var isInvalidData = string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password.Length < 3;

		return !isInvalidData;
	}
}