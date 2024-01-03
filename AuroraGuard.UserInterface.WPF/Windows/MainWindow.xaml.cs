using AuroraGuard.Core.Interfaces;
using AuroraGuard.UserInterface.WPF.Helpers;
namespace AuroraGuard.UserInterface.WPF.Windows;

public partial class MainWindow : IResizableWindow
{
	public MainWindow()
	{
		InitializeComponent();
	}

	public void MaximizeRestore() => this.DoMaximizeRestore();

	public void Minimize() => this.DoMinimize();
}