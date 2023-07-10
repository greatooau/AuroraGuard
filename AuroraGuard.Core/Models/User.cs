namespace AuroraGuard.Core.Models;

public class User
{
	public string Id { get; set; } = null!;
	public string Password { get; set; } = null!;
	public string Salt { get; set; } = null!;
	public string Name { get; set; } = null!;
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}