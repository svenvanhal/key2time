using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Create the subjects elements list.
        /// </summary>
		public override void Create()
		{
			var query = dB.Subject_MasterData_Subject.Where(subject => subject.IsActive == true)
			              .Select(subject => subject.SubjectID);

			foreach (var subject in query)
			{

				list.Add(new XElement("Subject",
									  new XElement("Name", subject)));
			}
		}
	}
}