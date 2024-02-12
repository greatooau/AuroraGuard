using System.Security.Cryptography;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace AuroraGuard.Services;

public class AuthService(IFileService fileService, IConfiguration configuration) : IAuthService
{
	private const int KeySizeInBytes = 32;

	public (byte[] hash, byte[] hashSalt) HashPassword(string plainTextPassword, byte[]? salt = null)
	{
		salt ??= RandomNumberGenerator.GetBytes(KeySizeInBytes);
		
		var hash = Rfc2898DeriveBytes.Pbkdf2(plainTextPassword, salt, 1000, HashAlgorithmName.SHA256, KeySizeInBytes);

		var hashSalt = MergeHashAndSalt(hash, salt);

		return (hash, hashSalt);
	}

	private static byte[] MergeHashAndSalt(byte[] hash, byte[] salt)
	{
		var hashSaltBytes = new byte[hash.Length + salt.Length];

		Buffer.BlockCopy(hash, 0, hashSaltBytes, 0, hash.Length);
		Buffer.BlockCopy(salt, 0, hashSaltBytes, hash.Length, salt.Length);
		
		return hashSaltBytes;
	}

	
	public bool SaveMasterPassword(string masterPassword)
	{
		var filePath = GetPasswordFilePath();
		
		if (filePath is null) return false;
		
		var hashSaltBytes = HashPassword(masterPassword).hashSalt;

		using var file = fileService.Create(filePath);
		
		file.Write(hashSaltBytes);

		return true;
	}

	public bool WasMasterPasswordSet()
	{
		var filePath = GetPasswordFilePath();
		if (filePath is null) return false;
		
		byte[] hashSaltBytes;
		try
		{
			hashSaltBytes = fileService.ReadAllBytes(filePath);
		}
		catch (Exception)
		{

			return false;
		}

		if (hashSaltBytes.Length != KeySizeInBytes * 2)
			return false;

		var hashBytes = hashSaltBytes[new Range(Index.Start,KeySizeInBytes)];
		var saltBytes = hashSaltBytes[new Range(KeySizeInBytes, Index.End)];
		
		return hashBytes.Length == KeySizeInBytes && saltBytes.Length == KeySizeInBytes;
	}

	public bool CanAccess(string password)
	{
		var filePath = GetPasswordFilePath();
	
		if (filePath is null) return false;

		var hashSaltBytes = fileService.ReadAllBytes(filePath);

		if (hashSaltBytes.Length != KeySizeInBytes * 2) return false;
	
		var hashBytes = hashSaltBytes[new Range(Index.Start,KeySizeInBytes)];
		var saltBytes = hashSaltBytes[new Range(KeySizeInBytes, Index.End)];

		var hash = HashPassword(password, saltBytes).hash;

		var result = CryptographicOperations.FixedTimeEquals(hash, hashBytes);
		
		return result;
	}

	private string? GetPasswordFilePath()
	{
		var fileName = configuration["masterPassword-filename"];
		var appDirectory = configuration["app-directory"];
		
		if (fileName is null || appDirectory is null) return null;
		
		var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appDirectory, fileName);
		return filePath;
	}
}