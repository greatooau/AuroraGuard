using System.IO.Abstractions;
using AuroraGuard.Services.Dialog;

namespace AuroraGuard.Services.Files;

public class FileService : IFileService
{
	private readonly IDialogService _dialogService;
	private readonly IFile _file;

	public FileService(IDialogService dialogService, IFile file)
	{
		_dialogService = dialogService;
		_file = file;
	}

	public bool CreateFile(string path)
	{
		try
		{
			_file.Create(path);
			return true;
		}
		catch (Exception e)
		{
			_dialogService.ShowError(e, "File creation Error");
			return false;
		}
	}
	

	public async Task<bool> WriteBytesAsync(string path, byte[] bytes)
	{
		try
		{
			if (!_file.Exists(path))
				throw new FileNotFoundException($"File {path} not found", path);

			await _file.WriteAllBytesAsync(path, bytes);
			return true;
		}
		catch (FileNotFoundException e)
		{
			_dialogService.ShowError(e, e.Message);
			return false;
		}
	}
}