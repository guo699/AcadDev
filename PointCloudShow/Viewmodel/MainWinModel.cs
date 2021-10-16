using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PointCloudShow.Viewmodel
{
    class MainWinModel
    {
        public DelegateCommand ImportCommand { get; set; }

        public MainWinModel()
        {
            ImportCommand = new DelegateCommand(this.ImportData);
        }

        private void ImportData(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }
    }
}
