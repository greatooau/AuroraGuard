using AuroraGuard.Core.Models;
using AuroraGuard.DTOs.Users;

namespace AuroraGuard.DataAccess.Repositories.Users;

internal static class UserRepositoryHelper
{
	internal static object GenerateGetUserByUsernameParam(string username) => new {Username = username};

	internal static object GenerateCreateUserParam(CreateUserDto createUserDto)
	{
		return new
		{
			Id = Guid.NewGuid().ToString(),
			createUserDto.Name,
			createUserDto.Password,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now
		};
	}
	
	internal static object GenerateUpdateUserParam(string id, UpdateUserDto updateUserDto)
	{
		return new
		{
			Id = id,
			updateUserDto.Name,
			updateUserDto.Password,
			UpdatedAt = DateTime.Now
		};
	}

	internal static object GenerateGetUserByIdParam(string id) => new {Id = id};
}