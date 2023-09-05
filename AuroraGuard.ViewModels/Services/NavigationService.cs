using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.Core.Interfaces.Stores;

namespace AuroraGuard.ViewModels.Services;

public class NavigationService : INavigationService
{
	private readonly INavigationStore _navigationStore;

	public NavigationService(INavigationStore navigationStore)
	{
		_navigationStore = navigationStore;
	}

	public void NavigateTo<T>() where T : class
	{
		if (!typeof(T).IsSubclassOf(typeof(ViewModelBase)))
			throw new Exception($"{typeof(T)} is not of Type ViewModelBase");

		var newViewModel = Activator.CreateInstance<T>() as ViewModelBase;
		
		_navigationStore.ViewModelsStack.Push(newViewModel!);
		_navigationStore.CurrentViewModelStackIndex = 0;
		_navigationStore.CurrentViewModel = newViewModel;
	}
	
	public void GoBack()
	{
		var currentViewModel = _navigationStore.ViewModelsStack.ElementAt(++_navigationStore.CurrentViewModelStackIndex);
		_navigationStore.CurrentViewModel = currentViewModel;
	}

	public void GoForward()
	{
		var currentViewModel = _navigationStore.ViewModelsStack.ElementAt(--_navigationStore.CurrentViewModelStackIndex);

		if (currentViewModel is null)
			throw new Exception("There's no any view model forward");
		
		_navigationStore.CurrentViewModel = currentViewModel;
	}
}