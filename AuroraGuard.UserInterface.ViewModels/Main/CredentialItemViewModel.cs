namespace AuroraGuard.UserInterface.ViewModels.Main;

public class CredentialItemViewModel(DateTime creationDate, string type, string appName)
{
	public string CreationDate { get;  } = creationDate.ToString("d/M/yyyy hh:mm");
	public string Type { get;  } = type;
	public string AppName { get;  } = appName;
}