using AuroraGuard.Core.Models;
using AuroraGuard.DataAccess.Repositories.Users;
using AuroraGuard.DTOs.Users;
using AuroraGuard.Services.Auth;

namespace AuroraGuard.Services.Users;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly IAuthService _authService;

	public UserService(IUserRepository userRepository, IAuthService authService)
	{
		_userRepository = userRepository;
		_authService = authService;
	}

	public Task<User> RegisterUser(CreateUserDto createUserDto)
	{
		var hash = _authService.HashPassword(createUserDto.Password, out var salt);
		createUserDto.Password = Convert.ToHexString(hash);
		createUserDto.Salt = salt;
		
		return _userRepository.CreateUser(createUserDto);
	}
	
	
}