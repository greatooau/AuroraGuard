using System.Data;
using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Models;

namespace AuroraGuard.DataAccess.Repositories.Credentials;

public class CredentialRepository(IDbTransaction transaction, IDapperRepository dapperRepository)
	: ICredentialRepository
{
	public async Task<Credential> Create(CreateCredentialDto createCredentialDto)
	{
		const string sql = @"
			INSERT INTO Credentials (Id, AccessUser, AccessPassword, CreatedAt, ModifiedAt)
			VALUES (@Id, @AccessUser, @AccessPassword, @CreatedAt, @ModifiedAt)";

		var param = CredentialRepositoryHelper.GenerateCreateParam(createCredentialDto);
		
		await dapperRepository.ExecuteAsync(sql, param, transaction);
		
		transaction.Commit();
		
		return (Credential)param;
	}

	public Task<Credential> GetById(Guid id)
	{
		const string sql = "SELECT * FROM Credentials WHERE Id = @Id";

		var param = CredentialRepositoryHelper.GenerateGetByIdParam(id);
		
		return dapperRepository.QuerySingleAsync<Credential>(sql, param);
	}
}