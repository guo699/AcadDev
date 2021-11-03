using AcadPlugInCommon.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataVisualization.ViewModel
{
    public class FigureSettingVM:ViewModelBase
    {
        public ObservableCollection<int> Dimensions { get; set; }
        public ObservableCollection<int> Colors { get; set; }
        public int SelectX { get; set; }
        public int SelectY { get; set; }
        public int SelectZ { get; set; }
        public int SelectC { get; set; }

        public DelegateCommand OKCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public FigureSettingVM(int dims)
        {
            Dimensions = new ObservableCollection<int>(Enumerable.Range(0,dims));
            Colors = new ObservableCollection<int>(Enumerable.Range(1,128));
            OKCommand = new DelegateCommand(this.Btn_Ok_Click);
            CancelCommand = new DelegateCommand(Btn_Cancel_Click);

            SelectX = 0;
            SelectY = 1;
            SelectZ = 2;
            SelectC = 1;
        }

        private void Btn_Ok_Click(object obj)
        {
            (obj as Window).Close();
        }

        private void Btn_Cancel_Click(object obj)
        {
            (obj as Window).Close();
        }
    }
}
