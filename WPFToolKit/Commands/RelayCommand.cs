﻿namespace WPFToolKit.Commands
{


    public class RelayCommand<T> : CommandBase
    {
        private readonly Action<T?> _execute;
        private readonly Func<T?, bool> _canExecute;

        public RelayCommand(Action<T?> execute, Func<T?, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public override bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter is null ?  default : (T)parameter);
        }

        public override void Execute(object? parameter)
        {
            _execute(parameter is null ? default : (T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}
