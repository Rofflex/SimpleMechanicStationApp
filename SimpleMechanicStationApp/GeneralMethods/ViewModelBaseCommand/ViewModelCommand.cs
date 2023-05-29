using System;
using System.Windows.Input;

namespace SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand
{
    public class ViewModelCommand<T> : ICommand
    {
        // Fields
        private readonly Action<T> _executeAction;
        private readonly Predicate<T>? _canExecuteAction;

        // Constructors
        public ViewModelCommand(Action<T> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }

        public ViewModelCommand(Action<T> executeAction, Predicate<T> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        // Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Methods
        public bool CanExecute(object parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction((T)parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction((T)parameter);
        }
    }
}
