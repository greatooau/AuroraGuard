using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Models;

namespace AuroraGuard.Core.Interfaces;

public interface IHandleCredentialCreation
{
    Credential? CreateCredential(ICredentialRepository credentialRepository);
}