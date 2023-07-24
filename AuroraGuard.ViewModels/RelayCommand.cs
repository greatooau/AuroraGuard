using System.Diagnostics;
using System.Windows.Input;

namespace AuroraGuard.ViewModels;

public class RelayCommand : ICommand
{
	private readonly Action<object> _execute;
	private readonly Predicate<object?>? _canExecute;

	public RelayCommand(Action<object> execute, Predicate<object?>? canExecute = null)
	{
		_execute = execute;
		_canExecute = canExecute;
	}
	
	[DebuggerStepThrough]
	public bool CanExecute(object? parameter)
	{
		return _canExecute == null || _canExecute(parameter);
	}

	public void Execute(object? parameter)
	{
		_execute(parameter!);
	}

	public event EventHandler? CanExecuteChanged;
}
