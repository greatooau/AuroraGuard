using System.Text.Json;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.Tests.ViewModels.HelperClasses;
using AuroraGuard.UserInterface.ViewModels.Auth;

namespace AuroraGuard.Tests.ViewModels;

public class EnterPasswordViewModelTests
{
	private readonly EnterPasswordViewModel _sut;
	private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
	private readonly IAuthService _authService = Substitute.For<IAuthService>();
	private readonly IDialogService _dialogService = Substitute.For<IDialogService>();
	private const string Password = "FeliciaTheGoat";
	
	public EnterPasswordViewModelTests() => _sut = new EnterPasswordViewModel(_authService, _dialogService, _navigationService);

	[Fact]
	public void ExecuteRequestAccessCommand_ShouldDisplayMessage_WhenAccessIsDenied()
	{
		// Arrange
		IPasswordContainer passwordContainer = new Parameter(Password);
		_authService.CanAccess(Password).Returns(false);
		
		// Act
		_sut.RequestAccessCommand.Execute(passwordContainer);
		
		// Assert
		_dialogService.Received().ShowMessage(Arg.Any<string>());
		_authService.Received().CanAccess(Password);
	}
	
	[Fact]
	public void ExecuteRequestAccessCommand_ShouldNavigateToMainView_WhenAccessGranted()
	{
		// Arrange
		var window = Substitute.For<Parameter>(Password);
		_authService.CanAccess(Password).Returns(true);
		
		// Act
		_sut.RequestAccessCommand.Execute(window);
		
		// Assert
		_authService.Received().CanAccess(Password);
		window.Received().Navigate();
	}

	[Theory]
	[InlineData("")]
	[InlineData("'Cause God don't like ugly and no one likes niggas")]
	public void CanRequestAccess_ShouldReturnCorrectResult(string password)
	{
		// Arrange
		IPasswordContainer passwordContainer = Substitute.For<Parameter>(password);
		
		// Act
		var canExecute = _sut.RequestAccessCommand.CanExecute(passwordContainer);

		// Assert
		if (password.Length > 0) 
			Assert.True(canExecute);
		else
			Assert.False(canExecute);
	}
}
