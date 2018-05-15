using System;
using Timetable.timetable.DB;
using System.Linq;
using System.Xml.Linq;

namespace Timetable.timetable.Objects
{
	public class ActivitiesList : AbstractList
	{
		public ActivitiesList(DataModel _db) : base(_db)
		{
			SetListElement("Activities_List");
		}

		public override void Create()
		{
			var query = from activity in dB.School_TeacherClass_Subjects
						join c in dB.School_Lookup_Class on activity.ClassID equals c.ClassID
						select new { activity.TeacherID, activity.SubjectID, c.ClassName, activity.ID };
			foreach (var item in query)
			{

				list.Add(new XElement("Activity",
									  new XElement("Teacher", item.TeacherID),
									  new XElement("Subject", item.SubjectID),
									  new XElement("Students", item.ClassName),
									  new XElement("Id", item.ID),
									  new XElement("Activity_Group_Id", "0"),
									  new XElement("Duration", '1'),
									  new XElement("Total_Duration", '1')
								)
							);
			}
			//var query = from lesson in dB.School_TeacherClass_Subjects
			//join t in dB.HR_MasterData_Employees on lesson.TeacherID equals t.EmployeeID
			//join c in dB.School_Lookup_Class on lesson.ClassID equals c.ClassID
			//join s in dB.Subject_MasterData_Subject on lesson.SubjectID equals s.SubjectID
			//select new {t.EmployeeID, c.ClassID, s.SubjectID, 

		}
	}


}
