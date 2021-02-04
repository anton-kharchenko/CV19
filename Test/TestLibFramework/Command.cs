using System;
using System.Windows.Input;

namespace TestLibFramework
{
    public abstract class Command : ICommand
    {
        /// <summary>
        /// Говорит можно ли выполнить команду
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract bool CanExecute(object parameter);

        /// <summary>
        /// То что должна делать команда
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
