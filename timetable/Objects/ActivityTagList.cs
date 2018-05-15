using System;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public class ActivityTagList : AbstractList
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="T:Timetable.timetable.Objects.ActivityTagList"/> class.
        /// </summary>
        /// <param name="_db">Database model</param>
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
