using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PointCloudShow.Viewmodel
{
    class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> _action;
        public DelegateCommand(Action<object> action)
        {
            this._action = action;
        }

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
