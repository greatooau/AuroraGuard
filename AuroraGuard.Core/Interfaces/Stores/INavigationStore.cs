using AuroraGuard.Core.Abstract;

namespace AuroraGuard.Core.Interfaces.Stores;

public interface INavigationStore
{
	ViewModelBase? CurrentViewModel { get; set; }
	int CurrentViewModelStackIndex { get; set; }
	event Action? OnCurrentViewModelChanges;
	Stack<ViewModelBase> ViewModelsStack{ get; }
}