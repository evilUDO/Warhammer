using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Viewmodel
{
    class UserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<Object> action;

        public UserCommand(Action<Object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                action(parameter);
            }
            else
            {
                action("Error");
            }
        }
    }
}
