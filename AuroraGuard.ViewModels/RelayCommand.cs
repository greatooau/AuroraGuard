using System.Windows.Input;

namespace AuroraGuard.ViewModels;

public class RelayCommand : ICommand
{
	private readonly Action<object?> _execute;
	private readonly Func<object?, bool>? _canExecute;
	public RelayCommand(Func<object?, bool>? canExecute, Action<object?> execute)
	{
		_canExecute = canExecute;
		_execute = execute;
	}

	public bool CanExecute(object? parameter) => _canExecute is null || _canExecute(parameter);

	public void Execute(object? parameter)
	{
		_execute(parameter);
	}

	public event EventHandler? CanExecuteChanged;
}
