using AuroraGuard.Core.Models;
using AuroraGuard.DTOs.Users;

namespace AuroraGuard.DataAccess.Repositories.Users;

public static class UserRepositoryHelper
{
	public static object GenerateGetByUsernameParam(string username) => new {Username = username};

	public static User GenerateCreateParam(CreateUserDto createUserDto)
	{
		return new User
		{
			Id = createUserDto.Id, 
			Password = createUserDto.Password,
			Name = createUserDto.Name,
			Username = createUserDto.Username,
			Salt = createUserDto.Salt,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now
		};
	}
	
	public static object GenerateUpdateParam(Guid id, UpdateUserDto updateUserDto)
	{
		return new
		{
			Id = id.ToString(),
			updateUserDto.Name,
			updateUserDto.Password,
			UpdatedAt = DateTime.Now
		};
	}

	public static object GenerateGetByIdParam(Guid id) => new {Id = id.ToString()};
}