﻿using AuroraGuard.DataAccess;
using AuroraGuard.Services;
using AuroraGuard.UserInterface.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraGuard.IoC;

public static class DependencyContainer
{
	public static IServiceCollection AddAuroraGuardDependencies(this IServiceCollection services)
	{
		services.AddSingleton(_ => AuroraGuardConfiguration.Get());
		
		services.AddDataAccess();
		services.AddAuroraGuardServices();
		services.AddViewModels();
		return services;
	}
}