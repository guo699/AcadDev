using AcadPlugInCommon.Core.Base;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadPlugInCommon.Core
{
    public static partial class DocumentEx
    {
        public static void DeleteEntity(this Document doc,ObjectId id)
        {
            Database db = doc.Database;
            using(Transaction ts = db.TransactionManager.StartTransaction())
            {
                Entity ent = ts.GetObject(id, OpenMode.ForWrite) as Entity;
                ent.Erase();
                ts.Commit();
            }
        }

        public static void DeleteEntitis(this Document doc,ObjectIdCollection ids)
        {
            Database db = doc.Database;
            using (Transaction ts = db.TransactionManager.StartTransaction())
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    ObjectId id = ids[i];
                    Entity ent = ts.GetObject(id, OpenMode.ForWrite) as Entity;
                    ent.Erase();
                }
                ts.Commit();
            }
        }

        public static void DeleteEntitis(this Document doc, IEnumerable<ObjectId> ids)
        {
            Database db = doc.Database;
            using (Transaction ts = db.TransactionManager.StartTransaction())
            {
                foreach (var id in ids)
                {
                    Entity ent = ts.GetObject(id, OpenMode.ForWrite) as Entity;
                    ent.Erase();
                }
                ts.Commit();
            }
        }

    }
}
