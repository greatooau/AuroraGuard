using System;
using System.Windows;
using System.Windows.Controls;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Models;
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

    public Credential? CreateCredential(ICredentialRepository credentialRepository)
    {
        var mainWindow = Application.Current.MainWindow!;
        CreateEditCredentialWindowViewModel viewModel = new(credentialRepository);
        
        var createCredentialWindow = new CreateOrEditCredentialWindow
        {
            Owner = mainWindow,
            DataContext = viewModel
        };

        createCredentialWindow.ShowDialog();

        return viewModel.CreatedCredential;
    }

    public void EditCredential(ICredentialRepository credentialRepository, Credential credential)
    {
        var mainWindow = Application.Current.MainWindow!;
        
        CreateEditCredentialWindowViewModel viewModel = new(credentialRepository)
        {
            Id = credential.Id,
            AppName = credential.AppName,
            Notes = credential.Notes,
            Username = credential.AccessUser,
            Password = credential.AccessPassword,
            ImagePath = credential.ImagePath,
            IsEdition = true,
            OriginalCredential = credential
        };

        var editCredentialWindow = new CreateOrEditCredentialWindow
        {
            Owner = mainWindow,
            DataContext = viewModel
        };

        editCredentialWindow.ShowDialog();
    }
}
