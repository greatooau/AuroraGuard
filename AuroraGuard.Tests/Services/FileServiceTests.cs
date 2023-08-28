using System.IO.Abstractions;
using AuroraGuard.Services.Dialog;
using AuroraGuard.Services.Files;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace AuroraGuard.Tests.Services;

public class FileServiceTests
{
	private readonly IDialogService _dialogService = Substitute.For<IDialogService>();
	private readonly IFile _file = Substitute.For<IFile>();
	private readonly IFileService _sut;
	
	public FileServiceTests()
	{
		_sut = new FileService(_dialogService, _file);
	}

	[Fact]
	public void CreateFile_ShouldShowErrorMessage_IfExceptionOcurred()
	{
		// Arrange
		_file.Create(Arg.Any<string>()).Throws(new IOException("File already exists"));

		// Act
		var result = _sut.CreateFile("Existent file");
		
		// Assert
		_dialogService.Received().ShowError(Arg.Any<IOException>(), Arg.Any<string>());
		Assert.False(result);
	}

	[Fact]
	public async void WriteBytesAsync_ShouldShowErrorMessage_IfFileWasNotFound()
	{
		// Arrange
		_file.Exists(Arg.Any<string>()).Returns(false);

		// Act
		var result = await _sut.WriteBytesAsync("Filename.txt", Array.Empty<byte>());
		
		// Assert
		_dialogService.Received().ShowError(Arg.Any<IOException>(), Arg.Any<string>());
		Assert.False(result);
	}
	
	[Fact]
	public async void WriteBytesAsync_ShouldCallWriteAllBytesAsync()
	{
		// Arrange
		_file.Exists(Arg.Any<string>()).Returns(true);

		// Act
		var result = await _sut.WriteBytesAsync("Filename.txt", Array.Empty<byte>());
		
		// Assert
		await _file.Received().WriteAllBytesAsync("Filename.txt", Array.Empty<byte>());
		Assert.True(result);
	}
}
