using System.IO.Abstractions;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.Services;

public static class DependencyContainer
{
	// , IConfiguration configuration
	public static IServiceCollection AddAuroraGuardServices(this IServiceCollection services)
	{
		// services.AddTransient<IFileSystem, FileSystem> ();
		// services.AddTransient<IFile, FileWrapper>();
		services.AddTransient<IFileService, FileService>();
		services.AddTransient<IAuthService, AuthService>();
		
		return services;
	}
}