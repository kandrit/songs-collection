using System.Windows.Input;

namespace SongsCollection.UI.Commands;

public class Command : ICommand
{
    private readonly Action<object?> executeAction;
    private readonly Func<object?, bool> canExecuteFunction;


    public Command(Action<object?> executeAction, Func<object?, bool> canExecuteFunction)
    {
        this.executeAction = executeAction;
        this.canExecuteFunction = canExecuteFunction;
    }

    public bool CanExecute(object? parameter) => this.canExecuteFunction(parameter);

    public void Execute(object? parameter) => this.executeAction(parameter);

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}
