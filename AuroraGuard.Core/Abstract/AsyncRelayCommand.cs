using System.Windows.Input;

namespace AuroraGuard.Core.Abstract;

/// <summary>
/// 
/// </summary>
/// <param name="execute">The delegate to execute when the command is invoked</param>
/// <param name="canExecute">The delegate to check if the command can be executed</param>
public sealed class AsyncRelayCommand(Func<object?, CancellationToken, Task> execute, Func<object?, bool>? canExecute = null)
    : ICommand
{
    // The cancellation token source to cancel the operation
    private CancellationTokenSource? _cancellationTokenSource;

    // The event to notify when the command state changes
    public event EventHandler? CanExecuteChanged;

    // The property to indicate if the command can be canceled
    public bool CanBeCanceled => _cancellationTokenSource is not null;

    /// <summary>
    /// Indicates if the command is running
    /// </summary>
    public bool IsRunning => _cancellationTokenSource is {IsCancellationRequested: false};

    // The property to get the current execution task
    public Task? ExecutionTask { get; private set; }

    // The method to check if the command can be executed
    public bool CanExecute(object? parameter) => !IsRunning && (canExecute is null || canExecute(parameter));

    // The method to execute the command asynchronously
    public async void Execute(object? parameter)
    {
        // Create a new cancellation token source
        _cancellationTokenSource = new CancellationTokenSource();

        // Raise the event to notify the command state change
        OnCanExecuteChanged();

        try
        {
            // Invoke the delegate and store the task
            ExecutionTask = execute(parameter, _cancellationTokenSource.Token);

            // Await the task to complete
            await ExecutionTask;
        }
        catch (OperationCanceledException)
        {
            // Handle the cancellation exception
        }
        finally
        {
            // Dispose the cancellation token source
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;

            // Raise the event to notify the command state change
            OnCanExecuteChanged();
        }
    }

    // The method to cancel the command execution
    public void Cancel()
    {
        if (CanBeCanceled) _cancellationTokenSource?.Cancel();
    }

    // The method to raise the event
    public void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
