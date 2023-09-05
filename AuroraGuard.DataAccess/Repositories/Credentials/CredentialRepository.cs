using System.Data;
using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Models;

namespace AuroraGuard.DataAccess.Repositories.Credentials;

public class CredentialRepository : ICredentialRepository
{
	private readonly IDbTransaction _transaction;
	private readonly IDapperRepository _dapperRepository;

	public CredentialRepository(IDbTransaction transaction, IDapperRepository dapperRepository)
	{
		_transaction = transaction;
		_dapperRepository = dapperRepository;
	}

	public async Task<Credential> Create(CreateCredentialDto createCredentialDto)
	{
		const string sql = @"
			INSERT INTO Credentials (Id, AccessUser, AccessPassword, CreatedAt, ModifiedAt)
			VALUES (@Id, @AccessUser, @AccessPassword, @CreatedAt, @ModifiedAt)";

		var param = CredentialRepositoryHelper.GenerateCreateParam(createCredentialDto);
		
		await _dapperRepository.ExecuteAsync(sql, param, _transaction);
		
		_transaction.Commit();
		
		return (Credential)param;
	}

	public Task<Credential> GetById(Guid id)
	{
		const string sql = "SELECT * FROM Credentials WHERE Id = @Id";

		var param = CredentialRepositoryHelper.GenerateGetByIdParam(id);
		
		return _dapperRepository.QuerySingleAsync<Credential>(sql, param);
	}
}