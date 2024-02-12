using System.Data;
using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Models;

namespace AuroraGuard.DataAccess.Repositories.Credentials;

public class CredentialRepository(IDbConnection connection, IDapperRepository dapperRepository) : ICredentialRepository
{
	public async Task<Credential> Create(CreateCredentialDto createCredentialDto)
	{
		const string sql = """
                           INSERT INTO Credentials (Id, AccessUser, AccessPassword, AppName, ImagePath, Notes, CreatedAt, UpdatedAt)
                           VALUES (@Id, @AccessUser, @AccessPassword, @AppName, @ImagePath, @Notes, @CreatedAt, @UpdatedAt)
                           """;
        
		var param = CredentialRepositoryHelper.GenerateCreateParam(createCredentialDto);
        
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            await dapperRepository.ExecuteAsync(connection, sql, param, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new Exception(e.Message, e);
        }
        finally
        {
            connection.Close();
        }
		
		return await GetById(createCredentialDto.Id);
	}

	public Task<Credential> GetById(Guid id)
	{
		const string sql = "SELECT * FROM Credentials WHERE Id = @Id";
		
		return dapperRepository.QuerySingleAsync<Credential>(connection, sql, new { Id = id });
	}

	public Task<IEnumerable<Credential>> GetAll()
	{
		const string sql = "SELECT * FROM Credentials";
		return dapperRepository.QueryAsync<Credential>(connection, sql);
	}

    public async Task Update(Guid id, UpdateCredentialDto dto)
    {
        const string sql = """
                           UPDATE Credentials SET
                             AccessUser = @AccessUser,
                             AccessPassword = @AccessPassword,
                             AppName = @AppName,
                             ImagePath = @ImagePath,
                             UpdatedAt = @UpdatedAt,
                             Notes = @Notes
                           WHERE Id = @Id
                           """;

        connection.Open();
        using var transaction = connection.BeginTransaction();

        var @params = CredentialRepositoryHelper.GenerateUpdateParam(id, dto);

        try
        {
            await dapperRepository.ExecuteAsync(connection, sql, @params, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new Exception(e.Message, e);
        }
        finally
        {
            connection.Close();
        }
    }

    public async Task Delete(Guid id)
    {
        const string sql = "DELETE FROM Credentials WHERE Id = @Id";

        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            await dapperRepository.ExecuteAsync(connection, sql, new { Id = id }, transaction);
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new Exception(e.Message, e);
        }
        finally
        {
            connection.Close();
        }
    }
}