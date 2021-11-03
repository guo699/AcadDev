using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using BaseTest.Jig;
using AcadPlugInCommon.Core;
using AcadPlugInCommon.Graphical;
using System.Threading;
using System.Windows.Forms;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

[assembly: CommandClass(typeof(BaseTest.DllMain))]
namespace BaseTest
{
    public class DllMain
    {
        [CommandMethod("JigTest")]
        public void JigTest()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            List<Entity> ents = new List<Entity>();

            PromptSelectionOptions options = new PromptSelectionOptions();
            options.AllowDuplicates = false;
            options.MessageForAdding = "\n拾取实体";
            options.MessageForRemoval = "\n筛出实体";
            PromptSelectionResult selectionResult = ed.GetSelection(options);
            if(selectionResult.Status == PromptStatus.OK)
            {
                SelectionSet selectionSet = selectionResult.Value;
                ents = this.GetEntitisFromIds(doc, selectionSet.GetObjectIds());

                PromptPointResult pointResult = ed.GetPoint("\n基点");
                if (pointResult.Status == PromptStatus.OK)
                {
                    Point3d origin = pointResult.Value;
                    MoveJig moveJig = new MoveJig(ents,origin);
                    PromptResult result = ed.Drag(moveJig);
                    if(result.Status == PromptStatus.OK)
                    {
                        this.MoveEntitis(doc, moveJig.M, ents.Select(n=>n.Id).ToArray());
                    }
                }
            }
        }

        [CommandMethod("MoveTest")]
        public void MoveTest()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            List<Entity> ents = new List<Entity>();

            PromptSelectionOptions options = new PromptSelectionOptions();
            options.AllowDuplicates = false;
            options.MessageForAdding = "\n拾取实体";
            options.MessageForRemoval = "\n筛出实体";
            PromptSelectionResult selectionResult = ed.GetSelection(options);
            if (selectionResult.Status == PromptStatus.OK)
            {
                SelectionSet selectionSet = selectionResult.Value;
                ents = this.GetEntitisFromIds(doc, selectionSet.GetObjectIds());
                Matrix3d m = Matrix3d.Displacement((new Point3d(100, 100, 0)).GetVectorTo(Point3d.Origin));
                this.MoveEntitis(doc, m, ents.Select(n=>n.Id).ToArray());
            }
        }

        [CommandMethod("CircleJig")]
        public void CircleJig()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            Database db = doc.Database;

            BubbleJig bubbleJig = new BubbleJig();
            ed.Drag(bubbleJig);
        }

        [CommandMethod("AutoAnimation")]
        public void AutoAnimation()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            Database db = doc.Database;

            PromptEntityResult promptEntity = ed.GetEntity("\n拾取直线");
            if(promptEntity.Status == PromptStatus.OK)
            {
                Line line = doc.GetEntityFromID(promptEntity.ObjectId) as Line;
                if (line == null)
                    return;
                else
                {
                    AutoRotateJig jig = new AutoRotateJig(line);
                    PromptResult result = ed.Drag(jig);
                    if (result.Status == PromptStatus.Cancel)
                        return;
                }
            }
        }

        [CommandMethod("TuBao")]
        public void TuBao()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptSelectionResult selectionResult = ed.GetSelection();
            if(selectionResult.Status == PromptStatus.OK)
            {
                SelectionSet set = selectionResult.Value;
                IEnumerable<DBPoint> points = doc.GetDBObjectFromIDs(set.GetObjectIds()).OfType<DBPoint>();
                Graham graham = new Graham(points.Select(n => n.Position));
                List<Point3d> result = graham.GetSerialPoint();
                List<Line> lines = doc.ConnectPoints(result);
                doc.AddEntitis(lines);
            }
        }

        [CommandMethod("DongHua")]
        public void DongHua()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            var ent = ed.GetEntity("");
            if(ent.Status == PromptStatus.OK)
            {
                var c = doc.GetEntityFromID(ent.ObjectId) as Circle;

                using (Transaction ts = db.TransactionManager.StartTransaction())
                {
                    BlockTable table = ts.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                    BlockTableRecord record = ts.GetObject(table[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    for (int i = 0; i < 300; i++)
                    {
                        //c.Radius = i + 10;
                        //record.AppendEntity(c);
                        //ts.AddNewlyCreatedDBObject(c,true);
                        (ts.GetObject(c.Id, OpenMode.ForWrite) as Circle).Radius = i + 100;
                        c.Draw();
                        //UpdateEntity();
                        Application.UpdateScreen();
                        Thread.Sleep(15);
                    }

                    ts.Commit();
                }
            }

        }

        private void UpdateEntity()
        {
            Application.UpdateScreen();
            System.Windows.Forms.Application.DoEvents();
        }

        private void AddEntity(Document doc,params Entity[] entities)
        {
            using(Transaction ts = doc.TransactionManager.StartTransaction())
            {
                BlockTable table = ts.GetObject(doc.Database.BlockTableId, OpenMode.ForWrite) as BlockTable;
                BlockTableRecord record = ts.GetObject(table[BlockTableRecord.ModelSpace],OpenMode.ForWrite) as BlockTableRecord;
                foreach (var item in entities)
                {
                    record.AppendEntity(item);
                    ts.AddNewlyCreatedDBObject(item, true);
                }
                ts.Commit();
            }
        }

        private void MoveEntitis(Document doc,Matrix3d m,params ObjectId[] ids)
        {
            using (Transaction ts = doc.TransactionManager.StartTransaction())
            {
                Entity ent;
                for (int i = 0; i < ids.Length; i++)
                {
                    ent = ts.GetObject(ids[i],OpenMode.ForWrite) as Entity;
                    ent.TransformBy(m);
                }
                ts.Commit();
            }
        }

        private List<Entity> GetEntitisFromIds(Document doc,params ObjectId[] ids)
        {
            List<Entity> entities = new List<Entity>();

            Entity ent = null;
            using(Transaction ts = doc.TransactionManager.StartTransaction())
            {
                foreach (var id in ids)
                {
                    ent = ts.GetObject(id, OpenMode.ForWrite) as Entity;
                    entities.Add(ent);
                }
            }

            return entities;
        }
    }
}
