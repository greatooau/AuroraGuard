using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces.Stores;

namespace AuroraGuard.ViewModels.Stores;

public class NavigationStore : INavigationStore
{
	private ViewModelBase? _currentViewModel;
	public ViewModelBase? CurrentViewModel
	{
		get => _currentViewModel;
		set
		{
			
			if (_currentViewModel != null && _currentViewModel.Equals(value)) return;

			_currentViewModel = value;
			
			if (ViewModelsStack.Count == 0) AddToStack(value!);

			OnCurrentViewModelChanges?.Invoke();	
		}
	}

	public Stack<ViewModelBase> ViewModelsStack { get; } = new();
	public int CurrentViewModelStackIndex { get; set; } = -1;

	private void AddToStack(ViewModelBase viewModel)
	{
		ViewModelsStack.Push(viewModel);
		CurrentViewModelStackIndex = 0;
	}
	
	public event Action? OnCurrentViewModelChanges;
}
