namespace AuroraGuard.Core.DTO.Credentials;

public class UpdateCredentialDto
{
    public string AccessUser { get; set; } = null!;
    public byte[] AccessPassword { get; set; } = null!;
    public string AppName { get; set; } = null!;
    public string? ImagePath { get; set; } = null!;
    public string? Notes { get; set; }
}