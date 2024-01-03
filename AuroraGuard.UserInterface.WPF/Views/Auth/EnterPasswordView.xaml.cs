using System.Security;
using System.Windows;
using System.Windows.Controls;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.UserInterface.ViewModels.Auth;
using AuroraGuard.UserInterface.ViewModels.Main;
using AuroraGuard.UserInterface.WPF.Windows;

namespace AuroraGuard.UserInterface.WPF.Views.Auth;
/// <summary>
/// Interaction logic for EnterPasswordPage.xaml
/// </summary>
public partial class EnterPasswordView : UserControl, IPasswordContainer, IHandleWindowNavigation
{

    public EnterPasswordView ThisView1
    {
        get => ThisView;
        set => ThisView = value;
    }

    public EnterPasswordView()
    {
        InitializeComponent();
    }


    public SecureString SecurePassword => Password.SecurePassword;
    
    public void Dispose()
    {
        SecurePassword.Dispose();
    }

    private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is not EnterPasswordViewModel viewModel)
            return;
        
        viewModel.OnCanRequestAccessChanged();
    }

    public void Navigate()
    {
        if (DataContext is not EnterPasswordViewModel viewModel)
            return;
        
        viewModel.NavigationService.NavigateTo<AuthWindow, MainWindow>();
        viewModel.NavigationService.NavigateTo<MainViewModel>();
    }
}
