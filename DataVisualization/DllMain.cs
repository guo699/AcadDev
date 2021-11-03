using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using DataVisualization.ViewModel;
using DataVisualization.Views;
using AcadPlugInCommon.MVVM;
using Exception = Autodesk.AutoCAD.Runtime.Exception;
using System.Windows;

namespace DataVisualization
{
    public class DllMain
    {
        [CommandMethod("ShowData")]
        public void ShowData()
        {
            try
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
                MainUM um = new MainUM();
                MainVM vm = new MainVM(doc,um);
                MainWindow win = new MainWindow();
                win.DataContext = vm;
                win.ShowDialogOnHost();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
