using AuroraGuard.Core.Interfaces;

namespace AuroraGuard.Core.Implementations;

public class FileStreamWrapper(Stream stream) : IFileStream
{
	public void Write(ReadOnlySpan<byte> buffer) => stream.Write(buffer);

	public void Dispose()
	{
		stream.Dispose();
	}
}