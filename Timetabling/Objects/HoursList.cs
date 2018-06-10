using System.Collections.Generic;
using System.Xml.Linq;
using Timetabling.DB;
using Timetabling.Resources;

namespace Timetabling.Objects
{
    
    /// <summary>
    /// Hours list.
    /// </summary>
    public class HoursList : AbstractList
    {
        /// <summary>
        /// Gets the number of hours, default is 8.
        /// </summary>
        /// <value>The number of hours.</value>
        public int numberOfHours { get; } = 9;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.HoursList"/> class.
        /// </summary>
        /// <param name="_db">The datamodel</param>
        public HoursList(DataModel _db) : base(_db) => SetListElement("Hours_List");

        /// <summary>
        /// Creates the hour elements.
        /// </summary>
        public override void Create()
        {
            List.Add(new XElement("Number_of_Hours", numberOfHours));

            for (int i = 1; i <= numberOfHours; i++)
            {
                List.Add(new XElement("Hour", new XElement("Name", i)));
            }
        }

        public static Dictionary<int, Timeslot> GetTimeslots()
        {

            var timeslots = new Dictionary<int, Timeslot>();

            for (var i = 1; i <= 9; i++)
            {
                timeslots.Add(i, new Timeslot{
                    Name = $"Period {i}"
                });
            }

            return timeslots;
        }
    }

}
