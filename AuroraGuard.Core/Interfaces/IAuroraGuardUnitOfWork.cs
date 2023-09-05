using AuroraGuard.Core.Interfaces.Repositories;

namespace AuroraGuard.Core.Interfaces;

public interface IAuroraGuardUnitOfWork : IUnitOfWork
{
	ICredentialRepository CredentialRepository { get; }
}