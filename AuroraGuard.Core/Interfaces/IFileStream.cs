namespace AuroraGuard.Core.Interfaces;

public interface IFileStream : IDisposable
{
	public void Write(ReadOnlySpan<byte> buffer);
}