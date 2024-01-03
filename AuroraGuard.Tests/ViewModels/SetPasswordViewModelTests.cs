using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.Tests.ViewModels.HelperClasses;
using AuroraGuard.UserInterface.ViewModels.Auth;
using AuroraGuard.UserInterface.WPF.Services;
using AuroraGuard.UserInterface.WPF.Windows;

namespace AuroraGuard.Tests.ViewModels;

public class SetPasswordViewModelTests
{
	private readonly SetPasswordViewModel _sut;
	private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
	private readonly IAuthService _authService = Substitute.For<IAuthService>();
	private readonly IDialogService _dialogService = Substitute.For<IDialogService>();
	private const string Password = "FeliciaTheGoat";

	public SetPasswordViewModelTests() => _sut = new SetPasswordViewModel(_dialogService, _authService, _navigationService);

	[Fact]
	public void ExecuteSavePassword_ShouldShowMessage_WhenPasswordCouldntBeSaved()
	{
		// Arrange
		_sut.Password = Password;
		_authService.SaveMasterPassword(Password).Returns(false);

		// Act
		_sut.SavePasswordCommand.Execute(null);

		// Assert
		_authService.Received().SaveMasterPassword(Password);
		_dialogService.Received().ShowMessage(Arg.Any<string>());
	}

	[Fact]
	public void ExecuteSavePassword_ShouldNavigateToMainView_WhenPasswordSaved()
	{
		// Arrange
		_sut.Password = Password;
		var window = Substitute.For<Parameter>();
		_authService.SaveMasterPassword(Password).Returns(true);

		// Act
		_sut.SavePasswordCommand.Execute(window);

		// Assert
		_authService.Received().SaveMasterPassword(Password);
		window.Received().Navigate();
	}
}