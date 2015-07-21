using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gately.Model;

namespace Gately.LL.GetFollowers
{
    class GetFollowers
    {
        private string dataFolder { get; set; }

        public GetFollowers(string _dataFolder)
        {
            dataFolder = _dataFolder;
        }

        public bool get(User user) {
            try
            {
                var worker = new lib.worker(dataFolder);
                worker.saveUser(user);

                return true;
            }
            catch (Exception e) {
                Console.WriteLine("failed to get user:" + e);
                return false;
            }
        }



    }
}
