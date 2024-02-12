using AuroraGuard.Core.Enum;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.UserInterface.WPF.Helpers;
namespace AuroraGuard.UserInterface.WPF.Windows;

public partial class MainWindow : IResizableWindow, IShowDialog
{
	public MainWindow()
    {
		InitializeComponent();
	}

	public WindowCurrentState MaximizeRestore() => this.DoMaximizeRestore();

	public void Minimize() => this.DoMinimize();
}