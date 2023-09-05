namespace AuroraGuard.Core.DTO.Credentials;

public class CreateCredentialDto
{
	public Guid Id { get; set; }
	public string AccessUser { get; set; } = null!;
	public string AccessPassword { get; set; } = null!;
	
}