using System;
using System.Collections.Generic;
using System.Text;

namespace RentCafeTracker
{
    public static class Constants
    {
        public const string API_URL = "https://www.rentnema.com/rent-cafe-api.php?type=0";
        public const string DATABASE_FILENAME = "database.xml";
        public const string JSON_DIRECTORY_NAME = "json-data-files/";
        public static List<string> API_TYPE_LIST = new List<string>(new[] { "0", "1", "2" });

        // Bedroom Types
        public const string STUDIO = "STUDIO";
        public const string ONE_BED = "1BR";
        public const string TWO_BED = "2BR";


    }
}
