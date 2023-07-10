using AuroraGuard.Core.Interfaces;
using AuroraGuard.DataAccess.Repositories.Credentials;
using AuroraGuard.DataAccess.Repositories.Users;

namespace AuroraGuard.DataAccess.UnitOfWork;

public interface IAuroraGuardUnitOfWork : IUnitOfWork
{
	ICredentialRepository CredentialRepository { get; }
	IUserRepository UserRepository { get; }
}