using System;
using System.Windows;
using AuroraGuard.Core.Interfaces.Services;
using Microsoft.Win32;

namespace AuroraGuard.UserInterface.WPF.Services;

public class DialogService(OpenFileDialog fileDialog) : IDialogService
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

    public string? SelectSingleImage()
    {
        fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        fileDialog.Multiselect = false;
        fileDialog.Filter = "All Images|*.BMP;*.JPG;*.JPEG;*.PNG";

        var dialogResult = fileDialog.ShowDialog();

        return dialogResult == true ? fileDialog.FileName : null;
    }
}