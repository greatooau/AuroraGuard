using System.Security.Cryptography;
using System.Text;
using AuroraGuard.Core.Models;
using AuroraGuard.DataAccess.Repositories.Users;
using AuroraGuard.DTOs.Login;

namespace AuroraGuard.Services.Auth;

public class AuthService : IAuthService
{
	private const int KeySize = 32;
	private readonly IUserRepository _userRepository;

	public AuthService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public byte[] HashPassword(string plainTextPassword, out byte[] salt)
	{
		salt = RandomNumberGenerator.GetBytes(KeySize);
		var hash = Rfc2898DeriveBytes.Pbkdf2(plainTextPassword, salt, 1000, HashAlgorithmName.SHA256, KeySize);

		return hash;
	}
	
	public async Task<bool> ValidateCredentials(LoginDto loginDto)
	{
		if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password)) return false;

		var user = await _userRepository.GetUserByUserName(loginDto.Username);

		if (user is null)
			return false;

		var isAuthenticated = ValidatePassword(loginDto, user);

		return isAuthenticated;
	}

	private static bool ValidatePassword(LoginDto loginDto, User user)
	{
		var hashedPassword = Rfc2898DeriveBytes.Pbkdf2(loginDto.Password, Encoding.UTF8.GetBytes(user.Salt), 1000, HashAlgorithmName.SHA256, KeySize);

		return CryptographicOperations.FixedTimeEquals(hashedPassword, Convert.FromHexString(user.Password));
	}
}