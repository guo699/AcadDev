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
    public static partial class DocumentEx
    {
        public static void RotateEnitity(this Document doc,Entity ent,Point3d center,double angle,Vector3d axis)
        {
            
        }

        public static void MidifyColor(this Document doc,Entity ent,int colorIdx)
        {
            using(Transaction ts = doc.TransactionManager.StartTransaction())
            {
                ent.ColorIndex = colorIdx;
                ts.Commit();
            }
        }
    }
}
