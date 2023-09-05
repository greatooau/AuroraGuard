using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.ViewModels.Views;

namespace AuroraGuard.Tests.ViewModels;

public class CreateNewMasterPasswordViewModelTests
{
	private readonly CreateNewMasterPasswordViewModel _sut;
	private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
	private readonly IAuthService _authService = Substitute.For<IAuthService>();
	private readonly IDialogService _dialogService = Substitute.For<IDialogService>();
	private const string Password = "FeliciaTheGoat";

	public CreateNewMasterPasswordViewModelTests() => _sut = new CreateNewMasterPasswordViewModel(_dialogService, _authService, _navigationService);

	[Fact]
	public void ExecuteSavePassword_ShouldShowMessage_WhenPasswordCouldntBeSaved()
	{
		// Arrange
		_sut.Password = Password;
		_authService.SaveMasterPassword(Password).Returns(false);

		// Act
		_sut.SavePassword.Execute(null);

		// Assert
		_authService.Received().SaveMasterPassword(Password);
		_dialogService.Received().ShowMessage(Arg.Any<string>());
	}

	[Fact]
	public void ExecuteSavePassword_ShouldNavigateToMainView_WhenPasswordSaved()
	{
		// Arrange
		_sut.Password = Password;
		_authService.SaveMasterPassword(Password).Returns(true);

		// Act
		_sut.SavePassword.Execute(null);

		// Assert
		_authService.Received().SaveMasterPassword(Password);
		_navigationService.Received().NavigateTo<MainViewViewModel>();
	}
}