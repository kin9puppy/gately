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

namespace Gately.LL.GetFollowers.lib
{
    class worker
    {

        private string dataFolder { get; set; }

        public worker(string _dataFolder) {
            dataFolder = _dataFolder;
        }

        public void saveUser(User user)
        {
            user.following = getAllFollowing(user.key);
            if (user.following != null) {
                Console.WriteLine(user.following.Count + " contacts");
                File.AppendAllText(dataFolder + "_total", user.following.Count.ToString() + Environment.NewLine);
            }
            var text = new JavaScriptSerializer().Serialize(user);
            File.WriteAllText(dataFolder + user.key, text);
        }

       

        private List<dynamic> parseFollowing(string text, ref bool added)
        {
            var f = new LL.GetFollowers.lib.Following();
            return f.get(text, ref added);
        }


        private List<dynamic> getAllFollowing(string userKey)
        {
            var increments = new string[] { "0", "25", "50", "75", "100", "125" };
            string json = "";
            var list = new List<dynamic>();
            bool added = false;

            foreach (var i in increments)
            {
                json = getFollowing(userKey, i);
                list.AddRange(parseFollowing(json, ref added));


                if (added == false)
                { //if parsed all of followers before reaching 150 
                    break;
                }
            }

            return list;

        }

        private string getFollowing(string userKey, string increment)
        {

            var headers = new string[] { "Accept-Language:en-US,en;q=0.8" };
            var url = @"https://www.researchgate.net/publicprofile.ProfileFollowingWidget.html";

            var parameters = string.Join("&", new string[]{ 
                "view=dialog", 
                "enableUnfollow=1", 
                "showFollowButton=1", 
                "account_key=" + userKey, 
                "appendItemsAfterFollowed=1", 
                "limit=100",
                "offset=" + increment
            });

            url += "?" + parameters;

            return new Request().get(url, headers);
        }

    }
}
