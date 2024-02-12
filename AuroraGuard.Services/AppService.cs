using AuroraGuard.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace AuroraGuard.Services;

public class AppService(IConfiguration configuration) : IAppService
{
    public string GetAppImagesPath()
    {
        var imagesDirectory = Path.Combine(GetAppDirectory(), "images");

        var imagesDirectoryInfo = new DirectoryInfo(imagesDirectory);

        if (!imagesDirectoryInfo.Exists)
            imagesDirectoryInfo.Create();

        return imagesDirectory;
    }

    public string GetAppDirectory()
    {
        var appDirectory = configuration["app-directory"];

        return appDirectory is null
            ? throw new KeyNotFoundException("\"app-directory\" was not found in appsettings.json")
            : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appDirectory);
    }
}