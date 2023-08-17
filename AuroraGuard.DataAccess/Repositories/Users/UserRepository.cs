using System.Data;
using AuroraGuard.Core.Models;
using AuroraGuard.DataAccess.Repositories.DapperRepository;
using AuroraGuard.DTOs.Users;

namespace AuroraGuard.DataAccess.Repositories.Users;

public class UserRepository : IUserRepository
{
	private readonly IDapperRepository _dapperRepository;
	private readonly IDbTransaction _transaction;

	public UserRepository(IDapperRepository dapperRepository, IDbTransaction transaction)
	{
		_dapperRepository = dapperRepository;
		_transaction = transaction;
	}

	public Task<User?> GetByUserName(string username)
	{
		const string sql = "SELECT * FROM User WHERE Username = @Username";

		var param = UserRepositoryHelper.GenerateGetByUsernameParam(username);

		return _dapperRepository.QuerySingleOrDefaultAsync<User?>(sql, param);
	}

	public Task<User> GetById(Guid id)
	{
		const string sql = "SELECT * FROM User WHERE Id = @Id";

		var param = UserRepositoryHelper.GenerateGetByIdParam(id);
		
		return _dapperRepository.QuerySingleAsync<User>(sql, param);
	}

	public async Task<User> Create(CreateUserDto createUserDto)
	{
		const string sql = "INSERT INTO User (Id, Password, Username, Salt, Name, CreatedAt, UpdatedAt)" +
		                   "VALUES (@Id, @Password, @Username, @Salt, @Name, @CreatedAt, @UpdatedAt)";

		var user = UserRepositoryHelper.GenerateCreateParam(createUserDto);

		await _dapperRepository.ExecuteAsync(sql, user, _transaction);
		
		_transaction.Commit();

		return user;
	}

	public async Task Update(Guid id, UpdateUserDto updateUserDto)
	{
		const string sql = @"
			UPDATE User 
			SET  
			    Password = @Password,
			    Name = @Name,
			    UpdatedAt = @UpdatedAt
			WHERE Id = @Id";

		var param = UserRepositoryHelper.GenerateUpdateParam(id, updateUserDto);

		await _dapperRepository.ExecuteAsync(sql, param, _transaction);
		
		_transaction.Commit();
	}
}