using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace RentCafeTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Rootobject jsonData = download_serialized_json_data<Rootobject>("https://www.rentnema.com/rent-cafe-api.php?type=0");
            System.Console.WriteLine("JSON DATA: " + jsonData.units[0].dateAvailable);
        }

        private static T download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                    w.DownloadFile(url, "data.json");
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }
    }
}
