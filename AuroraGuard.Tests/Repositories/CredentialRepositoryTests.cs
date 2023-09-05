﻿using System.Data;
using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Models;
using AuroraGuard.DataAccess.Repositories.Credentials;

namespace AuroraGuard.Tests.Repositories;

public class CredentialRepositoryTests
{
	private readonly ICredentialRepository _sut;
	private readonly IDbTransaction _dbTransaction = Substitute.For<IDbTransaction>();
	private readonly IDapperRepository _dapperRepository = Substitute.For<IDapperRepository>();

	public CredentialRepositoryTests() => _sut = new CredentialRepository(_dbTransaction, _dapperRepository);

	[Fact]
	public async void Create_ExecuteAndCommit()
	{
		// Arrange
		var createCredentialDto = new CreateCredentialDto
		{	
			Id = Guid.NewGuid(),
			AccessUser = "Tyler",
			AccessPassword = "Aurora"
		};
		
		// Act
		await _sut.Create(createCredentialDto);

		// Assert
		await _dapperRepository.Received().ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), _dbTransaction);
		_dbTransaction.Received().Commit();
	}


	[Fact]
	public async void GetById_ShouldReturnObjectAndCallQuerySingleAsync()
	{
		// Arrange
		var id = Guid.NewGuid();
		var param = CredentialRepositoryHelper.GenerateGetByIdParam(id);
		
		_dapperRepository
			.QuerySingleAsync<Credential>(Arg.Any<string>(), param)
			.Returns(new Credential
			{
				Id = id.ToString(),
				AccessUser = "Aurora",
				AccessPassword = "Tyler",
				CreatedAt = DateTime.Now,
				ModifiedAt = DateTime.Now
			});

		// Act
		var credential = await _sut.GetById(id);

		// Assert
		Assert.Equal(id.ToString(), credential.Id);
		await _dapperRepository.Received().QuerySingleAsync<Credential>(Arg.Any<string>(), param);
	}
}