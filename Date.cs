using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace RentCafeTracker
{
    [DataContract]
    public class Date
    {
        [DataMember]
        private List<Unit> unitList;

        [DataMember]
        public DateTime timestamp;

        // Default constructor
        public Date()
        {
            // Fetch current timestamp
            timestamp = DateTime.Today;
            Console.WriteLine(timestamp.ToShortDateString());
            unitList = new List<Unit>();
        }

        public void ImportUnitData(Unit[] unitArray)
        {
            List<Unit> units = new List<Unit>(unitArray);
            foreach (Unit unit in units)
            {
                unit.date_fetched = timestamp;
                unitList.Add(unit);
            }
        }

        public List<Unit> GetUnitsList()
        {
            return unitList;
        }
    }
}
