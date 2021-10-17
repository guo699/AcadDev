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
    public class MoveJig : DrawJig
    {
        private List<Entity> _entitis;
        private Point3d _targetPoint = Point3d.Origin;
        private Point3d _originPoint;
        public Matrix3d M { get; set; }
        public MoveJig(List<Entity> entitis,Point3d origin)
        {
            this._entitis = entitis;
            this._originPoint = origin;
        }
        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            PromptPointResult pointResult = prompts.AcquirePoint("\n目标点");
            if (pointResult.Status == PromptStatus.OK)
            {
                _targetPoint = pointResult.Value;
                return SamplerStatus.OK;
            }
            else
                return SamplerStatus.Cancel;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            M = Matrix3d.Displacement(_targetPoint - _originPoint);
            draw.Geometry.PushModelTransform(M);
            foreach (var item in _entitis)
            {
                draw.Geometry.Draw(item);
            }
            draw.Geometry.PopModelTransform();
            return true;
        }
    }
}
