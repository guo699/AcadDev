using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD;
using Autodesk.Windows;

namespace AcadPlugInCommon.RibbonUI
{
    class RibbonCreator
    {
        public RibbonControl Ribbon { get; set; }
        public Dictionary<string,RibbonTab> UserTabs { get; set; }
        public RibbonCreator()
        {
            this.Ribbon = ComponentManager.Ribbon;
        }

        public void AddTab(string tabName)
        {
            RibbonTab tab = new RibbonTab();
            tab.Title = tabName;
            Ribbon.Tabs.Add(tab);
        }

        public void AddPanel(RibbonTab targetTab,string panelName)
        {
            RibbonPanel panel = new RibbonPanel();
            RibbonPanelSource source = new RibbonPanelSource();
            source.Name = panelName;
            panel.Source = source;
            targetTab.Panels.Add(panel);
        }

    }
}
