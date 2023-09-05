using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.DataAccess.Repositories.Credentials;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.DataAccess.Repositories;

internal static class RepositoriesContainer
{
	internal static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<ICredentialRepository, CredentialRepository>();
		services.AddScoped<IDapperRepository, DapperRepository>();

		return services;
	}
}