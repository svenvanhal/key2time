using System;

using Timetable.timetable.DB;
namespace Timetable.timetable.Objects

{
	public class RoomsList : AbstractList
	{
		public RoomsList(DataModel _db) : base(_db)
		{
			SetListElement("Rooms_List");
		}
        
		public override void Create()
		{
			throw new NotImplementedException();
		}
	}
}
