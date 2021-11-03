using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadPlugInCommon.Core.Base
{
    public class TransactionUtils
    {
        private TransactionUtils() { }
        public static void Invoke(Document doc,Action action)
        {
            Database db = doc.Database;
            using(Transaction ts = db.TransactionManager.StartTransaction())
            {
                action.Invoke();
                ts.Commit();
            }
        }
    }
}
