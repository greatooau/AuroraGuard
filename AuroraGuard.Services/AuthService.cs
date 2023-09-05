using System.IO.Abstractions;
using System.Security.Cryptography;
using AuroraGuard.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace AuroraGuard.Services;

public class AuthService : IAuthService
{
	private readonly IFile _file;
	private readonly IDialogService _dialogService;
	private readonly IConfiguration _configuration;
	private const int KeySizeInBytes = 32;

	public AuthService(IFile file, IDialogService dialogService, IConfiguration configuration)
	{
		_file = file;
		_dialogService = dialogService;
		_configuration = configuration;
	}

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
		var fileName = _configuration["masterPassword-filename"];
		
		if (fileName is null) return false;
		
		var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileName);

		_file.Create(filePath);

		var hashSaltBytes = HashPassword(masterPassword).hashSalt;

		_file.WriteAllBytes(filePath, hashSaltBytes);
		
		return true;
	}

	public bool WasMasterPasswordSet()
	{
		var fileName = _configuration["masterPassword-filename"];
	
		if (fileName is null) return false;
		
		var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileName);

		byte[] hashSaltBytes;
		try
		{
			hashSaltBytes = _file.ReadAllBytes(filePath);
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
		var fileName = _configuration["masterPassword-filename"];
	
		if (fileName is null) return false;
		var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileName);

		var hashSaltBytes = _file.ReadAllBytes(filePath);

		if (hashSaltBytes.Length != KeySizeInBytes * 2) return false;
	
		var hashBytes = hashSaltBytes[new Range(Index.Start,KeySizeInBytes)];
		var saltBytes = hashSaltBytes[new Range(KeySizeInBytes, Index.End)];

		var hash = HashPassword(password, saltBytes).hash;

		var result = CryptographicOperations.FixedTimeEquals(hash, hashBytes);
		
		return result;
	}
}