using System.Data;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;

namespace AuroraGuard.DataAccess;

public class UnitOfWork : IAuroraGuardUnitOfWork
{
	private readonly IDbTransaction _transaction;

	public ICredentialRepository CredentialRepository { get; }

	public UnitOfWork(ICredentialRepository credentialRepository, IDbTransaction transaction)
		=> (_transaction, CredentialRepository) = (transaction, credentialRepository);

	public void SaveChanges()
	{
		try
		{
			_transaction.Commit();
		}
		catch (Exception e)
		{
			_transaction.Rollback();
		}
	}
}