using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFTest.Command
{
    class RunCommandWithCheck : ICommand
    {

        // This whole class is used to call Functions Bound to buittons with checks to see if the button can be clicked

        // Eg 1 Before adding a new account check that the accno does not exist then if it doesnt enable button and allow the adding of a new account
        // Eg 1 Before adding a new account check that the name is longer than 2 characters then if it is enable button and allow the updating of a the account

        // The Checks are in the form of a Function that returns a boolean and the updatye is a method that executes

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;


        public RunCommandWithCheck(Action<object> execute, Func<object, bool> canExecute)
        {

            this._canExecute = canExecute;
            this._execute = execute;

        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
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
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }


        #endregion


    }
}
