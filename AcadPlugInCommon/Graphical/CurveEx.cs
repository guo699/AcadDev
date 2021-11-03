using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadPlugInCommon.Core;

namespace AcadPlugInCommon.Graphical
{
    public static partial class CurveEx
    {
        public static bool IsEndPoint(this Curve curve,Point3d point3d)
        {
            return curve.StartPoint.IsAlmostEqual(point3d) || curve.EndPoint.IsAlmostEqual(point3d);
        }

        public static Vector3d AsVector(this Line line)
        {
            return line.EndPoint - line.StartPoint;
        }

        public static bool IsParallel(this Line first,Line second)
        {
            return first.AsVector().IsParallelTo(second.AsVector());
        }

        public static double GetDistance(this Line first,Line second)
        {
            return 0;
        }

        public static bool IsCoplanar(this Curve first,Curve second)
        {
            return false;
        }
    }
}
