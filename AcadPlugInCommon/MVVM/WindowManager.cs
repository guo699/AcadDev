using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Autodesk.AutoCAD.ComponentModel;
using Autodesk.Windows;

namespace AcadPlugInCommon.MVVM
{
    public static class WindowManager
    {
        public static void ShowOnHost(this Window window)
        {
            SetHost(window);
            window.Show();
        }

        public static void ShowDialogOnHost(this Window window)
        {
            SetHost(window);
            window.ShowDialog();
        }

        static void SetHost(Window window)
        {
            IntPtr hostIntPrt = ComponentManager.ApplicationWindow;
            WindowInteropHelper helper = new WindowInteropHelper(window);
            helper.Owner = hostIntPrt;
        }
    }
}
