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

        [DataMember]
        private static Unit lowestStudio;

        [DataMember]
        private static Unit lowestOneBedroom;

        [DataMember]
        private static Unit lowestTwoBedroom;

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

        public static void CalculateLowestPrices()
        {
            if (lowestStudio == null)
            {
                lowestStudio = new Unit()
                {
                    rent = 10000
                };
            }

            if (lowestOneBedroom == null)
            {
                lowestOneBedroom = new Unit()
                {
                    rent = 10000
                };
            }

            if (lowestTwoBedroom == null)
            {
                lowestTwoBedroom = new Unit()
                {
                    rent = 10000
                };
            }


            foreach (Date date in dateList)
            {
                foreach (Unit unit in date.GetUnitsList())
                {
                    if (unit.bedType.Equals(Constants.STUDIO))
                    {
                        if (unit.rent < lowestStudio.rent)
                        {
                            lowestStudio = unit;
                            Console.WriteLine("ALERT: New low price found: " + Constants.STUDIO);
                        }
                    }
                    if (unit.bedType.Equals(Constants.ONE_BED))
                    {
                        if (unit.rent < lowestOneBedroom.rent)
                        {
                            lowestOneBedroom = unit;
                            Console.WriteLine("ALERT: New low price found: " + Constants.ONE_BED);
                        }
                    }
                    if (unit.bedType.Equals(Constants.TWO_BED))
                    {
                        if (unit.rent < lowestTwoBedroom.rent)
                        {
                            lowestTwoBedroom = unit;
                            Console.WriteLine("ALERT: New low price found: " + Constants.TWO_BED);
                        }
                    }
                }
                
            }
            // Print results
            Console.WriteLine("\n\nLOWEST HISTORICAL STUDIO PRICE: ");
            if (lowestStudio.rent != 10000)
            {
                lowestStudio.PrintUnitInfo();
            }

            // Print results
            Console.WriteLine("\n\nLOWEST HISTORICAL ONE BEDROOM PRICE:");
            if (lowestOneBedroom.rent != 10000)
            {
                lowestOneBedroom.PrintUnitInfo();
            }

            // Print results
            Console.WriteLine("\n\nLOWEST HISTORICAL TWO BEDROOM PRICE:");
            if (lowestTwoBedroom.rent != 10000)
            {
                lowestTwoBedroom.PrintUnitInfo();
            }
        }
    }
}
