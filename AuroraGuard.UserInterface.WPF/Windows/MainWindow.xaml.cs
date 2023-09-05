using AuroraGuard.ViewModels.Windows;

namespace AuroraGuard.UserInterface.WPF.Windows;

public partial class MainWindow
{
	
	public MainWindow(MainWindowViewModel mainWindowViewModel)
	{
		InitializeComponent();
		DataContext = mainWindowViewModel;
	}
}