using Timetabling.DB;
using System.Linq;
using System.Xml.Linq;

namespace Timetabling.Objects
{
    /// <summary>
    /// Activities list.
    /// </summary>
    public class ActivitiesList : AbstractList
    {
        private int counter = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.ActivitiesList"/> class.
        /// </summary>
        /// <param name="_db">Database model</param>
        public ActivitiesList(DataModel _db) : base(_db) => SetListElement("Activities_List");

        /// <summary>
        /// Create the list with Activity XElements
        /// </summary>
        public override void Create()
        {
            var query = from activity in dB.School_TeacherClass_Subjects
                        join c in dB.School_Lookup_Class on activity.ClassID equals c.ClassID
                        join s in dB.Subject_SubjectGrade on new { activity.SubjectID, c.GradeID } equals new { s.SubjectID, s.GradeID }
                        select new { c.ClassName, activity.TeacherID, activity.SubjectID, activity.ID, s.NumberOfLlessonsPerWeek, s.NumberOfLlessonsPerDay };
            query.ToList().ForEach(x => System.Console.WriteLine(x));
            foreach (var item in query)
            {
                var studentsQuery = from g in dB.TeacherClassSubjectGroups
                                    join c in dB.Tt_ClassGroup on g.GroupId equals c.Id
                                    where g.teacherClassSubjectId == item.ID
                                    select c.groupName;
                var groupId = counter;

                for (var i = 1; i <= item.NumberOfLlessonsPerWeek; i++)
                {
                    var temp = new XElement("Activity",
                             new XElement("Teacher", item.TeacherID),
                             new XElement("Subject", item.SubjectID),
                             new XElement("Id", counter),
                             new XElement("Activity_Group_Id", groupId),
                             new XElement("Duration", item.NumberOfLlessonsPerDay),
                             new XElement("Total_Duration", item.NumberOfLlessonsPerWeek * item.NumberOfLlessonsPerDay));

                    if (studentsQuery.Count() > 0)
                    {
                        studentsQuery.ToList().ForEach(x => temp.Add(new XElement("Students", x)));
                    }
                    else
                    {
                        temp.Add(new XElement("Students", item.ClassName));
                    }
                    List.Add(temp);

                    counter++;
                }
            }

        }
    }

}
