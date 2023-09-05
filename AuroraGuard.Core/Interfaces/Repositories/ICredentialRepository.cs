using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Models;

namespace AuroraGuard.Core.Interfaces.Repositories;

public interface ICredentialRepository
{
	Task<Credential> Create(CreateCredentialDto createCredentialDto);
	Task<Credential> GetById(Guid id);
}