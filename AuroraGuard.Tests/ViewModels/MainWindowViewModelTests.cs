using System.Windows.Input;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.UserInterface.ViewModels.Main;

namespace AuroraGuard.Tests.ViewModels;

public class MainWindowViewModelTests
{
	private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
	private readonly MainWindowViewModel _sut;

	public MainWindowViewModelTests()
	{
		_sut = Substitute.For<MainWindowViewModel>(_navigationService);
	}

	[Fact]
	public void ExecuteWindowsOperationsCommand_ShouldCallWindowClose()
	{
		// Arrange
		var resizableWindow = Substitute.For<IResizableWindow>();
		
		// Act
		_sut.CloseWindowCommand.Execute(resizableWindow);
		_sut.MaximizeRestoreWindowCommand.Execute(resizableWindow);
		_sut.MinimizeWindowCommand.Execute(resizableWindow);

		// Assert
		resizableWindow.Received().Close();
		resizableWindow.Received().MaximizeRestore();
		resizableWindow.Received().Minimize();
	}
	
	[Fact]
	public void ExecuteWindowsOperationsCommand_ThrowsException_WhenParameterTypeIsNotIClosableWindow()
	{
		// Arrange
		var falseClosableWindow = Substitute.For<IDialogService>();
		IEnumerable<ICommand> commands =
			[_sut.CloseWindowCommand, _sut.MaximizeRestoreWindowCommand, _sut.MinimizeWindowCommand];

		Assert.All(commands, command =>
		{
			Assert.Throws<Exception>(() => command.Execute(falseClosableWindow));
			Assert.Throws<Exception>(() => command.Execute(null));
		});
	}
}