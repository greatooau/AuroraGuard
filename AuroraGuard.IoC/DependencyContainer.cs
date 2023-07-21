using AuroraGuard.DataAccess;
using AuroraGuard.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.IoC;

public static class DependencyContainer
{
	public static IServiceCollection AddAuroraGuardDependencies(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDataAccess(configuration);
		services.AddAuroraGuardServices();
		return services;
	}
}