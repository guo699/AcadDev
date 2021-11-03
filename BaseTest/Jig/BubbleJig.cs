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
    public class BubbleJig : DrawJig
    {
        public Point3d Position;
        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            PromptPointResult promptPoint = prompts.AcquirePoint();
            if(promptPoint.Status == PromptStatus.OK)
            {
                Position = promptPoint.Value;
                return SamplerStatus.OK;
            }
            return SamplerStatus.Cancel;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            draw.Geometry.Circle(Position, 100, Vector3d.ZAxis);
            draw.Geometry.Circle(Position, 200, Vector3d.ZAxis);
            draw.Geometry.Circle(Position, 300, Vector3d.ZAxis);
            draw.Geometry.Circle(Position, 400, Vector3d.ZAxis);
            return true;
        }
    }
}
