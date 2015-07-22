using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Gately.LL.GetMembers
{
    class GetMembers
    {

        private string rgFolder = @"C:\Users\MHUEY\Desktop\rg\";

        public bool get() {

            //var California = new string[] {  "California_Institute_of_Technology", "Stanford_University", "University_of_California_Berkeley", "University_of_California_Davis", "University_of_California_Irvine",
            //    "University_of_California_Los_Angeles", "University_of_California_Merced", "University_of_California_Riverside", "University_of_California_San_Diego2", "University_of_California_San_Francisco", 
            //    "University_of_California_Santa_Barbara", "University_of_California_Santa_Cruz","University_of_Southern_California","California_State_University_Long_Beach","California_State_University_Los_Angeles",
            //    "California_State_University_Northridge","San_Jose_State_University","SanC:\Users\Michael\Desktop\apps\gately\Gately\LL\GetMembers\GetMembers.cs_Diego_State_University", "University_of_San_Francisco", "Sonoma_State_University", "Chapman_University", "Harvey_Mudd_College", 
            //    "Occidental_College", "Santa_Clara_University", "California_Polytechnic_State_University_San_Luis_Obispo", "California_State_Polytechnic_University_Pomona", "The_Scripps_Research_Institute" };


            var line = "";
            var count = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(rgFolder + "top500.txt");
            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    if (new lib.worker().get(line.Trim()))
                    {
                        Console.WriteLine(line + " completed.");
                        Console.WriteLine(count + " orgs completed");
                    }
                    else
                    {
                        Console.WriteLine(line + " may have written but had some complication.");
                    }
                }
                catch { }
            }
            file.Close();

             
            return true;
        }
    }
}
