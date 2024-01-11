using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Models;

namespace AuroraGuard.Core.Interfaces;

public interface IHandleCredentialCreationEdition
{
    Credential? CreateCredential(ICredentialRepository credentialRepository);
    void EditCredential(ICredentialRepository credentialRepository, Credential credential);
}