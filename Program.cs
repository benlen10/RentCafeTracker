using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace RentCafeTracker
{
    public class Program
    {
        // Constant Values
        private const string API_URL = "https://www.rentnema.com/rent-cafe-api.php?type=0";
        private const string DATABASE_FILENAME = "database.xml";
        private const string JSON_DIRECTORY_NAME = "json-data-files/";


        static void Main(string[] args)
        {
            // string stringData = DownloadJsonData(API_URL);
            string stringData = ReadJsonFileToString("data.json");
            Rootobject jsonData = SeralizeJsonData<Rootobject>(stringData);
            //Console.WriteLine("JSON DATA: " + jsonData.units[0].dateAvailable);

            // Import existing database
            Database.Initalize();
            if (LoadDatabase())
            {
                Console.WriteLine("Database loaded successfully");
            }
            else
            {
                Console.WriteLine("Failed to load database");
            }

            // Create a new date object and import units data
            Date newDate = new Date();
            newDate.ImportUnitData(jsonData.units);
            Console.WriteLine("Read data for the following number of units:" + jsonData.units.Length.ToString());

            // Load new date entry into database
            Database.AddDate(newDate);

            // Save database
            if (SaveDatabase())
            {
                Console.WriteLine("Database saved successfully");
            }
        }

        private static string DownloadJsonData(string url)
        {
            if (!Directory.Exists(JSON_DIRECTORY_NAME))
            {
                Directory.CreateDirectory(JSON_DIRECTORY_NAME);
            }

            using (var w = new WebClient())
            {
                var jsonString = string.Empty;
                try
                {
                    jsonString = w.DownloadString(url);
                    w.DownloadFile(url, (JSON_DIRECTORY_NAME + DateTime.Today.ToShortDateString() + "data.json"));
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to fetch new JSON data");
                }
                return jsonString;
            }
        }

        private static string ReadJsonFileToString(string fileName)
        {
            FileStream jsonFilestream = File.OpenRead(fileName);
            TextReader tr = new StreamReader(jsonFilestream);
            string jsonString = tr.ReadToEnd();
            jsonFilestream.Close();
            return jsonString;
        }

        private static T SeralizeJsonData<T>(string jsonData) where T : new()
        {
            return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData) : new T();
        }

        /// <summary>
        /// Load the database file from the specified path
        /// </summary>
        /// <returns>false if the database file does not exist</returns>
        public static bool LoadDatabase(string path = DATABASE_FILENAME)
        {
            //First check if the database file exists
            if (!File.Exists(path))
            {
                return false;
            }

            List<Date> dateList;

            DataContractSerializer s = new DataContractSerializer(typeof(List<Date>));
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                dateList = (List<Date>)s.ReadObject(fs);
            }

            dateList.ForEach(d => Database.AddDate(d));

            return true;
        }

        /// <summary>
        /// Save the database to the specified path. Delete any preexisting database files
        /// </summary>
        public static bool SaveDatabase(string path = DATABASE_FILENAME)
        {
            var dateList = Database.GetDateList();

            var xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };

            DataContractSerializer s = new DataContractSerializer(typeof(List<Date>));
            using (var xmlWriter = XmlWriter.Create(path, xmlWriterSettings))
            {
                s.WriteObject(xmlWriter, dateList);
            }
            return true;
        }
    }
}
