using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces.Stores;

namespace AuroraGuard.ViewModels.Windows;

public class MainWindowViewModel : ViewModelBase
{
	private readonly INavigationStore _navigationStore;

	public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel!;
	
	public MainWindowViewModel(INavigationStore navigationStore)
	{
		_navigationStore = navigationStore;
		navigationStore.OnCurrentViewModelChanges += OnCurrentViewModelChange;
	}

	private void OnCurrentViewModelChange() => OnPropertyChanged(nameof(CurrentViewModel));
}