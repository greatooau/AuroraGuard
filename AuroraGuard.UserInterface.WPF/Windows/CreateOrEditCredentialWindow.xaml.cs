using System.Windows;
using AuroraGuard.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AuroraGuard.UserInterface.WPF.Windows;

/// <summary>
/// Interaction logic for CreateCredentialWindow.xaml
/// </summary>
public partial class CreateOrEditCredentialWindow : Window, IClosableWindow
{
    private readonly IConfiguration _configuration;

    public CreateOrEditCredentialWindow(IConfiguration configuration)
    {
        _configuration = configuration;
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

}
