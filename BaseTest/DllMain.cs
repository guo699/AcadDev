using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using BaseTest.Jig;

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
