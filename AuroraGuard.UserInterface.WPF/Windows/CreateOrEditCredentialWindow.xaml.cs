using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AuroraGuard.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;

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
