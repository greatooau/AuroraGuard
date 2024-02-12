using Microsoft.Extensions.Configuration;

namespace AuroraGuard.IoC;

public static class AuroraGuardConfiguration
{
	public static IConfiguration? Configuration { get; set; }
	public static IConfiguration Get()
    {
        return Configuration ??= new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();
    }
}