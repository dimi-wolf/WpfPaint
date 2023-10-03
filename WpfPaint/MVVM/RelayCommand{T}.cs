using System;
using System.Windows.Input;

namespace WpfPaint.MVVM
{
    /// <summary>
    /// The relay command delegates the execute and can execute actions to the view model.
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter.</typeparam>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class RelayCommand<TParameter> : ICommand
    {
        private readonly Action<TParameter?> _execute;
        private readonly Predicate<TParameter?>? _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute action.</param>
        /// <param name="canExecute">The function that determines an action can be executed.</param>
        public RelayCommand(Action<TParameter?> execute, Predicate<TParameter?>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Occurs when the execution possibility of the execute action has changed.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns>
        /// <c>true</c> if this command can be executed; otherwise <c>false</c>.
        /// </returns>
        public bool CanExecute(object? parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute((TParameter?)parameter);
            }

            return true;
        }

        /// <summary>
        /// The method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public void Execute(object? parameter)
        {
            _execute((TParameter?)parameter);
        }

        /// <summary>
        /// Notifies that the can execute has changed.
        /// </summary>
        public void NotifyCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
