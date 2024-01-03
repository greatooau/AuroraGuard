using System.Windows;

namespace AuroraGuard.UserInterface.WPF.Helpers;

public static class WindowsOperationsHelper
{
	public static void DoMaximizeRestore(this Window window)
	{
		window.WindowState = window.WindowState == WindowState.Normal 
			? WindowState.Maximized
			: WindowState.Normal;
	}

	public static void DoMinimize(this Window window)
	{
		window.WindowState = WindowState.Minimized;
	}
}