using System;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public class HoursList : AbstractList
    {
		public HoursList(DataModel _db)  : base(_db)
        {
			SetListElement("Hours_List");
        }

		public override void Create()
		{
			throw new NotImplementedException();
		}
	}
}
