using System;
using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Objects
{
	public class HoursList : AbstractList
	{
		public HoursList(DataModel _db) : base(_db)
		{
			SetListElement("Hours_List");
		}

		public override void Create()
		{
			var numberOfHours = 8; //TO DO:Retrieve from DB
			list.Add(new XElement("Number_of_Hours", numberOfHours));
			for (int i = 1; i <= numberOfHours; i++)
			{
				list.Add(new XElement("Hour", new XElement("Name", i)));
			}
		}
	}
}
