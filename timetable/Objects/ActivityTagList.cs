using System;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public class ActivityTagList : AbstractList
    {
		public ActivityTagList(DataModel _db) : base( _db)
        {
			SetListElement("Activity_Tags_List");
        }

		public override void Create()
		{
			throw new NotImplementedException();
		}
	}
}
