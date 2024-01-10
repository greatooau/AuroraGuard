namespace AuroraGuard.UserInterface.ViewModels.Main;

public class CredentialItemViewModel(string id, string? imagePath, string appName, DateTime creationDate)
{
	public string Id { get; } = id;
	public string? ImagePath { get; } = imagePath;
	public string AppName { get; } = appName;
	public string CreationDate { get; } = creationDate.ToString("dd/MM/yyyy hh:mm tt");
}