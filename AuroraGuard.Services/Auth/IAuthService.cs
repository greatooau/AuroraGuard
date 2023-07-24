using System.Net;
using AuroraGuard.DTOs.Login;

namespace AuroraGuard.Services.Auth;

public interface IAuthService
{
	byte[] HashPassword(string plainTextPassword, out byte[] salt);
	Task<bool> ValidateCredentials(NetworkCredential credentials);
}