using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using AcadPlugInCommon.Core;
using System.Timers;

namespace BaseTest.Animation
{
    public class TimerAnimation
    {
        Line line;
        int i = 0;
        Timer timer;
        Editor ed;

        [CommandMethod("TimerAnim")]
        public void TimerAnim()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            ed = doc.Editor;
            Database db = doc.Database;
            timer = new Timer(200);
            timer.Elapsed += Update_line;

            PromptEntityResult entityResult = ed.GetEntity("\n拾取直线");
            if(entityResult.Status == PromptStatus.OK)
            {
                ed.WriteMessage("Start");
                //line = doc.GetEntityFromID(entityResult.ObjectId) as Line;
                if(line != null)
                {
                    timer.Start();
                    doc.ModifyColor(entityResult.ObjectId, i);
                }
            }
        }

        private void Update_line(object sender, ElapsedEventArgs e)
        {
            i++;
            if (i == 100)
                timer.Close();
            ed.WriteMessage("\n Hello");
        }
    }
}
