﻿namespace AuroraGuard.Core.Interfaces.Services;

public interface IDialogService
{
	void ShowMessage(string message, string caption = "AURORA's Guard says");
	bool ShowConfirmation(string message, string title);
	void ShowError(Exception ex, string title);
    string? SelectSingleImage();
}
