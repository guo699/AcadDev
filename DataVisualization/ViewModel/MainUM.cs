using AcadPlugInCommon.MVVM;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualization.ViewModel
{
    class MainUM: ViewModelBase
    {
        private string _fileName;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; base.OnPropertyChanged(nameof(FileName)); }
        }

        private string _filePath;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            set 
            { 
                _filePath = value;
                _fileName = Path.GetFileNameWithoutExtension(value);
                base.OnPropertyChanged(nameof(FilePath));
                base.OnPropertyChanged(nameof(FileName));
            }
        }

        private DataTable _dataSource;
        /// <summary>
        /// 表格数据
        /// </summary>
        public DataTable DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; base.OnPropertyChanged(nameof(DataSource)); }
        }

    }
}
