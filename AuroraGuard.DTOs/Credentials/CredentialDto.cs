using AuroraGuard.Core.Models;

namespace AuroraGuard.DTOs.Credentials;

public class CredentialDto
{
	public string Id { get; set; } = null!;
	public string AccessUser { get; set; } = null!;
	public string AccessPassword { get; set; } = null!;
	public DateTime ModifiedAt { get; set; }

	public static CredentialDto operator +(CredentialDto dto, Credential credential)
	{
		
		return new CredentialDto();
	}
}