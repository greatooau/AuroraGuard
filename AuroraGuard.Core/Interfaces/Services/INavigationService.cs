namespace AuroraGuard.Core.Interfaces.Services;

public interface INavigationService
{
	void NavigateTo<T>() where T : class;
	void GoBack();
	void GoForward();
}