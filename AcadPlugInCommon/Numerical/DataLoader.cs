using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadPlugInCommon.Numerical
{
    public class DataLoader
    {
        private string _filePath;
        /// <summary>
        /// 文件完成路径
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public DataLoader() { }

        public DataLoader(string path)
        {
            this._filePath = path;
        }

        /// <summary>
        /// 读取CSV文件
        /// </summary>
        /// <param name="sep">单个数据之间的分隔符</param>
        /// <returns>数据表</returns>
        public DataTable ReadCsv(char sep = ',')
        {
            DataTable table = new DataTable("csv");
            using (StreamReader stream = new StreamReader(_filePath))
            {
                DataRow row;
                bool first = true;
                while (!stream.EndOfStream)
                {
                    if (first)
                    {
                        string[] headers = stream.ReadLine().Split(sep);
                        for (int i = 0; i < headers.Length; i++)
                            table.Columns.Add(((char)(i + 65)).ToString(), typeof(object));
                        first = !first;
                        continue;
                    }
                    row = table.NewRow();
                    row.ItemArray = ConvertToRowData(stream.ReadLine());
                    table.Rows.Add(row);
                }
            }
            return table;

            object[] ConvertToRowData(string line)
            {
                string[] cells = line.Split(sep);
                object[] rowData = new object[cells.Length];
                for (int i = 0; i < cells.Length; i++)
                {
                    if (double.TryParse(cells[i], out double num))
                        rowData[i] = num;
                    else
                        rowData[i] = "null";
                }
                return rowData;
            }
        }
    }
}
