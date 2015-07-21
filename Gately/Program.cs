using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using Gately.DAL;
using Gately.LL;
using Gately.LL.WebReq;
using Gately.Model;

namespace Gately
{
    class Program
    {
        private string db = @"data source=C:\Users\MHUEY\Desktop\rg\database";
        private string dataFolder = @"C:\Users\MHUEY\Desktop\rg\data\"; 

        static void Main(string[] args)
        {
            ////******
            //new Program().createTables(); //creates tables

            ////******
            //var userFollowers = new LL.GetFollowers.GetFollowers(@"C:\Users\MHUEY\Desktop\rg\data\"); //gets followers and saves for a user
            //userFollowers.get(new User
            //    {
            //        key = "Heide_Castaneda"
            //    });

            //******
            new LL.GetMembers.GetMembers().get(); //get members of organization

            ////******
            //new LL.GetOrganizations.GetOrganizations().get();

            Console.ReadLine();
        }

 

        private void createTables()
        {
            var b = new _database().createTables(db);
            Console.WriteLine("Tables created: " + b);
        }
    }
}
