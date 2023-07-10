using AuroraGuard.Services.Auth;
using AuroraGuard.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.Services;

public static class DependencyContainer
{
	public static IServiceCollection AddAuroraGuardServices(this IServiceCollection services)
	{
		services.AddTransient<IAuthService, AuthService>();
		services.AddTransient<IUserService, UserService>();
		
		return services;
	}
}