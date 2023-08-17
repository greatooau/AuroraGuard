using System.Data;
using AuroraGuard.Core.Models;
using AuroraGuard.DataAccess.Repositories.DapperRepository;
using AuroraGuard.DataAccess.Repositories.Users;
using AuroraGuard.DTOs.Users;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace AuroraGuard.Tests.Repositories;

public class UserRepositoryTests
{
	private readonly IDbTransaction _dbTransaction = Substitute.For<IDbTransaction>();
	private readonly IDapperRepository _dapperRepository = Substitute.For<IDapperRepository>();
	private readonly IUserRepository _sut;

	public UserRepositoryTests()
	{
		_sut =  new UserRepository(_dapperRepository, _dbTransaction);
	}

	[Fact]
	public async Task GetByUsername_ShouldReturnNull_WhenUsernameNotFound()
	{
		// Arrange
	    _dapperRepository.QuerySingleOrDefaultAsync<User?>(Arg.Any<string>(), Arg.Any<object>()).ReturnsNull();
		
		// Act
		var user = await _sut.GetByUserName("");

		// Assert
		Assert.Null(user);
	}

	[Fact]
	public async Task GetById_ShouldReturnNull_WhenIdNotFound()
	{
		// Arrange
		_dapperRepository.QuerySingleAsync<User>(Arg.Any<string>(), Arg.Any<object>()).ReturnsNull();
		var id = Guid.NewGuid();
		
		// Act
		var user = await _sut.GetById(id);
		
		// Assert
		Assert.Null(user);
	}

	[Fact]
	public async Task Create_ShouldReturnNewUser()
	{
		// Arrange
		var userDto = new CreateUserDto { Name = "greatooau", Password = "greatooau", Username = "greatooau" };

		// Act
		var newUser = await _sut.Create(userDto);

		// Assert
		Assert.Equal(userDto.Id, newUser.Id);
		await _dapperRepository.Received().ExecuteAsync(Arg.Any<string>(), Arg.Any<User>(), _dbTransaction);
		_dbTransaction.Received().Commit();
	}
	
	[Fact]
	public async Task Update_ShouldExecuteAndCommit()
	{
		// Arrange
		var id = Guid.NewGuid();
		
		// Act
		await _sut.Update(id, new UpdateUserDto());
		
		// Assert
		await _dapperRepository.Received().ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), _dbTransaction);
		_dbTransaction.Received().Commit();
	}
	
}