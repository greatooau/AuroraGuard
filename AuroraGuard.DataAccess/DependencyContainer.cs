using System.Data;
using AuroraGuard.DataAccess.Repositories;
using AuroraGuard.DataAccess.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.DataAccess;

public static class DependencyContainer
{
	public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<IDbConnection>(_ =>
		{
			var connectionString = configuration.GetConnectionString("aurora-guard")!;
			return DatabaseFactory.CreateDatabaseConnection(connectionString);
		});

		services.AddScoped<IDbTransaction>(serviceProvider =>
		{
			var connection = serviceProvider.GetService<IDbConnection>()!;
			
			connection.Open();

			return connection.BeginTransaction();
		});
		
		services.AddScoped<IAuroraGuardUnitOfWork, AuroraGuardUnitOfWork>();

		services.AddRepositories();
		
		
		return services;
	}
}