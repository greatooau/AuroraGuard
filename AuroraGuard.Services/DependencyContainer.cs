using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.Services;

public static class DependencyContainer
{
	// , IConfiguration configuration
	public static IServiceCollection AddAuroraGuardServices(this IServiceCollection services)
	{
		services.AddTransient<IFileService, FileService>();
		services.AddTransient<IAuthService, AuthService>();
		services.AddTransient<IEncryptionService, EncryptionService>();
        services.AddTransient<IAppService, AppService>();

		return services;
	}
}