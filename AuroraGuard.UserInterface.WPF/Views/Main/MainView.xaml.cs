using System.Windows;
using System.Windows.Controls;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Models;
using AuroraGuard.IoC;
using AuroraGuard.UserInterface.ViewModels.Main;
using AuroraGuard.UserInterface.WPF.Windows;

namespace AuroraGuard.UserInterface.WPF.Views.Main;
/// <summary>
/// Interaction logic for MainView.xaml
/// </summary>
public partial class MainView : UserControl, IHandleCredentialCreationEdition
{

    public MainView()
    {
        InitializeComponent();
    }

    public Credential? CreateCredential(ViewModel viewModel)
    {
        var mainWindow = Application.Current.MainWindow!;

        var createCredentialWindow = new CreateOrEditCredentialWindow(AuroraGuardConfiguration.Get())
        {
            Owner = mainWindow,
            DataContext = viewModel
        };

        createCredentialWindow.ShowDialog();

        return ((CreateEditCredentialWindowViewModel)viewModel).CreatedCredential;
    }

    public bool EditCredential(ViewModel viewModel)
    {
        var mainWindow = Application.Current.MainWindow!;
        
        var editCredentialWindow = new CreateOrEditCredentialWindow(AuroraGuardConfiguration.Get())
        {
            Owner = mainWindow,
            DataContext = viewModel
        };

        return editCredentialWindow.ShowDialog() == true;
    }

}
