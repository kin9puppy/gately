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
        public string name { get; set; }
        public string org { get; set; }
        public string url { get; set; }
        public string orgUrl { get; set; }
        public string imgUrl { get; set; }
        public List<dynamic> following { get; set; } 
    }
}
