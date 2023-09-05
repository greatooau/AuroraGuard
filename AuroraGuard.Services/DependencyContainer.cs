using System.IO.Abstractions;
using AuroraGuard.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.Services;

public static class DependencyContainer
{
	public static IServiceCollection AddAuroraGuardServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSingleton<IFileSystem, FileSystem> ();

		services.AddTransient<IFile, FileWrapper>();
		
		services.AddTransient<IAuthService, AuthService>(sp =>
		{
			var dialogService = sp.GetRequiredService<IDialogService>();
			
			var file = sp.GetRequiredService<IFile>();

			return new AuthService(file, dialogService, configuration);
		});

		return services;
	}
}