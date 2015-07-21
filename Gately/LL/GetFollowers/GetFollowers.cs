using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gately.Model;
using System.IO;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace Gately.LL.GetFollowers
{
    class GetFollowers
    {
        private Stopwatch stopwatch = new Stopwatch();
        private string orgFolder = @"C:\Users\MHUEY\Desktop\rg\members\";
        private string dataFolder = @"C:\Users\MHUEY\Desktop\rg\data\"; 

        public GetFollowers()
        { 
        }

        public bool get() {
            //"California_Institute_of_Technology", 
            var California = new string[] {  "Stanford_University", "University_of_California_Berkeley", "University_of_California_Davis", "University_of_California_Irvine",
                "University_of_California_Los_Angeles", "University_of_California_Merced", "University_of_California_Riverside", "University_of_California_San_Diego2", "University_of_California_San_Francisco", 
                "University_of_California_Santa_Barbara", "University_of_California_Santa_Cruz","University_of_Southern_California","California_State_University_Long_Beach","California_State_University_Los_Angeles",
                "California_State_University_Northridge","San_Jose_State_University","San_Diego_State_University", "University_of_San_Francisco", "Sonoma_State_University", "Chapman_University", "Harvey_Mudd_College", 
                "Occidental_College", "Santa_Clara_University", "California_Polytechnic_State_University_San_Luis_Obispo", "California_State_Polytechnic_University_Pomona", "The_Scripps_Research_Institute" };



            try
            {
                var userCount   = 0;
                var worker      = new lib.worker(dataFolder);
                var ser         =  new JavaScriptSerializer();
                dynamic user;


                foreach (var orgKey in California)
                {
                    var line = "";
                    var count = 0;
                    stopwatch.Start();
                    System.IO.StreamReader file = new System.IO.StreamReader(orgFolder + orgKey);
                    while ((line = file.ReadLine()) != null)
                    {
                        try
                        {
                            user = ser.Deserialize<User>(line);
                            worker.saveUser(user);
                            count++;
                            userCount++;
                            Console.WriteLine(user.name + " added [" + user.org + "]"); 
                            Console.WriteLine(count + " contacts in " + stopwatch.ElapsedMilliseconds / 1000 + " sec");
                            if (userCount == 1000) {
                                userCount = 0;
                            }
                        }
                        catch { }
                    }
                    stopwatch.Stop();
                    file.Close();
                    Console.WriteLine(Environment.NewLine + orgKey + " completed" + Environment.NewLine);
                }



                

                return true;
            }
            catch (Exception e) {
                Console.WriteLine("failed to get user:" + e);
                return false;
            }
        }



    }
}
