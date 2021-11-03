using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using AcadPlugInCommon.Except;

namespace AcadPlugInCommon.Core
{
    public static partial class DocumentEx
    {
        public static void AddEntity(this Document doc,Entity ent,bool modelSpace = true)
        {
            Database db = doc.Database;
            string space = modelSpace ? BlockTableRecord.ModelSpace : BlockTableRecord.PaperSpace;
            using (Transaction ts = db.TransactionManager.StartTransaction())
            {
                BlockTable table = ts.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                BlockTableRecord record = ts.GetObject(table[space], OpenMode.ForWrite) as BlockTableRecord;
                record.AppendEntity(ent);
                ts.AddNewlyCreatedDBObject(ent, true);
                ts.Commit();
            }
        }

        public static void AddEntitis(this Document doc,params Entity[] ents)
        {
            Database db = doc.Database;
            using (Transaction ts = db.TransactionManager.StartTransaction())
            {
                BlockTable table = ts.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                BlockTableRecord record = ts.GetObject(table[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                foreach (var ent in ents)
                {
                    record.AppendEntity(ent);
                    ts.AddNewlyCreatedDBObject(ent, true);
                }
                ts.Commit();
            }
        }

        public static void AddEntitis(this Document doc, IEnumerable<Entity> ents)
        {
            Database db = doc.Database;
            using (Transaction ts = db.TransactionManager.StartTransaction())
            {
                BlockTable table = ts.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                BlockTableRecord record = ts.GetObject(table[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                foreach (var ent in ents)
                {
                    record.AppendEntity(ent);
                    ts.AddNewlyCreatedDBObject(ent, true);
                }
                ts.Commit();
            }
        }

        public static void AddLine(this Document doc,Point3d startPoint,Point3d endPoint)
        {
            Line line = new Line(startPoint, endPoint);
            AddEntity(doc, line, true);
        }

        public static void AddCircle(this Document doc,Point3d center,Vector3d normal,double raduis)
        {
            Circle circle = new Circle(center, normal, raduis);
            AddEntity(doc, circle, true);
        }

        public static void AddDBPoint(this Document doc,Point3d position)
        {
            DBPoint dBPoint = new DBPoint(position);
            AddEntity(doc, dBPoint, true);
        }

        public static void AddDBPoints(this Document doc,IEnumerable<Point3d> points)
        {
            Database db = doc.Database;
            DBPoint point;
            using (Transaction ts = db.TransactionManager.StartOpenCloseTransaction())
            {
                BlockTable table = ts.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                BlockTableRecord record = ts.GetObject(table[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                foreach (var pnt in points)
                {
                    point = new DBPoint(pnt);
                    record.AppendEntity(point);
                    ts.AddNewlyCreatedDBObject(point, true);
                }
                ts.Commit();
            }
        }

        /// <summary>
        /// 在模型空间中绘制点云并指定颜色
        /// </summary>
        /// <param name="doc">活动文档</param>
        /// <param name="points">点云坐标</param>
        /// <param name="colors">颜色索引</param>
        public static void AddDBPoints(this Document doc, IList<Point3d> points,IList<int> colors)
        {
            if (points.Count != colors.Count)
                throw new NumberMismatchException();
            Database db = doc.Database;
            DBPoint point;
            using (Transaction ts = db.TransactionManager.StartOpenCloseTransaction())
            {
                BlockTable table = ts.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                BlockTableRecord record = ts.GetObject(table[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                for (int i = 0; i < points.Count; i++)
                {
                    point = new DBPoint(points[i]);
                    point.ColorIndex = Math.Min(colors[i], 255);
                    record.AppendEntity(point);
                    ts.AddNewlyCreatedDBObject(point, true);
                }
                ts.Commit();
            }
        }

        public static void AddSpheres(this Document doc, IEnumerable<Point3d> points,double radius)
        {
            Database db = doc.Database;
            Solid3d solid;
            using (Transaction ts = db.TransactionManager.StartOpenCloseTransaction())
            {
                BlockTable table = ts.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                BlockTableRecord record = ts.GetObject(table[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                foreach (var pnt in points)
                {
                    solid = new Solid3d();
                    solid.CreateSphere(radius);
                    solid.TransformBy(Matrix3d.Displacement(pnt - Point3d.Origin));
                    record.AppendEntity(solid);
                    ts.AddNewlyCreatedDBObject(solid, true);
                }
                ts.Commit();
            }
        }

        public static void AddSphere(this Document doc,Point3d position,double radius)
        {
            Solid3d solid = new Solid3d();
            solid.CreateSphere(radius);
            solid.TransformBy(Matrix3d.Displacement(position - Point3d.Origin));
            AddEntity(doc, solid, true);
        }

        public static Entity GetEntityFromID(this Document doc,ObjectId id)
        {
            Entity ent = null;
            Database db = doc.Database;
            using(Transaction ts = db.TransactionManager.StartTransaction())
            {
                ent = ts.GetObject(id, OpenMode.ForRead) as Entity;
                ts.Commit();
            }
            return ent;
        }

        public static List<DBObject> GetDBObjectFromIDs(this Document doc,IEnumerable<ObjectId> ids)
        {
            List<DBObject> objs = new List<DBObject>();
            DBObject obj;
            Database db = doc.Database;
            using (Transaction ts = db.TransactionManager.StartTransaction())
            {
                foreach (var item in ids)
                {
                    obj = ts.GetObject(item, OpenMode.ForRead);
                    objs.Add(obj);
                }
                ts.Commit();
            }
            return objs;
        }

        public static List<Line> ConnectPoints(this Document doc,List<Point3d> points)
        {
            List<Line> lines = new List<Line>();
            if (points.Count == 0)
                return lines;
            for (int i = 0; i < points.Count-1; i++)
            {
                lines.Add(new Line(points[i],points[i+1]));
            }
            lines.Add(new Line(points.Last(), points.First()));
            return lines;
        }
    }
}
