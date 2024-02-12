namespace AuroraGuard.Core.Interfaces.Services;

public interface IEncryptionService
{
	byte[] EncryptText(string plainText, byte[] key);
	string DecryptText(byte[] cipherText, byte[] key, byte[] iv);
	void CreateKeyFile();
	byte[] GetStoredKey();
	string DecryptPassword(byte[] ivPassword);
}