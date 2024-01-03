using System;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.UserInterface.ViewModels.Auth;
using AuroraGuard.UserInterface.WPF.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.UserInterface.WPF;

internal static class Bootstrap
{
	internal static void Start(IServiceProvider serviceProvider)
	{
		var navigationService = serviceProvider.GetRequiredService<INavigationService>();
		var authService = serviceProvider.GetService<IAuthService>()!;

		if (authService.WasMasterPasswordSet())
			navigationService.NavigateTo<EnterPasswordViewModel>();
		else
			navigationService.NavigateTo<SetPasswordViewModel>();

		var authWindowViewModel = serviceProvider.GetService<AuthWindowViewModel>()!;

		var mainWindow = serviceProvider.GetService<AuthWindow>()!;

		mainWindow.Height = authWindowViewModel.WindowHeight;
		mainWindow.Show();
	}
}