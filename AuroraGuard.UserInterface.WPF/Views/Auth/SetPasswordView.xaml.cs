using System.Windows.Controls;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.UserInterface.ViewModels.Auth;
using AuroraGuard.UserInterface.ViewModels.Main;
using AuroraGuard.UserInterface.WPF.Windows;

namespace AuroraGuard.UserInterface.WPF.Views.Auth;
/// <summary>
/// Interaction logic for SetPasswordView.xaml
/// </summary>
public partial class SetPasswordView : UserControl, IHandleWindowNavigation
{

    public SetPasswordView()
    {
        InitializeComponent();
    }
    
    public void Navigate()
    {
        if (DataContext is not SetPasswordViewModel viewModel)
            return;
        
        viewModel.NavigationService.NavigateTo<AuthWindow, MainWindow>();
        viewModel.NavigationService.NavigateTo<MainViewModel>();
    }
}
