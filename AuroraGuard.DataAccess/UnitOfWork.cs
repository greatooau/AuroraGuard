using System.Data;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;

namespace AuroraGuard.DataAccess;

public class UnitOfWork(ICredentialRepository credentialRepository, IDbTransaction transaction)
	: IAuroraGuardUnitOfWork
{
	public ICredentialRepository CredentialRepository { get; } = credentialRepository;

	public void SaveChanges()
	{
		try
		{
			transaction.Commit();
		}
		catch (Exception)
		{
			transaction.Rollback();
		}
	}
}