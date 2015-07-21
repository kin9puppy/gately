using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gately
{
    public class Utility
    {
        public string safeGet(dynamic dyn)
        {
            return (dyn != null) ? dyn.ToString() : "";
        }
    }
}
