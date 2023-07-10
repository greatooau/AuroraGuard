using AuroraGuard.Core.Models;
using AuroraGuard.DTOs.Users;

namespace AuroraGuard.Services.Users;

public interface IUserService
{
	Task<User> RegisterUser(CreateUserDto createUserDto);
}