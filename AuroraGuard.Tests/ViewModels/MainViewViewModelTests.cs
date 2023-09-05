using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.ViewModels.Views;

namespace AuroraGuard.Tests.ViewModels;

public class MainViewViewModelTests
{
	private readonly MainViewViewModel _sut;

	public MainViewViewModelTests()
	{
		_sut = new MainViewViewModel();
	}

	private const string Password = "FeliciaTheGoat";
}