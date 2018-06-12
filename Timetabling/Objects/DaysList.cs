using System;
using Timetabling.DB;
using System.Linq;
using System.Xml.Linq;

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
        public override XElement Create()
        {
            List.Add(new XElement("Number_of_Days", 7));

            var daysList = Enum.GetValues(typeof(Days)).OfType<Days>();
            foreach (var day in daysList)
            {
                List.Add(new XElement("Day", new XElement("Name", day)));
            }

            return List;
        }
    }

}
