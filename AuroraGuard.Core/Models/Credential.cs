namespace AuroraGuard.Core.Models;

public class Credential
{
	public Guid Id { get; set; }
	public string AccessUser { get; set; } = null!;
	public byte[] AccessPassword { get; set; } = null!;
	public string AppName { get; set; } = null!;
	public string? ImagePath { get; set; } = null!;
	public string? Notes { get; set; }
	public DateTime UpdatedAt { get; init; }
	public DateTime CreatedAt { get; init; }
}