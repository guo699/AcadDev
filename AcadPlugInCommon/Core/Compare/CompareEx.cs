using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadPlugInCommon.Core
{
    public static class CompareEx
    {
        const double ValueError = 1e-5;
        const double DistanceError = 1e-9;
        const double AngleError = 1e-6;
        public static bool IsGreaterThan(this double first,double second)
        {
            return first - second > ValueError;
        }

        public static bool IsLessThen(this double first,double second)
        {
            return second - first > ValueError;
        }

        public static bool IsAlmostEqual(this double first,double second)
        {
            return Math.Abs(first - second) < ValueError;
        }

        public static bool IsAlmostEqual(this Point3d first,Point3d second)
        {
            return first.DistanceTo(second) < DistanceError;
        }

        public static bool IsParallel(this Vector3d first,Vector3d second)
        {
            return first.GetAngleTo(second) < AngleError;
        }
    }
}
