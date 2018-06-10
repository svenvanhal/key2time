using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;
using Timetabling.Resources;

namespace Timetabling.Objects
{
    
    /// <summary>
    /// Teachers list.
    /// </summary>
    public class TeachersList : AbstractList
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.TeachersList"/> class.
        /// </summary>
        /// <param name="_dB">Database model.</param>
        public TeachersList(DataModel _dB) : base(_dB) => SetListElement("Teachers_List");

        /// <summary>
        /// Creates the Teacher elements from the datamodel
        /// </summary>
        public override void Create()
        {
            var query = dB.HR_MasterData_Employees.Where(teacher => teacher.IsTeacher == true && teacher.IsActive == true)
                          .Select(teacher => teacher.EmployeeID);

            foreach (var item in query)
            {
                List.Add(new XElement("Teacher", new XElement("Name", item)));
            }
        }

        public static Dictionary<int, Teacher> GetTeachers(DataModel model)
        {

            var teachers = new Dictionary<int, Teacher>();

            var query = model.HR_MasterData_Employees.Where(teacher => teacher.IsTeacher == true && teacher.IsActive == true);

            // Loop over all teachers
            foreach (var teacher in query)
            {
                teachers.Add((int) teacher.EmployeeID, new Teacher {
                    Id = (int)teacher.EmployeeID,
                    Name = teacher.ShortName
                });
            }

            return teachers;
        }

    }
}
