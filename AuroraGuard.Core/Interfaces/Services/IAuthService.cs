namespace AuroraGuard.Core.Interfaces.Services;

/// <summary>
/// Provides all the auth methods needed in this app
/// </summary>
public interface IAuthService
{
	/// <summary>
	/// Hashes a password
	/// </summary>
	/// <param name="plainTextPassword">The password to be hashed</param>
	/// <param name="salt">A custom salt to use to hash the password</param>
	/// <returns>A tuple containing the hash and the pair of hash and Salt already hashed</returns>
	(byte[] hash, byte[] hashSalt) HashPassword(string plainTextPassword, byte[]? salt = null);
	/// <summary>
	/// Saves user's master password
	/// </summary>
	/// <param name="masterPassword">The password passed to save</param>
	/// <returns><c>true</c> if the password was successfully saved</returns>
    bool SaveMasterPassword(string masterPassword);
	/// <summary>
	/// Checks if the master password was already saved
	/// </summary>
	/// <returns></returns>
	bool WasMasterPasswordSet();
	/// <summary>
	/// Checks if the password passed is correct for the current windows user
	/// </summary>
	/// <param name="password">The password to be verified</param>
	/// <returns><c>true</c> if the password is correct</returns>
	bool CanAccess(string password);
}