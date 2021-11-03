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
            string path = @"D:\Code\CSharp\AcadDev\BaseTest\bin\Debug\BaseTest.dll";
            //byte[] matedata = File.ReadAllBytes(path);
            //Assembly dll = Assembly.Load(matedata);
            Assembly dll = Assembly.LoadFrom(path);
            Type type = dll.GetType("BaseTest.DllMain");
            object instance = dll.CreateInstance("BaseTest.DllMain");
            type.GetMethod("CircleJig").Invoke(instance, null);
        }
    }
}
