using System.Windows.Input;

namespace AuroraGuard.Core.Abstract;

public class RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    : ICommand
{
    public bool CanExecute(object? parameter) => canExecute is null || canExecute(parameter);

    public void Execute(object? parameter) => execute(parameter);

    public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public event EventHandler? CanExecuteChanged;
}
