﻿using AuroraGuard.DataAccess.Repositories.Credentials;
using AuroraGuard.DataAccess.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.DataAccess.Repositories;

internal static class RepositoriesContainer
{
	internal static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<ICredentialRepository, CredentialRepository>();
		services.AddScoped<IUserRepository, UserRepository>();

		return services;
	}
}