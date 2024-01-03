namespace AuroraGuard.Core.Interfaces.Services;

public interface INavigationService
{
    void NavigateTo<TViewModel>() where TViewModel : class;
    public void NavigateTo<TOriginWindow, TDestinationWindow>() 
        where TOriginWindow : class
        where TDestinationWindow : class;
}