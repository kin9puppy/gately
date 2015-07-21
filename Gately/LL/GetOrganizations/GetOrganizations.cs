using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gately.LL.GetOrganizations
{
    class GetOrganizations
    {
        public bool get()
        {
            new lib.worker().get();
            return true;
        }
    }
}
