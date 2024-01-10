using System.Windows;
using AuroraGuard.Core.Interfaces;

namespace AuroraGuard.UserInterface.WPF.Windows;
/// <summary>
/// Interaction logic for AuthWindow.xaml
/// </summary>
public partial class AuthWindow : Window, IClosableWindow, IShowDialog
{
    public AuthWindow()
    {
        InitializeComponent();
    }
}
