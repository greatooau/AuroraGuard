using AuroraGuard.Core.Models;
using AuroraGuard.DTOs.Users;

namespace AuroraGuard.DataAccess.Repositories.Users;

public interface IUserRepository
{
	Task<User?> GetUserByUserName(string username);
	Task<User> CreateUser(CreateUserDto createUserDto);
	Task UpdateUser(string id, UpdateUserDto updateUserDto);
	Task<User> GetUserById(string id);
}