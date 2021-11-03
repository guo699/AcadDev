using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTest.Jig
{
    public class AutoRotateJig : DrawJig
    {
        public Line ClockLine { get; set; }
        public Point3d NewEndPoint { get; set; }
        public Point3d Start { get; set; }
        public int FrameCount { get; set; }
        public double Length { get; set; }
        public AutoRotateJig(Line line)
        {
            this.ClockLine = line;
            this.Start = line.StartPoint;
            this.Length = line.Length;
        }
        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            PromptPointResult prompt = prompts.AcquirePoint();
            if(prompt.Status == PromptStatus.OK)
            {
                FrameCount += 1;
                FrameCount %= 200;
            }
            return SamplerStatus.OK;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            Matrix3d m = Matrix3d.Rotation(FrameCount * Math.PI / 100, Vector3d.ZAxis, ClockLine.StartPoint);
            draw.Geometry.PushModelTransform(m);
            draw.Geometry.Draw(ClockLine);
            draw.Geometry.PopModelTransform();
            return true;
        }
    }
}
