using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;

namespace BaseTest.Jig
{
    public class RectJig : DrawJig
    {
        public Line Line1 { get; set; } = new Line();
        public Line Line2 { get; set; } = new Line();
        public Line Line3 { get; set; } = new Line();
        public Line Line4 { get; set; } = new Line();
        private Point3d _leftTopPoint;
        private Point3d _rightBottomPoint;
        public RectJig(Point3d leftTop)
        {
            this._leftTopPoint = leftTop;
        }
        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            PromptPointResult result = prompts.AcquirePoint("\n输入矩形的右下角点");
            if (result.Status == PromptStatus.OK)
            {
                _rightBottomPoint = result.Value;
                return SamplerStatus.OK;
            }
            else
                return SamplerStatus.Cancel;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            Line1.StartPoint = _leftTopPoint;
            Line1.EndPoint = new Point3d(_rightBottomPoint.X, _leftTopPoint.Y, 0);

            Line2.StartPoint = new Point3d(_rightBottomPoint.X, _leftTopPoint.Y, 0);
            Line2.EndPoint = _rightBottomPoint;

            Line3.StartPoint = _rightBottomPoint;
            Line3.EndPoint = new Point3d(_leftTopPoint.X, _rightBottomPoint.Y, 0);

            Line4.StartPoint = new Point3d(_leftTopPoint.X, _rightBottomPoint.Y, 0);
            Line4.EndPoint = _leftTopPoint;

            draw.Geometry.Draw(Line1);
            draw.Geometry.Draw(Line2);
            draw.Geometry.Draw(Line3);
            draw.Geometry.Draw(Line4);

            return true;
        }
    }
}
