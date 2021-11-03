using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;

namespace PlugInManager
{
    class MainWinVM:ViewModelBase
    {
        private string _dllPath;

        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand RunCommand { get; set; }

        public MainWinVM()
        {
            LoadCommand = new DelegateCommand(LoadDll);
            RunCommand = new DelegateCommand(RunMethod);
        }

        private void LoadDll(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            bool? rslt = dialog.ShowDialog();
            if(rslt == true)
            {
                _dllPath = dialog.FileName;
                string dllName = Path.GetFileName(_dllPath);

                byte[] matedata = File.ReadAllBytes(_dllPath);                
                
                Assembly dll = Assembly.Load(matedata);

                var attrs = dll.GetCustomAttributes<CommandClassAttribute>();
            }
        }

        private void RunMethod(object obj)
        {
        }
    }
}
