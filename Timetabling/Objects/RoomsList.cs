using System;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;
namespace Timetabling.Objects

{
	public class RoomsList : AbstractList
	{
		public RoomsList(DataModel _db) : base(_db)
		{
			SetListElement("Rooms_List");
		}

		public override void Create()
		{
			var query = dB.School_BuildingsUnits.Where(room => room.IsActive == true)
						  .Select(room => room.ID);

			foreach (var room in query)
			{

				list.Add(new XElement("Room",
									  new XElement("Name", room)));
			}
		}
	}
}
