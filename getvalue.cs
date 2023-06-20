using System;
using System.Collections.Generic;

class Program
{
    static string GetKey(Dictionary<string, object> obj)
    {
        List<string> keys = new List<string>(obj.Keys);
        if (keys.Count != 1)
            throw new Exception("Empty dictionary found.");
        else
            return keys[0];
    }

 // This function will return the object depending on the search result. 
    static object GetValue(Dictionary<string, object> obj, string key, bool isFound = false)
    {
        if (!(obj is Dictionary<string, object>) && !isFound)
            return null;

        if (isFound || obj.ContainsKey(key))
        {
            if (obj[key] is Dictionary<string, object>)
                return GetValue((Dictionary<string, object>)obj[key], GetKey((Dictionary<string, object>)obj[key]), true);
            else
                return obj[GetKey(obj)];
        }
        else
        {
            string nestedKey = GetKey(obj);
            return GetValue((Dictionary<string, object>)obj[nestedKey], key, false);
        }
    }

    static void Main(string[] args)
    {
        Dictionary<string, object> obj = new Dictionary<string, object>
        {
            { "a", new Dictionary<string, object>
                {
                    { "b", new Dictionary<string, object>
                        {
                            { "c", "d" }
                        }
                    }
                }
            }
        };

        object value = GetValue(obj, "a");
        Console.WriteLine(value);
    }
}
