using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gately.Model
{
    class User
    {
        public string key { get; set; }
        public List<dynamic> following { get; set; }
    }
}
