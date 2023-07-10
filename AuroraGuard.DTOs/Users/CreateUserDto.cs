namespace AuroraGuard.DTOs.Users;

public class CreateUserDto
{
	public string Username { get; set; } = null!;
	public string Password { get; set; } = null!;
	public string Name { get; set; } = null!;
	public byte[] Salt { get; set; } = null!;
}