using AuroraGuard.Core.Models;
using AuroraGuard.DTOs.Credentials;

namespace AuroraGuard.DataAccess.Repositories.Credentials;

public interface ICredentialRepository
{
	Task<Credential> CreateCredential(CreateCredentialDto createCredentialDto, string userId);
	Task<IEnumerable<Credential>> GetCredentialsByUserId(string userId);
	Task<Credential> GetCredentialById(string id);
}