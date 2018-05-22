using System;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public class HoursList : AbstractList
	{

		public int numberOfHours { get; } = 8;

		public HoursList(DataModel _db) : base(_db)
		{
			SetListElement("Hours_List");
		}

		public override void Create()
		{
			
			list.Add(new XElement("Number_of_Hours", numberOfHours));
			for (int i = 1; i <= numberOfHours; i++)
			{
				list.Add(new XElement("Hour", new XElement("Name", i)));
			}
		}
	}
}
