namespace AuroraGuard.Core.Interfaces.Services;

public interface IAuthService
{
	(byte[] hash, byte[] hashSalt) HashPassword(string plainTextPassword, byte[]? salt = null);
	bool SaveMasterPassword(string masterPassword);
	bool WasMasterPasswordSet();
	bool CanAccess(string password);
}