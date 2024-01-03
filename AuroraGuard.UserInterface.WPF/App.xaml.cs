using System.Windows;
using AuroraGuard.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.UserInterface.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddUiDependencies();
        services.AddAuroraGuardDependencies();

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Bootstrap.Start(_serviceProvider);

        base.OnStartup(e);
    }
}