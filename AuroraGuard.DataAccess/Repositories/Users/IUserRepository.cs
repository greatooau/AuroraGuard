using AuroraGuard.Core.Models;
using AuroraGuard.DTOs.Users;

namespace AuroraGuard.DataAccess.Repositories.Users;

public interface IUserRepository
{
	Task<User?> GetByUserName(string username);
	Task<User> Create(CreateUserDto createUserDto);
	Task Update(Guid id, UpdateUserDto updateUserDto);
	Task<User> GetById(Guid id);
}