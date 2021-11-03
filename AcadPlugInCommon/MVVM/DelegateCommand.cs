using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AcadPlugInCommon.MVVM
{
    public class DelegateCommand : ICommand
    {
        Action<object> _action;
        public DelegateCommand(Action<object> action)
        {
            this._action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke(parameter);
        }
    }
}
