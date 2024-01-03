using System;
using System.Windows;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.UserInterface.WPF.Delegates;

namespace AuroraGuard.UserInterface.WPF.Services;

public class NavigationService(Func<Type, ViewModel> viewModelFactory, WindowResolver resolver)
    : ObservableObject, INavigationService, ICurrentViewModelContainer
{
    private ViewModel _currentViewModel = null!;
    public ViewModel CurrentViewModel
    {
        get => _currentViewModel;
        set => SetField(ref _currentViewModel, value);
    }

    public void NavigateTo<TViewModel>() where TViewModel : class => CurrentViewModel = viewModelFactory(typeof(TViewModel));

    public void NavigateTo<TOriginWindow, TDestinationWindow>() 
        where TOriginWindow : class 
        where TDestinationWindow : class
    {
        var areWindows = typeof(TOriginWindow).IsSubclassOf(typeof(Window))  && typeof(TDestinationWindow).IsSubclassOf(typeof(Window));
        
        if (!areWindows) return;
        
        if (resolver(typeof(TDestinationWindow).Name) is not { } targetWindow) throw new Exception("");
        
        targetWindow.Show();
        
        if (resolver(typeof(TOriginWindow).Name) is not { } originWindow) throw new Exception("");

        originWindow.Close();
    }
}