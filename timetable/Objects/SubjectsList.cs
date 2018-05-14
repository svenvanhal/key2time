using System;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public class SubjectsList : AbstractList
	{
		public SubjectsList(DataModel _dB) : base(_dB)
		{
			SetListElement("Subjects_List");

		}

		public override void Create()
		{
			var query = dB.Subject_MasterData_Subject;

			foreach (var subject in query)
			{

				list.Add(new XElement("Subject",
									  new XElement("Name", subject.SubjectName)));
			}
		}
	}
}