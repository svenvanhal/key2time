using System;
using System.Collections.Generic;
using Timetabling.DB;
using System.Linq;
using System.Xml.Linq;
using Timetabling.Resources;

namespace Timetabling.Objects
{
    
    /// <summary>
    /// Days list.
    /// </summary>
    public class DaysList : AbstractList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.DaysList"/> class.
        /// </summary>
        /// <param name="_db">The datamodel.</param>
        public DaysList(DataModel _db) : base(_db) => SetListElement("Days_List");

        /// <summary>
        /// Create this number of days element, and adds each day
        /// </summary>
        public override void Create()
        {
            List.Add(new XElement("Number_of_Days", 7));

            var daysList = Enum.GetValues(typeof(Days)).OfType<Days>();
            foreach (var day in daysList)
            {
                List.Add(new XElement("Day", new XElement("Name", day)));
            }
        }

        public static Dictionary<int, Day> GetDays()
        {

            var days = new Dictionary<int, Day>();
            
            foreach (int day in Enum.GetValues(typeof(Days)))
            {
                days.Add(day, new Day
                {
                    Name = Enum.GetName(typeof(Days), day)
                });
            }

            return days;
        }

    }

}
