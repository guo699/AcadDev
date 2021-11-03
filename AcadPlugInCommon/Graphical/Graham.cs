using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadPlugInCommon.Graphical
{
    /// <summary>
    /// Graham 凸包生成算法
    /// </summary>
    public class Graham
    {
        private IEnumerable<Point3d> _points;
        private Point3d _origin => this._getMinYpoint();
        public Graham(IEnumerable<Point3d> points)
        {
            this._points = points;
        }

        public List<Point3d> GetSerialPoint()
        {
            if (_points.Count() <= 3)
                return _points.ToList();

            List<Point3d> result = new List<Point3d>() { _origin };
            List<Point3d> source = this._sortByAngle();
            result.Add(source.First());

            Point3d start, end;
            bool? state;

            foreach (var item in source.Skip(1))
            {
                while(true)
                {
                    start = result[result.Count - 2];
                    end = result[result.Count - 1];
                    state = start.IsOnLeft(end, item);
                    if (state == true || result.Count<2)
                    {
                        result.Add(item);
                        break;
                    }
                    else if(state == false)
                        result.Remove(end);
                }
            }

            return result;
        }

        private Point3d _getMinYpoint()
        {
            Point3d bottom = _points.FirstOrDefault();
            double miny = double.MaxValue;
            foreach (var pnt in _points)
            {
                if (pnt.Y <= miny)
                {
                    bottom = pnt;
                    miny = bottom.Y;
                }
            }
            return bottom;
        }

        private List<Point3d> _sortByAngle()
        {
            return _points.Where(n=>n!=_origin).OrderBy(n => (n - _origin).GetAngleTo(Vector3d.XAxis)).ToList();
        }
    }
}
