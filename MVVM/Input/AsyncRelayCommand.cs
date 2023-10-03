using System.Windows.Input;

namespace MVVM.Input
{
    /// <summary>
    /// The async relay command delegates the execute and can execute tasks to the view model.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool>? _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute function.</param>
        /// <param name="canExecute">The can execute function.</param>
        public AsyncRelayCommand(Func<Task> execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// The method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        /// <returns>
        ///   <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
        /// </returns>
        public bool CanExecute(object? parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute();
            }

            return true;
        }

        /// <summary>
        /// The method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object? parameter)
        {
            Task executionTask = _execute();
            AwaitAndThrowIfFailed(executionTask);
        }

        /// <summary>
        /// Notifies that the can execute has changed.
        /// </summary>
        public void NotifyCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Awaits the and throw if failed.
        /// </summary>
        /// <param name="executionTask">The execution task.</param>
        internal static async void AwaitAndThrowIfFailed(Task executionTask)
        {
            await executionTask.ConfigureAwait(true);
        }
    }
}
