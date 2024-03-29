﻿using AuroraGuard.Core.Implementations;
using AuroraGuard.Core.Interfaces;

namespace AuroraGuard.Services;

public class FileService : IFileService
{
	public IFileStream Create(string path) => new FileStreamWrapper(File.Create(path));
	public byte[] ReadAllBytes(string path) => File.ReadAllBytes(path);

    public void Copy(string path, string destination)
    {
        File.Copy(path, destination, true);
    }
	public void Delete(string path) => File.Delete(path);
}