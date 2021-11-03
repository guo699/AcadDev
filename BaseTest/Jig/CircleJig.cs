using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using AcadPlugInCommon.Core;

namespace BaseTest.Jig
{
    public class CircleJig : DrawJig
    {
        public Point3d BasePoint;
        public Point3d TargetPoint;
        public List<Line> Lines;
        public CircleJig(Point3d basePoint,int num)
        {
            this.BasePoint = basePoint;
            Line line = new Line(TargetPoint, new Point3d(TargetPoint.X + 100, TargetPoint.Y, TargetPoint.Z));
            this.Lines = line.RingArray(basePoint, num).Cast<Line>().ToList();
        }
        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            PromptPointResult promptPoint = prompts.AcquirePoint();
            if(promptPoint.Status == PromptStatus.OK)
            {
                TargetPoint = promptPoint.Value;
                return SamplerStatus.OK;
            }
            return SamplerStatus.Cancel;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            foreach (var item in Lines)
            {
                item.StartPoint = TargetPoint;
                draw.Geometry.Draw(item);
            }
            return true;
        }
    }
}
