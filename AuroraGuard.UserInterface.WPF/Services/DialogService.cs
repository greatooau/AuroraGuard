using System;
using System.Windows;
using AuroraGuard.Core.Interfaces.Services;

namespace AuroraGuard.UserInterface.WPF.Services;

public class DialogService : IDialogService
{
	public void ShowMessage(string message, string caption = "AURORA's Guard says")
	{
		MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
	}

	public bool ShowConfirmation(string message, string title)
	{
		var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);

		return result == MessageBoxResult.Yes;
	}

	public void ShowError(Exception ex, string title)
	{
		MessageBox.Show(ex.Message, title, MessageBoxButton.OK, MessageBoxImage.Error);
	}
}