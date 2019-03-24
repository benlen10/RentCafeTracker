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

            // Save database
            SaveDatabase();
            Database.CalculateLowestPrices();
        }


        private static bool FetchNewData()
        {
            Date newDate = new Date();

            foreach (string apiType in Constants.API_TYPE_LIST)
            {
                // Create a new date object and import units data
                string stringData = DownloadJsonData(apiType);
                Rootobject jsonData = SeralizeJsonData<Rootobject>(stringData);
                newDate.ImportUnitData(jsonData.units);
            }

            // Load new date entry into database
            Console.WriteLine("Read data for the following number of units:" + newDate.GetUnitsList().Count);
            Database.AddDate(newDate);

            return true;
        }

        public static bool ImportLocalFile(string fileName)
        {
            Date newDate = new Date();
            string stringData = ReadJsonFileToString(fileName);
            Rootobject jsonData = SeralizeJsonData<Rootobject>(stringData);
            newDate.ImportUnitData(jsonData.units);
            Console.WriteLine("Read data for the following number of units:" + newDate.GetUnitsList().Count);
            Database.AddDate(newDate);
            return true;
        }

        private static string DownloadJsonData(string apiType)
        {
            if (!Directory.Exists(Constants.JSON_DIRECTORY_NAME))
            {
                Directory.CreateDirectory(Constants.JSON_DIRECTORY_NAME);
            }

            string url = (Constants.API_URL + apiType);

            using (var w = new WebClient())
            {
                var jsonString = string.Empty;
                try
                {
                    jsonString = w.DownloadString(url);
                    w.DownloadFile(url, (Constants.JSON_DIRECTORY_NAME + DateTime.Today.ToShortDateString() + "data.json"));
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
        public static bool LoadDatabase(string path = Constants.DATABASE_FILENAME)
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
        public static bool SaveDatabase(string path = Constants.DATABASE_FILENAME)
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
            Console.WriteLine("Database saved successfully");
            return true;
        }
    }
}
