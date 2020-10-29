using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFTest.Command
{
    public class RelayCommand : ICommand
    {

        // This whole class is used for the PersonViewModel Class

        private readonly Action<object>  _execute;
        private readonly Func<object, bool> _canExecute;


        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {

            this._canExecute = canExecute;
            this._execute = execute;

        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null )
            {
                return true;
            }
            else
            {
                return _canExecute(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add {CommandManager.RequerySuggested += value;}
            remove {CommandManager.RequerySuggested -= value;}
        }

        public void Execute(object parameter)
        {
            _execute(parameter)  ;
        }


    }
}
