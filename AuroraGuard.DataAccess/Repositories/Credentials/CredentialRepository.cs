using System.Data;
using AuroraGuard.Core.Models;
using AuroraGuard.DTOs.Credentials;
using Dapper;

namespace AuroraGuard.DataAccess.Repositories.Credentials;

public class CredentialRepository : ICredentialRepository
{
	private readonly IDbTransaction _transaction;
	private readonly IDbConnection _connection;

	public CredentialRepository(IDbTransaction transaction, IDbConnection connection)
	{
		_transaction = transaction;
		_connection = connection;
	}

	public async Task<Credential> CreateCredential(CreateCredentialDto createCredentialDto, string userId)
	{
		const string sql = @"
			INSERT INTO Credentials (Id, AccessUser, AccessPassword, UserId, CreatedAt, ModifiedAt)
			VALUES (@Id, @AccessUser, @AccessPassword, @UserId, @CreatedAt, @ModifiedAt)
			RETURNING Id";

		var param = CredentialRepositoryHelper.GenerateCreateCredentialParam(createCredentialDto);
		
		var id = await _connection.QuerySingleAsync<string>(sql, param, _transaction);

		return await GetCredentialById(id);
	}

	public Task<IEnumerable<Credential>> GetCredentialsByUserId(string userId)
	{
		const string sql = "SELECT * FROM Credentials WHERE UserId = @UserId";

		var param = CredentialRepositoryHelper.GenerateGetCredentialByUserIdParam(userId);
		
		return _connection.QueryAsync<Credential>(sql, param);
	}

	public Task<Credential> GetCredentialById(string id)
	{
		const string sql = "SELECT * FROM Credentials WHERE Id = @Id";

		var param = CredentialRepositoryHelper.GenerateGetCredentialByIdParam(id);
		
		return _connection.QuerySingleAsync<Credential>(sql, param);
	}
}