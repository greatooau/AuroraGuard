namespace AuroraGuard.UserInterface.ViewModels.Main.Design;

public class MainViewDesignModel
{
    public static MainViewDesignModel Instance => new();
	public List<CredentialItemViewModel> CredentialList { get; set; } =
	[
		new CredentialItemViewModel("pepe", @"C:\Users\Sergio Adrian\OneDrive - Universidad Autonoma de Nuevo León\Cosas de la Uni\Descargas\negroperro.png", "pepe", DateTime.Now),
		new CredentialItemViewModel("pepe", @"C:\Users\Sergio Adrian\OneDrive - Universidad Autonoma de Nuevo León\Cosas de la Uni\Descargas\negroperro.png", "pepe", DateTime.Now)

	];

    public string AppName => CredentialList.First().AppName;
	public string Username => "TylerOkonma2";

	public string Notes =>
		"These are some notes aGuard.UserInterface.WPF -> C:\\cosas\\dotnet\\solutions\\AuroraGuard\\AuroraGuard.UserInterface.WPF\\bin\\Debug\\net7.0-windows\\AuroraGuard.UserInterface.WPF.dll\n4>------- Errors: 0. Warnings: 0\nCopying file from \"C:\\cosas\\dotnet\\solutions\\AuroraGuard\\AuroraGuard.UserInterface.WP";
}