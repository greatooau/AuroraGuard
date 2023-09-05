using System.Data;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.DataAccess;

public static class DependencyContainer
{
	public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<IDbConnection>(_ =>
		{
			var dbName = configuration.GetConnectionString("aurora-guard");
			
			if (dbName is null)
				throw new Exception("Connection string must not be null");

			var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), dbName);

			return new SqliteConnection($"DataSource={fileName};");
		});

		services.AddScoped<IDbTransaction>(serviceProvider =>
		{
			var connection = serviceProvider.GetService<IDbConnection>()!;
			
			connection.Open();

			return connection.BeginTransaction();
		});
		
		services.AddScoped<IAuroraGuardUnitOfWork, UnitOfWork>();

		services.AddRepositories();
		
		return services;
	}
}