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
            byte[] matedata = File.ReadAllBytes(@"D:\Code\CSharp\AcadDev\ViewMove\bin\Debug\ViewMove.dll");
            Assembly dll = Assembly.Load(matedata);
            Type type = dll.GetType("ViewMove.DllMain");
            object instance = dll.CreateInstance("ViewMove.DllMain");
            type.GetMethod("ViewRotate").Invoke(instance, null);
        }
    }
}
