using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugInManager
{
    class MainWinVM:ViewModelBase
    {
        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand RunCommand { get; set; }

        public MainWinVM()
        {
            LoadCommand = new DelegateCommand(LoadDll);
            RunCommand = new DelegateCommand(RunMethod);
        }

        private void LoadDll(object obj)
        {
            throw new NotImplementedException();
        }

        private void RunMethod(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
