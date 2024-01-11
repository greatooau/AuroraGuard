using System.Windows;
using AuroraGuard.Core.Interfaces;

namespace AuroraGuard.UserInterface.WPF.Windows;
/// <summary>
/// Interaction logic for CreateCredentialWindow.xaml
/// </summary>
public partial class CreateOrEditCredentialWindow : Window, IClosableWindow
{
    public CreateOrEditCredentialWindow()
    {
        InitializeComponent();
    }
}
