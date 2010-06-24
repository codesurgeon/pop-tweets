using System;
using System.Net;
using System.Web;
using System.IO;
using Procurios.Public;
using System.Collections.Generic;

namespace com.codesurgeon.sample.dotnet4dynamic
{
    class Driver
    {
        static void Main(string[] args)
        {
            string searchTerm = "codesurgeon";
            var search = new PopularTweets();
            search.Search(searchTerm);

            Console.WriteLine("Real-Time Search for " + 
                searchTerm + "yields the following results:\n");
            IList<Tweet> tweets = search.tweets;
            
            //showcasing dynamic dispatch for dyn obj Tweet
            foreach(dynamic tweet in tweets)
            {
                Console.WriteLine("Tweet: " + tweet.text);
                Console.WriteLine("By: " + tweet.from_user + "\n");
            }
        }
    }
}
