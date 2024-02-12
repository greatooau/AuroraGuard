namespace AuroraGuard.Core.DTO.Credentials;

public class CreateCredentialDto
{
	public Guid Id { get; set; }
	public string AccessUser { get; set; } = null!;
	public byte[] AccessPassword { get; set; } = null!;
	public string AppName { get; set; } = null!;
	public string? ImagePath { get; set; }
    public string? Notes { get; set; }
}