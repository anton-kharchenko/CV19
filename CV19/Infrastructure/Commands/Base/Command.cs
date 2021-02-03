using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CV19.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
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
