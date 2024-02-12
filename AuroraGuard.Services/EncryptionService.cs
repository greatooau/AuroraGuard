using System.Security.Cryptography;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace AuroraGuard.Services;

public class EncryptionService(IConfiguration configuration, IFileService fileService) : IEncryptionService
{
    private const int KeySize = 32;
    private const int IvSize = 16;
    
    public byte[] EncryptText(string plainText, byte[] key)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        
        using (var streamWriter = new StreamWriter(cryptoStream))
            streamWriter.Write(plainText);

        return [..aes.IV, ..memoryStream.ToArray()];
    }

    public string DecryptText(byte[] cipherText, byte[] key, byte[] iv)
    {
        using var aes = Aes.Create();
            
        using var memoryStream = new MemoryStream(cipherText);

        using var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);

        using var streamReader = new StreamReader(cryptoStream);

        return streamReader.ReadToEnd();
    }

    public void CreateKeyFile()
    {
        using var fileStream = fileService.Create(GetKeyFilePath());
        
        fileStream.Write(CreateKey());
    }

    private static byte[] CreateKey()
    {
        var key = new byte[KeySize];

        using var rng = RandomNumberGenerator.Create();
        
        rng.GetBytes(key);

        return key;
    }

    public byte[] GetStoredKey() => fileService.ReadAllBytes(GetKeyFilePath());

    private string GetKeyFilePath()
    {
        var kf = configuration["key-filename"]!;
        var directoryName = configuration["app-directory"]!;
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            directoryName, kf);

        var fileInfo = new FileInfo(path);

        var directoryInfo = fileInfo.Directory;

        if (directoryInfo!.Exists) return path;

        directoryInfo.Create();
        directoryInfo.Attributes = FileAttributes.Hidden;

        return path;
    }

    public string DecryptPassword(byte[] ivPassword)
    {
        var iv = new byte[IvSize];
        var password = new byte[ivPassword.Length - IvSize]; 
        
        Buffer.BlockCopy(ivPassword, 0, iv, 0, IvSize);
        Buffer.BlockCopy(ivPassword, IvSize, password, 0, password.Length);

        return DecryptText(password, GetStoredKey(), iv);
    }
}