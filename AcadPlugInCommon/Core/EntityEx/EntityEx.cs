using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadPlugInCommon.Core
{
    public static class EntityEx
    {
        public static void ModifyColor(this Document doc,ObjectId objectId,int colorIndex)
        {
            Database db = doc.Database;
            using (Transaction ts = db.TransactionManager.StartTransaction())
            {
                Entity ent = ts.GetObject(objectId, OpenMode.ForWrite) as Entity;
                ent.ColorIndex = colorIndex;
                ts.Commit();
            }
        }

        public static List<Entity> RingArray(this Entity ent,Point3d basePoint,int num)
        {
            List<Entity> result = new List<Entity>() { ent };
            for (int i = 1; i < num; i++)
            {
                result.Add(ent.GetTransformedCopy(Matrix3d.Rotation(i * (2*Math.PI / num), Vector3d.ZAxis, basePoint)));
            }
            return result;
        }
    }
}
