using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Models;

namespace AuroraGuard.Core.Interfaces.Repositories;

public interface ICredentialRepository
{
	Task<Credential> Create(CreateCredentialDto createCredentialDto);
	Task<Credential> GetById(Guid id);
	Task<IEnumerable<Credential>> GetAll();
    Task Update(Guid id, UpdateCredentialDto dto);
    Task Delete(Guid id);
}