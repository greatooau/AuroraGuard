using System;
using System.Windows;
using System.Windows.Controls;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Models;
using AuroraGuard.UserInterface.ViewModels.Main;
using AuroraGuard.UserInterface.WPF.Windows;
using Microsoft.Win32;

namespace AuroraGuard.UserInterface.WPF.Views.Main;
/// <summary>
/// Interaction logic for MainView.xaml
/// </summary>
public partial class MainView : UserControl, IHandleCredentialCreation
{
    public MainView()
    {
        InitializeComponent();
    }

    public Credential? CreateCredential(ICredentialRepository credentialRepository)
    {
        var mainWindow = Application.Current.MainWindow!;
        CreateCredentialWindowViewModel viewModel = new(credentialRepository);
        
        var createCredentialWindow = new CreateCredentialWindow
        {
            Owner = mainWindow,
            DataContext = viewModel
        };

        createCredentialWindow.ShowDialog();

        return viewModel.CreatedCredential;
    }
}
