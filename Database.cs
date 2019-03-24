using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RentCafeTracker
{
    [DataContract]
    public class Database
    {
        [DataMember]
        private static List<Date> dateList;

        // Public constructor
        public static void Initalize()
        {
            dateList = new List<Date>();
        }

        public static bool AddDate(Date date)
        {
            // Check if date entry already exists and if so, return false
            foreach (var dateEntry in dateList)
            {
                if (dateEntry.timestamp.ToShortDateString().Equals(date.timestamp.ToShortDateString()))
                {
                    System.Console.WriteLine("Date already exists in database");
                    return false;
                }
            }
            dateList.Add(date);
            return true;
        }

        public static List<Date> GetDateList()
        {
            return dateList;
        }

        public static void LowestPrices()
        {
            Unit lowestStudio = new Unit()
            {
                rent = 10000
            };

            Unit lowest1Bedroom = new Unit()
            {
                rent = 10000
            };

            Unit lowest2Bedroom = new Unit()
            {
                rent = 10000
            };
/*

            foreach (Date date in dateList)
            {
                foreach (Unit unit in date.GetUnitsList())
                {
                    if(unit.bedType.Equals())
                }
                
            }*/
        }
    }
}
