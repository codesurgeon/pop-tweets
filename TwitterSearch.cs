using System;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Web;
using System.IO;
using Procurios.Public;

namespace com.codesurgeon.sample.dotnet4dynamic
{
    class PopularTweets
    {
        private string baseSearchUrl;
        private string jsonResultsKey = "results";
        private IList<Tweet> searchResult = new List<Tweet>();

        public IList<Tweet> tweets { get { return searchResult; } }
       
        //default value for parameter: new C# 4.0 language feature
        public PopularTweets(string baseSearchUrl = 
            "http://search.twitter.com/search.json?q=") 
        {
            this.baseSearchUrl = baseSearchUrl;
        }

        public void Search(string query)
        {
            string url = baseSearchUrl + query;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //Console.WriteLine("Requesting: " + url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(responseStream);
            string responseString = reader.ReadToEnd();

            var jsonDecoded = JSON.JsonDecode(responseString);

            IList rawTweets = 
                (IList)((Hashtable)jsonDecoded)[jsonResultsKey];

            foreach (var rawTweet in rawTweets) 
            {
                searchResult.Add(new Tweet((Hashtable) rawTweet));
            }
            
            //Console.WriteLine(responseString);

            response.Close();
        }
    }
}
