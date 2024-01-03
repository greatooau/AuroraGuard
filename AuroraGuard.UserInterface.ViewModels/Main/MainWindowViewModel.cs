using System.Windows.Input;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;

namespace AuroraGuard.UserInterface.ViewModels.Main;

public class MainWindowViewModel : ViewModel
{
    public MainWindowViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        MinimizeWindowCommand = new RelayCommand(MinimizeWindow);
        MaximizeRestoreWindowCommand = new RelayCommand(MaximizeRestoreWindow);
        CloseWindowCommand = new RelayCommand(CloseWindow);
    }
    
    #region Bindable Properties
    
    public double TitleHeight => 30;
    public double OuterMarginSizeThickness => 0;
    public double ResizeBorderThickness => 0;
    
    private readonly INavigationService _navigationService = null!;
    public INavigationService NavigationService
    {
        get => _navigationService;
        init => SetField(ref _navigationService, value);
    }

    #region Commands

    #region MinimizeWindowCommand
    public ICommand MinimizeWindowCommand { get; }
    public static void MinimizeWindow(object? parameter)
    {
        if (parameter is not IResizableWindow window) 
            throw new Exception($"Parameter of type {parameter?.GetType().FullName} should inherit {nameof(IResizableWindow)}");
        
        window.Minimize();
    }

    #endregion

    #region MaximizeRestoreWindowCommand
    
    public ICommand MaximizeRestoreWindowCommand { get; }
    
    public static void MaximizeRestoreWindow(object? parameter)
    {
        if (parameter is not IResizableWindow window) 
            throw new Exception($"Parameter of type {parameter?.GetType().FullName} should inherit {nameof(IResizableWindow)}");

        window.MaximizeRestore();
    }

    #endregion

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