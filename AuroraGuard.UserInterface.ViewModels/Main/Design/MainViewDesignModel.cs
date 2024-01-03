namespace AuroraGuard.UserInterface.ViewModels.Main.Design;

public class MainViewDesignModel
{
    public static MainViewDesignModel Instance => new();
	public List<CredentialItemViewModel> Credentials { get; set; } = new()
    {
        new CredentialItemViewModel(DateTime.Now, "eoeoe", "BBVA"),
        new CredentialItemViewModel(DateTime.Now, "aaaaa", "Facebook2")
    };

    public string AppName => Credentials.First().AppName;
	public string UserName => "TylerOkonma2";

	public string Notes =>
		"These are some notes aGuard.UserInterface.WPF -> C:\\cosas\\dotnet\\solutions\\AuroraGuard\\AuroraGuard.UserInterface.WPF\\bin\\Debug\\net7.0-windows\\AuroraGuard.UserInterface.WPF.dll\n4>------- Errors: 0. Warnings: 0\nCopying file from \"C:\\cosas\\dotnet\\solutions\\AuroraGuard\\AuroraGuard.UserInterface.WP";
}