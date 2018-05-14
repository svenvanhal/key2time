

using System;
using System.Linq;
using System.Xml.Linq;
using Timetable.timetable.DB;

namespace Timetable.timetable.Objects
{
	public class TeachersList : AbstractList
	{


		public TeachersList(DataModel _dB) : base(_dB)
		{
			SetListElement("Teachers_List");
		}



		public override void Create()
		{
			var query = dB.HR_MasterData_Employees.Where(teacher => teacher.IsTeacher == true);

			foreach (var teacher in query)
			{

				list.Add(new XElement("Teacher",
											  new XElement("Name", teacher.EmployeeID)));
			}
		}
	}
}
