using System.Collections.Generic;
using System.Data;
using System.Windows;
using AcadPlugInCommon.MVVM;
using AcadPlugInCommon.Numerical;
using AcadPlugInCommon.Core;
using Autodesk.AutoCAD.ApplicationServices;
using DataVisualization.Views;
using Microsoft.Win32;
using Autodesk.AutoCAD.Geometry;

namespace DataVisualization.ViewModel
{
    class MainVM
    {
        public DelegateCommand ImportDataCommand { get; set; }
        public DelegateCommand ShowDataCommand { get; set; }
        public DelegateCommand SettingCommand { get; set; }
        public DelegateCommand InfomationCommand { get; set; }
        public MainUM UM { get; set; }

        Document _doc;
        DataLoader _dataLoader;
        List<int> _selectItems;
        public MainVM(Document doc, MainUM um)
        {
            this._doc = doc;
            this.UM = um;
            this._dataLoader = new DataLoader();

            ImportDataCommand = new DelegateCommand(this.ImportData);
            ShowDataCommand = new DelegateCommand(this.ShowData);
            SettingCommand = new DelegateCommand(this.Setting);
            InfomationCommand = new DelegateCommand(this.ShowInfo);

            _selectItems = new List<int>() { 0, 1, 2, 1 };
        }

        private void ShowInfo(object obj)
        {
            MessageBox.Show("Test 001");
        }

        private void Setting(object obj)
        {
            if (UM.DataSource == null || UM.DataSource.Rows.Count == 0 || UM.DataSource.Columns.Count == 0)
                return;
            FigureSettingVM vm = new FigureSettingVM(UM.DataSource.Columns.Count);
            FigureSetting settingWin = new FigureSetting();
            settingWin.DataContext = vm;
            settingWin.Owner = obj as Window;
            bool? result = settingWin.ShowDialog();
            _selectItems.Clear();
            _selectItems.Add(vm.SelectX);
            _selectItems.Add(vm.SelectY);
            _selectItems.Add(vm.SelectZ);
            _selectItems.Add(vm.SelectC);
        }

        private void ShowData(object obj)
        {
            if (UM.DataSource == null || UM.DataSource.Rows.Count == 0 || UM.DataSource.Columns.Count == 0)
                return;
            List<Point3d> points = new List<Point3d>();
            List<int> colors = new List<int>();
            double x, y, z;
            int c;
            foreach (DataRow row in UM.DataSource.Rows)
            {
                x = ObjToDouble(row[_selectItems[0]]);
                y = ObjToDouble(row[_selectItems[1]]);
                z = ObjToDouble(row[_selectItems[2]]);
                c = (int)ObjToDouble(row[_selectItems[3]]);
                points.Add(new Point3d(x, y, z));
                colors.Add(c);
            }
            _doc.AddDBPoints(points,colors);
            (obj as Window).Close();
        }

        private void ImportData(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                UM.FilePath = dialog.FileName;
                _dataLoader.FilePath = dialog.FileName;
                UM.DataSource = _dataLoader.ReadCsv();
            }
        }

        double ObjToDouble(object obj)
        {
            if (double.TryParse(obj.ToString(), out double value))
                return value;
            else
                return 0.0;
        }
    }
}
