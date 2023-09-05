using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.ViewModels.Views;

namespace AuroraGuard.Tests.ViewModels;

public class RequireMasterPasswordViewModelTests
{
	private readonly RequireMasterPasswordViewModel _sut;
	private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
	private readonly IAuthService _authService = Substitute.For<IAuthService>();
	private readonly IDialogService _dialogService = Substitute.For<IDialogService>();
	private const string Password = "FeliciaTheGoat";
	
	public RequireMasterPasswordViewModelTests() => _sut = new RequireMasterPasswordViewModel(_navigationService, _authService, _dialogService);

	[Fact]
	public void ExecuteRequestAccess_ShouldDisplayMessage_WhenAccessIsDenied()
	{
		// Arrange
		_sut.Password = Password;
		_authService.CanAccess(Password).Returns(false);

		// Act
		_sut.RequestAccess.Execute(null);

		// Assert
		_dialogService.Received().ShowMessage(Arg.Any<string>());
		_authService.Received().CanAccess(Password);
	}
	
	[Fact]
	public void ExecuteRequestAccess_ShouldNavigateToMainView_WhenAccessGranted()
	{
		// Arrange
		_sut.Password = Password;
		_authService.CanAccess(Password).Returns(true);

		// Act
		_sut.RequestAccess.Execute(null);
		
		// Assert
		_authService.Received().CanAccess(Password);
		_navigationService.Received().NavigateTo<MainViewViewModel>();
	}
	
	
}