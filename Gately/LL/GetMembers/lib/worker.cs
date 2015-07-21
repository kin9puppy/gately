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
using System.Diagnostics;

namespace Gately.LL.GetMembers.lib
{
    class worker
    {
        private string orgFolder = @"C:\Users\MHUEY\Desktop\rg\members\";
        private Stopwatch stopwatch = new Stopwatch();

        public bool get(string orgKey){
              
            try
            {  
                var list = getMembers(orgKey); 
                
                return true;
            }
            catch (Exception ex){
                Console.WriteLine(orgKey + "failed");
                File.AppendAllText(orgFolder + "_working.log", orgKey + "failed" + ex.ToString() + Environment.NewLine);
                return false;
            }   

        }

        private void writeOrgCount(string orgKey, string count){
            File.AppendAllText(orgFolder + "_totals.csv", orgKey + "," + count  + "," + DateTime.Now + Environment.NewLine);
        }

        private bool writeJSON(string orgKey, dynamic dyn) {
            var text = new JavaScriptSerializer().Serialize(dyn);
            File.AppendAllText(orgFolder + orgKey, text + Environment.NewLine);
            return true; 
        }

        private bool writeCSV(string orgKey, dynamic dyn){
            try
           {
                var line = "";
                var filename = orgFolder + orgKey + DateTime.Now.ToShortDateString().Replace(@"/", "-") + ".csv"; 

                    line += "\"" + dyn.name + "\",";
                    line += "\"" + dyn.key + "\",";
                    line += "\"" + dyn.org + "\","; 

                    line += Environment.NewLine; 
                File.AppendAllText(filename, line);
                return true;
            }
            catch {
                return false; //swallow fail
            }
        }

        private List<dynamic> getMembers(string orgKey)
        {
            
            bool added              = false;
            var list                = new List<dynamic>();
            string page;

            stopwatch.Start();


            for (var i = 1; i < 20000; i++){ //max org size 20k, so 210 is more than enough 210*100
                page = i.ToString();
                var text = getMembersRequest(orgKey, page);
                parse(orgKey, text, ref added);

                Console.WriteLine(orgKey + " to " + i * 100 + " members"); //log progress
                Console.WriteLine(i + " requests in " + stopwatch.ElapsedMilliseconds / 1000 + " sec");

                if (!added)
                {
                    writeOrgCount(orgKey, (i * 100).ToString());
                    break;
                }
            }

            stopwatch.Stop(); 
            return list;
        }



        private List<dynamic> parse(string orgKey, string text, ref bool added)
        {
            try
            {
                added = false;
                var count = 0;
                var u = new Utility();
                dynamic dyn;
                var x = new JavaScriptSerializer().Deserialize<dynamic>(text);
                var people = x["result"]["data"]["content"]["data"]["peopleListItems"];

                dynamic data;
                foreach (var p in people)
                {
                    try
                    {
                        count++;
                        data = p["data"];
                        dyn = new
                        {
                            name = u.safeGet(data["displayName"]),
                            key = u.safeGet(data["accountKey"]),
                            org = u.safeGet(data["professionalInstitutionName"]),
                            orgUrl = u.safeGet(data["professionalInstitutionUrl"]).Replace(@"\/", @"/"),
                            url = u.safeGet(data["url"]).Replace(@"\/", @"/"),
                            imgUrl = u.safeGet(data["imageUrl"]).Replace(@"\/", @"/")
                        };
                        writeJSON(orgKey, dyn);
                        writeCSV(orgKey, dyn);
                    }
                    catch { }
                }

                if (count > 0)
                {
                    added = true;
                }

                return null;
            }
            catch (Exception ex)
            {
                added = false;
                return null;
            }
        }

        private string safeGet(dynamic dyn)
        {
            return (dyn != null) ? dyn.ToString() : "";
        }


        private string getMembersRequest(string orgKey, string page)
        {

            var headers = new string[] { "Accept-Language:en-US,en;q=0.8" };
            var url = @"http://www.researchgate.net/publicinstitutions.FacilityMembersContent.html";

            var parameters = string.Join("&", new string[]{ 
                "institutionKey=" + orgKey,
                "page=" + page
            });

            url += "?" + parameters;

            return new Request().get(url, headers);
        }
    }
}
