namespace AuroraGuard.Core.Interfaces;

public interface IFileService
{
	public IFileStream Create(string path);
	public byte[] ReadAllBytes(string path);
    public void Copy(string path, string destination);
    public void Delete(string path);
}