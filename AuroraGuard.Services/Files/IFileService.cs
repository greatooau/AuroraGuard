namespace AuroraGuard.Services.Files;

public interface IFileService
{
	public bool CreateFile(string path);
	public Task<bool> WriteBytesAsync(string path, byte[] bytes);
}