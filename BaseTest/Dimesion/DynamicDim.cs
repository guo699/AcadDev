using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.Runtime;

namespace BaseTest.Dimesion
{
    public class DynamicDim
    {
        [CommandMethod("DynamicDimText")]
        public void DynamicDimText()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptEntityResult entityResult = ed.GetEntity("\n拾取尺寸标注");
            if(entityResult.Status == PromptStatus.OK)
            {
                ObjectId id = entityResult.ObjectId;
                DBObject ent = this.GetEntityFormId(doc, id);
                Dimension dimension = ent as Dimension;
                if (dimension == null)
                    return;
                else
                {
                    Point3d origin = Point3d.Origin;
                    var result = ed.GetPoint("\n基点");
                    if(result.Status == PromptStatus.OK)
                    {
                        origin = result.Value;
                    }
                    var dimSty = this.GetEntityFormId(doc, dimension.DimensionStyle) as DimStyleTableRecord;
                    DimTextJig dimTextJig = new DimTextJig(dimSty,origin);
                }
            }
        }

        private DBObject GetEntityFormId(Document doc,ObjectId id,OpenMode openMode = OpenMode.ForRead)
        {
            DBObject obj = null;
            using(Transaction ts = doc.TransactionManager.StartTransaction())
            {
                obj = ts.GetObject(id, OpenMode.ForRead);
                ts.Commit();
            }
            return obj;
        }

        private void SetDimensionTxtSize(Document doc,Dimension dim,double textSize)
        {
            try
            {
                DimStyleTableRecord dimStyle;
                using (Transaction ts = doc.TransactionManager.StartTransaction())
                {
                    dimStyle = ts.GetObject(dim.DimensionStyle, OpenMode.ForWrite) as DimStyleTableRecord;
                    dimStyle.Dimtxt = textSize;
                    ts.Commit();
                }
            }
            catch { }
        }
    }

    public class DimTextJig : DrawJig
    {
        private DimStyleTableRecord _dim;
        private Point3d _targetPoint = Point3d.Origin;
        private Point3d _originPoint;
        public double Scale { get; set; }
        public DimTextJig(DimStyleTableRecord dim,Point3d origin)
        {
            _dim = dim;
            _originPoint = origin;
        }
        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            PromptPointResult promptPoint = prompts.AcquirePoint("\n缩放参考比例点");
            if(promptPoint.Status == PromptStatus.OK)
            {
                _targetPoint = promptPoint.Value;
                return SamplerStatus.OK;
            }
            return SamplerStatus.Cancel;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            WorldGeometry worldGeometry = draw.Geometry;
            Scale = _targetPoint.DistanceTo(_originPoint) / 100;

            return true;
        }
    }
}
