using System.Windows;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.Core.Interfaces.Stores;
using AuroraGuard.IoC;
using AuroraGuard.UserInterface.WPF.Services;
using AuroraGuard.UserInterface.WPF.Windows;
using AuroraGuard.ViewModels.Services;
using AuroraGuard.ViewModels.Stores;
using AuroraGuard.ViewModels.Views;
using AuroraGuard.ViewModels.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuroraGuard.UserInterface.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
	public static IHost? AppHost { get; set; }
	// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
	private static IConfiguration? _configuration;

	public App()
	{
		_configuration = AuroraGuardConfiguration.Get();
		
		AppHost = Host.CreateDefaultBuilder()
		              .ConfigureServices((_, services) =>
		              {
			              services.AddSingleton<MainWindow>();
			              services.AddTransient<IDialogService, DialogService>();
			              
			              services.AddTransient<INavigationService, NavigationService>();
			              
			              
			              
			              services.AddAuroraGuardDependencies(_configuration);
			              
			              services.AddSingleton<INavigationStore, NavigationStore>();

			              services.AddTransient<MainWindowViewModel>(serviceProvider =>
			              {
				              var navigationStore = serviceProvider.GetRequiredService<INavigationStore>();
				              
				              var dialogService = serviceProvider.GetRequiredService<IDialogService>();
				              var authService = serviceProvider.GetRequiredService<IAuthService>();
				              var navigationService = serviceProvider.GetRequiredService<INavigationService>();

				              ViewModelBase initialViewModel = authService.WasMasterPasswordSet()
					              ? new RequireMasterPasswordViewModel(navigationService, authService, dialogService)
					              : new CreateNewMasterPasswordViewModel(dialogService, authService, navigationService);
				              
				              navigationStore.CurrentViewModel = initialViewModel;

				              return new MainWindowViewModel(navigationStore);
			              });
		              })
		              .Build();
	}

	protected override async void OnStartup(StartupEventArgs e)
	{
		await AppHost!.StartAsync();

		var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
		
		mainWindow.Show();
		base.OnStartup(e);
	}
}