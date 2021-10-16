using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System.Windows;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace PointCloudShow
{
    public class DllMain
    {
        [CommandMethod("ShowPointCloud")]
        public void ShowPointCloud()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            List<Point3d> points = this.ReadPointFile(out List<int> labels);

            try
            {
                using (Transaction ts = db.TransactionManager.StartTransaction())
                {
                    BlockTable table = ts.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                    BlockTableRecord record = ts.GetObject(table[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    for (int i = 0; i < points.Count; i++)
                    {
                        DBPoint point = new DBPoint(points[i]);
                        point.ColorIndex = labels[i];
                        record.AppendEntity(point);
                        ts.AddNewlyCreatedDBObject(point, true);
                    }
                    ts.Commit();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [CommandMethod("ShowPointUsingBoll")]
        public void ShowPointUsingBoll()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            double raduis = 10;
            PromptDoubleResult reslt = ed.GetDouble("\n球体半径");
            if (reslt.Status == PromptStatus.OK)
            {
                raduis = reslt.Value;
            }

            List<Point3d> points = this.ReadPointFile(out List<int> labels);
            Solid3d boll;

            try
            {
                using (Transaction ts = db.TransactionManager.StartTransaction())
                {
                    BlockTable table = ts.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                    BlockTableRecord record = ts.GetObject(table[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    for (int i = 0; i < points.Count; i++)
                    {
                        boll = this.CreateSolid(points[i],raduis,labels[i]);
                        record.AppendEntity(boll);
                        ts.AddNewlyCreatedDBObject(boll, true);
                    }
                    ts.Commit();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<Point3d> ReadPointFile(out List<int> labels)
        {
            List<Point3d> points = new List<Point3d>();
            labels = new List<int>();
            Point3d point;
            int label;
            string path = @"C:\Users\IronBin\Desktop\data.csv";
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string row = reader.ReadLine();
                        string[] cells = row.Split(',');
                        double x = Convert.ToDouble(cells[0]);
                        double y = Convert.ToDouble(cells[1]);
                        double z = Convert.ToDouble(cells[2]);
                        point = new Point3d(x, y, z);
                        points.Add(point);
                        label = Convert.ToInt32(cells[3]);
                        labels.Add(label);
                    }
                }
            }
            catch { }

            return points;
        }

        [CommandMethod("CreateBoll")]
        public void CreateBoll()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            try
            {
                using (Transaction ts = db.TransactionManager.StartTransaction())
                {
                    BlockTable table = ts.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                    BlockTableRecord record = ts.GetObject(table[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    Solid3d boll = this.CreateSolid(new Point3d(0,0,0),10,1);
                    record.AppendEntity(boll);
                    ts.AddNewlyCreatedDBObject(boll, true);
                    ts.Commit();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Solid3d CreateSolid(Point3d point3d,double raduis,int colorIndex)
        {
            Solid3d solid3d = new Solid3d();
            solid3d.CreateSphere(raduis);
            solid3d.ColorIndex = colorIndex;
            solid3d.TransformBy(Matrix3d.Displacement(point3d.GetAsVector()));
            return solid3d;
        }
    }
}
