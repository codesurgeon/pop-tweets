using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace com.codesurgeon.sample.dotnet4dynamic
{
    class Tweet : DynamicObject
    {
        private IDictionary<string, string> stringValues = 
            new Dictionary<string, string>();
        private IDictionary<string, double> numberValues =
            new Dictionary<string, double>();

        public Tweet(Hashtable rawTweet) 
        {
            FlattenJSON(rawTweet);
        }

        //flatten input and assign to correct map/dictionary based on JSON value type
        private void FlattenJSON(Hashtable table)
        {
            foreach (DictionaryEntry entry in table)
            {
                //this code is not handling the case, that the Tweet contains a JSON array
                //not necessary as of now: this code is intended for demo purposes in a talk
                //I'm giving on Friday 2010-06-25
                if (entry.Value is String)
                    stringValues.Add((string)entry.Key, (string)entry.Value);
                else if (entry.Value is Double)
                    numberValues.Add((string)entry.Key, (double)entry.Value);
                else if (entry.Value is Hashtable)
                    FlattenJSON((Hashtable)entry.Value);
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (stringValues.ContainsKey(binder.Name))
                result = stringValues[binder.Name];
            else result = numberValues[binder.Name];
            return true;
        }
    }
}
