namespace AuroraGuard.Core.Models;

public class Credential
{
	public string Id { get; set; } = null!;
	public string AccessUser { get; set; } = null!;
	public string AccessPassword { get; set; } = null!;
	public DateTime ModifiedAt { get; set; }
	public DateTime CreatedAt { get; set; }
}