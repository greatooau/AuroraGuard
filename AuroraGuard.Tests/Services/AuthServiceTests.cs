﻿using System.Security.Cryptography;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace AuroraGuard.Tests.Services;

public class AuthServiceTests
{
	private readonly AuthService _sut;
	private readonly IFileService _file = Substitute.For<IFileService>();
	private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();
	private const int KeySizeInBytes = 32;
	private const string Password = "AuroraTyler";
	private const string AppDirectoryName = "Peggy";
	private const string FileName = "filename.txt";
	private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppDirectoryName, FileName);

	public AuthServiceTests()
	{
		_sut = new AuthService(_file, _configuration);
	}

	[Theory]
	[MemberData(nameof(HashPassword_TestData))]
	public void HashPassword_WithValidPasswordAndSalt_ReturnsHashAndHashSalt(string password, byte[]? salt)
	{
		// Arrange
		salt ??= RandomNumberGenerator.GetBytes(32);

		// Act
		var (hash, hashSalt) = _sut.HashPassword(password, salt);

		// Assert
		Assert.NotNull(hash);
		Assert.NotNull(hashSalt);
		Assert.Equal(KeySizeInBytes, hash.Length);
		Assert.Equal(KeySizeInBytes * 2, hashSalt.Length);
		Assert.StartsWith(hash.ToString(), hashSalt.ToString());
		Assert.EndsWith(salt.ToString(), hashSalt.ToString());
	}

	public static IEnumerable<object[]> HashPassword_TestData()
	{
		yield return [Password, RandomNumberGenerator.GetBytes(KeySizeInBytes)];
		yield return ["Password",  null!];
	}

	[Fact]
	public void Methods_ShouldReturnFalse_WhenMasterPasswordNameIsNull()
	{
		// Arrange
		_configuration[Arg.Any<string>()].ReturnsNull();
		
		// Act
		var wasMasterPasswordSaved = _sut.SaveMasterPassword(Password);
		var wasMasterPasswordSet = _sut.WasMasterPasswordSet();
		var canAccess = _sut.CanAccess(Password);

		// Assert
		Assert.False(wasMasterPasswordSaved);
		Assert.False(wasMasterPasswordSet);
		Assert.False(canAccess);
	}

	[Fact]
	public void WasMasterPasswordSet_ShouldShowError_WhenExceptionOcurredWhileReadingMasterPasswordFile()
	{
		// Arrange
		ConfigurationArrange();

		_file.ReadAllBytes(_filePath).Throws<Exception>();
		
		// Act
		var wasMasterPasswordSet = _sut.WasMasterPasswordSet();

		// Assert
		Assert.False(wasMasterPasswordSet);
		_file.Received().ReadAllBytes(_filePath);
	}

	private void ConfigurationArrange()
	{
		_configuration["masterPassword-filename"].Returns(FileName);
		_configuration["app-directory"].Returns(AppDirectoryName);
	}

	[Theory]
	[InlineData(20)]
	[InlineData(65)]
	[InlineData(63)]
	public void WasMasterPasswordSet_ShouldReturnFalse_WhenHashSaltLenghtIsNot32(int byteLength)
	{
		// Arrange
		ConfigurationArrange();
		
		var bytes = RandomNumberGenerator.GetBytes(byteLength);
		
		_file.ReadAllBytes(_filePath).Returns(bytes);
		
		// Act
		var wasMasterPasswordSet = _sut.WasMasterPasswordSet();

		// Assert
		Assert.False(wasMasterPasswordSet);
		_file.Received().ReadAllBytes(_filePath);
	}
	
	[Fact]
	public void WasMasterPasswordSet_ShouldReturnTrue()
	{
		// Arrange
		ConfigurationArrange();
		
		var bytes = RandomNumberGenerator.GetBytes(KeySizeInBytes * 2);
		
		_file.ReadAllBytes(_filePath).Returns(bytes);
		
		// Act
		var wasMasterPasswordSet = _sut.WasMasterPasswordSet();

		// Assert
		Assert.True(wasMasterPasswordSet);
		_file.Received().ReadAllBytes(_filePath);
	}

	[Theory]
	[InlineData(30)]
	[InlineData(9999)]
	public void CanAccess_ShouldReturnFalse_WhenHashSaltBytesLengthIsNot64(int byteLength)
	{
		// Arrange
		ConfigurationArrange();
		
		var bytes = RandomNumberGenerator.GetBytes(byteLength);
		
		_file.ReadAllBytes(_filePath).Returns(bytes);
		
		// Act
		var canAccess = _sut.CanAccess(Password);

		// Assert
		Assert.False(canAccess);
		_file.Received().ReadAllBytes(_filePath);
	}
	
	[Theory]
	[InlineData(Password)]
	[InlineData("ThisYoo")]
	[InlineData("1234")]
	[InlineData("")]
	public void CanAccess_ShouldLetAccess(string password)
	{
		// Arrange
		ConfigurationArrange();

		var salt = RandomNumberGenerator.GetBytes(KeySizeInBytes);

		var (_, hashSalt) = _sut.HashPassword(password, salt); 
		
		_file.ReadAllBytes(_filePath).Returns(hashSalt);
		
		// Act
		var canAccess = _sut.CanAccess(password);

		// Assert
		Assert.True(canAccess);
		_file.Received().ReadAllBytes(_filePath);
	}
}