using System;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.UserInterface.ViewModels.Auth;
using AuroraGuard.UserInterface.ViewModels.Main;
using AuroraGuard.UserInterface.WPF.Delegates;
using AuroraGuard.UserInterface.WPF.Services;
using AuroraGuard.UserInterface.WPF.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.UserInterface.WPF;

internal static class DependencyContainer
{
	internal static IServiceCollection AddUiDependencies(this IServiceCollection services)
	{
		services.AddSingleton(serviceProvider => new AuthWindow
		{
			DataContext = serviceProvider.GetService<AuthWindowViewModel>()
		});
        
		services.AddSingleton(serviceProvider => new MainWindow
		{
			DataContext = serviceProvider.GetService<MainWindowViewModel>()
		});
        
		services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => 
			viewModelType => (ViewModel)serviceProvider.GetService(viewModelType)!);

		services.AddTransient<WindowResolver>(serviceProvider => window => window! switch
		{
			nameof(AuthWindow) => serviceProvider.GetService<AuthWindow>(),
			nameof(MainWindow) => serviceProvider.GetService<MainWindow>(),
			_ => throw new ArgumentOutOfRangeException(nameof(window))
		});

		services.AddSingleton<MainWindowViewModel>(); 
		services.AddSingleton<AuthWindowViewModel>();
        
		services.AddTransient<IDialogService, DialogService>();
		services.AddSingleton<INavigationService, NavigationService>();
		
		return services;
	}
}