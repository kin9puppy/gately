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

namespace Gately.LL.GetOrganizations.lib
{
    class worker
    {
        private string orglistFolder = @"C:\Users\MHUEY\Desktop\rg\orglist\";

        public bool get() {

            for (var i = 0; i < 20000; i += 100) //gets the major players and minor and most micro
            {
                var text = getOrganizationRequest(i.ToString());  
                bool added = parse(text);

                if (added == false) {
                    Console.WriteLine("Ended organization get. ~" + i + " entries.");
                    break;
                }
                Console.WriteLine("to " + i + " added.");
            }
            return true; 
        }


        private bool parse(string text) {
            try
            {
                string line = "";
                var n = new JavaScriptSerializer().Deserialize<dynamic>(text);
                var hasItems = n["result"]["data"]["hasItems"];
                if (hasItems)
                {
                     
                    var items = n["result"]["data"]["items"];
                    var u = new Utility();

                    foreach (var item in items)
                    {
                        var data = item["data"];

                         line = "";
                         line += "\"" + u.safeGet(data["countryName"]) + "\",";
                         line += "\"" + u.safeGet(data["institutionKey"]) + "\",";
                         line += "\"" + u.safeGet(data["institutionName"]) + "\",";
                         line += "\"" + u.safeGet(data["rank"]) + "\",";
                         line += "\"" + u.safeGet(data["impact"]) + "\","; 

                        File.AppendAllText(orglistFolder + "_list.txt", u.safeGet(data["institutionKey"]) + Environment.NewLine);
                        File.AppendAllText(orglistFolder + "_orgs.csv", line + Environment.NewLine);
                    }

                    return true;
                }
                else {
                    return false; //no more items to get, end process.
                }
            }
            catch { 
            }

            return true;
        }


        private string getOrganizationRequest(string offset)
        {

            var headers = new string[] { "Accept-Language:en-US,en;q=0.8" };
            var url = @"http://www.researchgate.net/publicinstitutions.TopInstitutionsRankingList.html?order=rgScore&method=total&offset=" + 
                offset + "&dbw=true&order=rgScore&method=total";
  

            return new Request().get(url, headers);
        }

    }
}
