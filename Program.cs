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
        protected const string API_URL = "https://www.rentnema.com/rent-cafe-api.php?type=0";
        protected const string DATABASE_FILENAME = "database.xml";
        protected const string JSON_DIRECTORY_NAME = "json-data-files/";
        protected static List<string> API_TYPE_LIST = new List<string>(new[] {"0", "1", "2"});


        static void Main(string[] args)
        {
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

            // Parse args
            if (args.Length > 0)
            {
                if (args[0].Equals("fetch"))
                {

                }
                else if (args[0].Equals("import"))
                {

                }
                else if (args[0].Equals("analysis"))
                {

                }
            }

            //FetchNewData();
            ImportLocalFile("data.json");
        }


        private static bool FetchNewData()
        {
            Date newDate = new Date();

            foreach (string apiType in API_TYPE_LIST)
            {
                // Create a new date object and import units data
                string stringData = DownloadJsonData(apiType);
                Rootobject jsonData = SeralizeJsonData<Rootobject>(stringData);
                newDate.ImportUnitData(jsonData.units);
            }

            // Load new date entry into database
            Database.AddDate(newDate);

            // Save database
            if (SaveDatabase())
            {
                Console.WriteLine("Database saved successfully");
                return true;
            }

            return false;
        }

        public static bool ImportLocalFile(string fileName)
        {
            Date newDate = new Date();
            string stringData = ReadJsonFileToString(fileName);
            Rootobject jsonData = SeralizeJsonData<Rootobject>(stringData);
            newDate.ImportUnitData(jsonData.units);
            Database.AddDate(newDate);
            return true;
        }

        private static string DownloadJsonData(string apiType)
        {
            if (!Directory.Exists(JSON_DIRECTORY_NAME))
            {
                Directory.CreateDirectory(JSON_DIRECTORY_NAME);
            }

            string url = (API_URL + apiType);

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
            string jsonString = "Null";
            try
            {
                FileStream jsonFilestream = File.OpenRead(fileName);
                TextReader tr = new StreamReader(jsonFilestream);
                jsonString = tr.ReadToEnd();
                jsonFilestream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: " + e.Message);
            }


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
