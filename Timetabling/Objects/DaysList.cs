using System;
using Timetabling.DB;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

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
		public DaysList(DataModel _db) : base(_db)
		{
			SetListElement("Days_List");
		}

        /// <summary>
        /// Create this number of days element, and adds each day
        /// </summary>
		public override void Create()
		{

			list.Add(new XElement("Number_of_Days", 7 - dB.Section_WeekEnd.Where(item => item.sectionId.Equals(1)).Count()));
			var l = dB.Section_WeekEnd.Select(day => day.dayIndex);

			var daysList = Enum.GetValues(typeof(Days)).OfType<Days>().Where(day => !l.Any(x => x == (int)day));
			foreach (var day in daysList)
			{
				list.Add(new XElement("Day", new XElement("Name", day)));
			}
		}
	}
}
