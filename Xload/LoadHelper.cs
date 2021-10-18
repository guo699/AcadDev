using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Autodesk.AutoCAD.Runtime;
using System.IO;

namespace Xload
{
    public class LoadHelper
    {
        [CommandMethod("Xload")]
        public void Xload()
        {
            byte[] matedata = File.ReadAllBytes(@"D:\Code\CSharp\AcadDev\BaseTest\bin\Debug\BaseTest.dll");
            Assembly dll = Assembly.Load(matedata);
            Type type = dll.GetType("BaseTest.Dimesion.DynamicDim");
            object instance = dll.CreateInstance("BaseTest.Dimesion.DynamicDim");
            type.GetMethod("DynamicDimText").Invoke(instance, null);
        }
    }
}
