using System.Windows.Input;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;

namespace AuroraGuard.UserInterface.ViewModels.Auth;

public class AuthWindowViewModel : ViewModel
{
	public AuthWindowViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        CloseWindowCommand = new RelayCommand(CloseWindow);
    }
    
    #region Bindable Properties

    private readonly INavigationService _navigationService = null!;
    public INavigationService NavigationService
    {
        get => _navigationService;
        init => SetField(ref _navigationService, value);
    }
    private Type CurrentViewModelType => ((ICurrentViewModelContainer) NavigationService).CurrentViewModel.GetType();

    public double WindowHeight => CurrentViewModelType == typeof(EnterPasswordViewModel) ? 225 : 300;
    public double TitleHeight => 20;
    public double OuterMarginSizeThickness => 0;
    public double ResizeBorderThickness => 0;
    
    #region Commands

    #region CloseWindowCommand

    public ICommand CloseWindowCommand { get; }
    public static void CloseWindow(object? parameter)
    {
        if (parameter is not IClosableWindow window) 
            throw new Exception($"Parameter of type {parameter?.GetType().FullName} should inherit {nameof(IClosableWindow)}");
        
        window.Close();
    }

    #endregion

    #endregion
    
    #endregion
}