using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.UserInterface.ViewModels.Auth;

namespace AuroraGuard.Tests.ViewModels;
		
public class AuthWindowViewModelTests
{
	private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
	private readonly AuthWindowViewModel _sut;

	public AuthWindowViewModelTests()
	{
		_sut = Substitute.For<AuthWindowViewModel>(_navigationService);
	}

	[Fact]
	public void ExecuteCloseWindowCommand_ShouldCallWindowClose()
	{
		// Arrange
		var closableWindow = Substitute.For<IClosableWindow>();
		
		// Act
		_sut.CloseWindowCommand.Execute(closableWindow);

		// Assert
		closableWindow.Received().Close();
	}
	
	[Fact]
	public void ExecuteCloseWindowCommand_ThrowsException_WhenParameterTypeIsNotIClosableWindow()
	{
		// Arrange
		var falseClosableWindow = Substitute.For<IDialogService>();
		
		// Act - Assert
		Assert.Throws<Exception>(() => _sut.CloseWindowCommand.Execute(falseClosableWindow));
		Assert.Throws<Exception>(() => _sut.CloseWindowCommand.Execute(null));
	}
}