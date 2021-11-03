using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadPlugInCommon.Core;

namespace AcadPlugInCommon.Graphical
{
    public static class Point3dEx
    {
        const double DistanceError = 1e-5;
        public static bool IsAlmostEqualTo(this Point3d first,Point3d second)
        {
            return first.DistanceTo(second) < DistanceError;
        }

        public static bool IsSameZ(this Point3d first,Point3d second)
        {
            return first.Z.IsAlmostEqual(second.Z);
        }

        /// <summary>
        /// 指定起点和终点，判断目标点是否在左侧
        /// </summary>
        /// <param name="start">起点</param>
        /// <param name="end">终点</param>
        /// <param name="target">目标点</param>
        /// <returns>在起点和终点连线上返回NULL</returns>
        public static bool? IsOnLeft(this Point3d start,Point3d end,Point3d target)
        {
            double s = start.X * end.Y + target.X * start.Y + end.X * target.Y - target.X * end.Y - end.X * start.Y - start.X * target.Y;
            if (s.IsAlmostEqual(0.0))
                return null;
            else if (s > 0)
                return true;
            else
                return false;
        }
    }
}
