using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;

namespace Gately.LL.GetFollowers.lib
{
    public class Following
    {
        public List<dynamic> get(string text, ref bool added)
        { 
            try
            {
                return extract(text, ref added);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Whole parse failed: " + ex.ToString());
                added = false;
                return new List<dynamic>();
            }
        }


        private List<dynamic> extract(string text, ref bool added)
        {
            var eCount = 0;
            var count = 0;
            var x = new JavaScriptSerializer().Deserialize<dynamic>(text);
            dynamic dyn;
            var n = x["result"];


            if (n.GetType().Namespace != "System")
            {
                var m           = n["data"]["content"]["data"]["listItems"];
                var list        = new List<dynamic>();

                foreach (var item in m)
                {
                    try
                    {
                        dyn = parse(item);
                        count++;
                        list.Add(dyn);
                    }
                    catch (Exception ex)
                    {
                        eCount++;
                    }
                }

                Console.WriteLine("New contacts:" + count);
                added = (count > 0);
                return list;
            }
            else
            {
                Console.WriteLine("end");
                added = false;
            }

            return new List<dynamic>();
        }


        private dynamic parse(dynamic item)
        {
            var data = item["data"];
            return new
            {
                rId = item["id"].ToString(),
                name = data["displayName"].ToString(),
                key = data["accountKey"].ToString(),
                org = data["professionalInstitutionName"].ToString(),
                orgUrl = data["professionalInstitutionUrl"].ToString().Replace(@"\/", @"/"),
                url = data["url"].ToString().Replace(@"\/", @"/"),
                imgUrl = data["imageUrl"].ToString().Replace(@"\/", @"/")
            };
        }

    }
}
