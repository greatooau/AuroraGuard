using System.Data;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.DataAccess.Repositories;
using AuroraGuard.DataAccess.Repositories.Credentials;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.DataAccess;

public static class DependencyContainer
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        
        services.AddScoped(serviceProvider =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>()!;
            return GetConnection(configuration);
        });

        services.AddScoped(serviceProvider =>
        {
            var connection = serviceProvider.GetService<IDbConnection>()!;

            connection.Open();

            return connection.BeginTransaction();
        });

        services.AddScoped<IAuroraGuardUnitOfWork, UnitOfWork>();

        services.AddTransient<DapperRepository>();
        services.AddTransient<ICredentialRepository, CredentialRepository>();

        return services;
    }

    private static SqliteConnection GetConnection(IConfiguration configuration)
    {
        var dbName = configuration.GetConnectionString("aurora-guard");

        if (dbName is null) 
            throw new Exception("Connection string must not be null");

        var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), dbName);

        return new SqliteConnection($"DataSource={fileName};");
    }
}