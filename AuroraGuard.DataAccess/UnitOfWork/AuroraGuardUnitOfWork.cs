using System.Data;
using AuroraGuard.DataAccess.Repositories.Credentials;
using AuroraGuard.DataAccess.Repositories.Users;
using Microsoft.Extensions.Logging;

namespace AuroraGuard.DataAccess.UnitOfWork;

public class AuroraGuardUnitOfWork : IAuroraGuardUnitOfWork
{
	private readonly IDbTransaction _transaction;
	private readonly ILogger<AuroraGuardUnitOfWork> _logger;

	public ICredentialRepository CredentialRepository { get; }
	public IUserRepository UserRepository { get; }
	
	public AuroraGuardUnitOfWork(
		ICredentialRepository credentialRepository,
		IUserRepository userRepository,
		IDbTransaction transaction,
		ILogger<AuroraGuardUnitOfWork> logger
		)
	{
		_transaction = transaction;
		_logger = logger;
		CredentialRepository = credentialRepository;
		UserRepository = userRepository;
	}
	
	public void SaveChanges()
	{
		try
		{
			_transaction.Commit();
		}
		catch (Exception e)
		{
#pragma warning disable CA2254
			_logger.LogError(e.Message);
#pragma warning restore CA2254
			_transaction.Rollback();
		}
	}
}