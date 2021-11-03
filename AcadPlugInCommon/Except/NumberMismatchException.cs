using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadPlugInCommon.Except
{
    public class NumberMismatchException:Exception
    {
        public override string Message => "元素个数不匹配";
    }
}
