using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointCloudShow.View;
using PointCloudShow.Viewmodel;

namespace PointCloudShow
{
    class TestAppMain
    {
        [STAThread]
        public static void Main()
        {
            MainWindow win = new MainWindow();
            MainWinModel vm = new MainWinModel();
            win.DataContext = vm;
            win.ShowDialog();
        }
    }
}
