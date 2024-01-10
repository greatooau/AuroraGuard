using System.Data;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.DataAccess.Repositories;
using AuroraGuard.DataAccess.Repositories.Credentials;
using AuroraGuard.DataAccess.TypeHandlers;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.DataAccess;

public static class DependencyContainer
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
        SqlMapper.RemoveTypeMap(typeof(Guid));
        SqlMapper.RemoveTypeMap(typeof(Guid?));

        services.AddTransient<IDbConnection>(serviceProvider =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>()!;
            var connection = GetConnection(configuration);
            connection.Open();
            return connection;
        });

        services.AddTransient<IDapperRepository, DapperRepository>();
        services.AddTransient<ICredentialRepository, CredentialRepository>();
        return services;
    }

    private static SqliteConnection GetConnection(IConfiguration configuration)
    {
        var dbName = configuration.GetConnectionString("aurora-guard");
        var appDirectory = configuration["app-directory"];

        if (dbName is null || appDirectory is null) 
            throw new Exception("Connection string must not be null");

        var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appDirectory, dbName);

        return new SqliteConnection($"DataSource={fileName};");
    }
}