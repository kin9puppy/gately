using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace Gately.LL.WebReq
{
    class Request
    {
        public string get(string url, string[] headers){
             
            try {

                string contentText = "";
                 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                 
			    WebHeaderCollection headersCollection = request.Headers;

                request.Accept = @"application/json";

                foreach (var header in headers) {
                    headersCollection.Add(header); 
                }
                 
		 	    
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();


                using (var reader = new StreamReader(response.GetResponseStream()))
                { 
                    contentText = reader.ReadToEnd(); 
                }

			    //Print the headers for the request.
			    //printHeaders(myWebHeaderCollection);
			    response.Close();
                return contentText;
		    }
		    //Catch exception if trying to add a restricted header. 
		    catch(ArgumentException e) {
			    Console.WriteLine(e.Message);
                return null;
		    } 

               
        }

    }
}
