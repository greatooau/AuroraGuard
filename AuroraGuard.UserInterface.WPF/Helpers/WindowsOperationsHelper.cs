using System.Windows;
using AuroraGuard.Core.Enum;

namespace AuroraGuard.UserInterface.WPF.Helpers;

public static class WindowsOperationsHelper
{
	public static WindowCurrentState DoMaximizeRestore(this Window window)
    {
        var isRestored = window.WindowState == WindowState.Normal;

        window.WindowState = isRestored ? WindowState.Maximized : WindowState.Normal;

        isRestored = window.WindowState == WindowState.Normal;

        return isRestored ? WindowCurrentState.Restored : WindowCurrentState.Maximized;
    }

	public static void DoMinimize(this Window window)
	{
		window.WindowState = WindowState.Minimized;
	}
}