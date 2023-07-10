using System.Data;
using AuroraGuard.Core.Models;
using AuroraGuard.DTOs.Users;
using Dapper;

namespace AuroraGuard.DataAccess.Repositories.Users;

public class UserRepository : IUserRepository
{
	private readonly IDbConnection _connection;
	private readonly IDbTransaction _transaction;

	public UserRepository(IDbConnection connection, IDbTransaction transaction)
	{
		_connection = connection;
		_transaction = transaction;
	}

	public Task<User?> GetUserByUserName(string username)
	{
		const string sql = "SELECT * FROM User WHERE Username = @Username";

		var param = UserRepositoryHelper.GenerateGetUserByUsernameParam(username);

		return _connection.QuerySingleOrDefaultAsync<User?>(sql, param);
	}

	public Task<User> GetUserById(string id)
	{
		const string sql = "SELECT * FROM User WHERE Id = @Id";

		var param = UserRepositoryHelper.GenerateGetUserByIdParam(id);
		
		return _connection.QuerySingleAsync<User>(sql, param);;
	}

	public async Task<User> CreateUser(CreateUserDto createUserDto)
	{
		const string sql = "INSERT INTO User (Id, Password, Salt, Name, CreatedAt, UpdatedAt)" +
		                   "VALUES (@Id, @Password, @Salt, @Name, @CreatedAt, @UpdatedAt)" +
		                   "RETURNING Id";

		var param = UserRepositoryHelper.GenerateCreateUserParam(createUserDto);

		var id = await _connection.QuerySingleAsync<string>(sql, param, _transaction);

		
		return await GetUserById(id);
	}

	public async Task UpdateUser(string id, UpdateUserDto updateUserDto)
	{
		const string sql = @"
			UPDATE User 
			SET  
			    Password = @Password,
			    Name = @Name,
			    UpdatedAt = @UpdatedAt
			WHERE Id = @Id";

		var param = UserRepositoryHelper.GenerateUpdateUserParam(id, updateUserDto);

		await _connection.ExecuteAsync(sql, param, _transaction);
	}
}