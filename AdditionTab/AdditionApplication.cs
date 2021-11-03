using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Windows;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.EditorInput;

[assembly:CommandClass(typeof(AdditionTab.AdditionApplication))]
namespace AdditionTab
{
    public class AdditionApplication : IExtensionApplication
    {
        Editor ed;
        public void Initialize()
        {
            RibbonControl control = ComponentManager.Ribbon;
            var tabs = control.Tabs;
            if(!tabs.Select(n=>n.Name).Contains("IronBIN"))
            {
                RibbonPanel panel = new RibbonPanel();
                panel.Source = new RibbonPanelSource() { Name = "Panel1"};
                RibbonTab tab = new RibbonTab() { Name = "IronBIN" };
                tab.Panels.Add(panel);
                tabs.Add(tab);
            }
            ed = Application.DocumentManager.MdiActiveDocument.Editor;
            for (int i = 0; i < 10; i++)
            {
                ed.WriteMessage($"{i},Auto Load IronBIN Tab");
            }
        }

        public void Terminate()
        {
            ed.WriteMessage("end");
        }
    }
}
