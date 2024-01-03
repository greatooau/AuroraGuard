using AuroraGuard.UserInterface.ViewModels.Auth;
using AuroraGuard.UserInterface.ViewModels.Main;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.UserInterface.ViewModels;

public static class DependencyContainer
{
	public static IServiceCollection AddViewModels(this IServiceCollection services)
	{
		services.AddSingleton<EnterPasswordViewModel>();
		services.AddSingleton<SetPasswordViewModel>();
		services.AddSingleton<MainViewModel>();
		return services;
	}
}