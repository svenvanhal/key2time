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
			var query = dB.School_TeacherClass_Subjects;
			foreach( var item in query){

				list.Add(new XElement("Activity", 
				                      new XElement("Teacher", item.TeacherID), 
				                      new XElement("Subject", item.SubjectID),
				                      new XElement("Students", item.ClassID)));
			}

			//var query = from lesson in dB.School_TeacherClass_Subjects
			                             //join t in dB.HR_MasterData_Employees on lesson.TeacherID equals t.EmployeeID
			                             //join c in dB.School_Lookup_Class on lesson.ClassID equals c.ClassID
			                             //join s in dB.Subject_MasterData_Subject on lesson.SubjectID equals s.SubjectID
			                             //select new {t.EmployeeID, c.ClassID, s.SubjectID, 
				        
		}
	}


}
