using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.GraphicsSystem;
using System.Windows;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Threading;

namespace ViewMove
{
    public class DllMain
    {
        [CommandMethod("ViewMove")]
        public void ViewMove()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            try
            {
                Point3d center = Point3d.Origin;
                PromptPointResult pointRst = ed.GetPoint("\n视图中点");
                if(pointRst.Status == PromptStatus.OK)
                {
                    center = pointRst.Value;
                }
                ViewTableRecord viewTableRecord = ed.GetCurrentView();
                using(Transaction ts = db.TransactionManager.StartTransaction())
                {
                    viewTableRecord.CenterPoint = new Point2d(center.X, center.Y);

                    ed.SetCurrentView(viewTableRecord);
                    ts.Commit();
                }
                ed.UpdateScreen();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        [CommandMethod("ViewRotate")]
        public void ViewRotate()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    ed.WriteMessage(string.Format("{0} times Hello\n", i));
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }


}
