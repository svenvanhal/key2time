using System;
using Timetabling.DB;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Timetabling.Objects
{
	public class DaysList : AbstractList
	{
		public DaysList(DataModel _db) : base(_db)
		{
			SetListElement("Days_List");
		}

        /// <summary>
        /// Create this number of days element, and adds each day
        /// </summary>
		public override void Create()
		{

			list.Add(new XElement("Number_of_Days", 7 - dB.Section_WeekEnd.Count()));
			var l = dB.Section_WeekEnd.Select(day => day.dayIndex);

			var daysList = Enum.GetValues(typeof(Days)).OfType<Days>().Where(day => !l.Any(x => x == (int)day));
			foreach (var day in daysList)
			{
				list.Add(new XElement("Day", new XElement("Name", day)));
			}
		}
	}
}
