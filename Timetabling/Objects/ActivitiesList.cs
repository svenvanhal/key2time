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
            //Gets all the activities
            var activityQuery = from activity in dB.School_TeacherClass_Subjects
                        join c in dB.School_Lookup_Class on activity.ClassID equals c.ClassID
                        join s in dB.Subject_SubjectGrade on new { activity.SubjectID, c.GradeID } equals new { s.SubjectID, s.GradeID }
                        select new { c.ClassName, activity.TeacherID, activity.SubjectID, activity.ID, s.NumberOfLlessonsPerWeek, s.NumberOfLlessonsPerDay };
           
            foreach (var item in activityQuery)
            {
                //Gets the subgroups of a class
                var studentsQuery = from g in dB.TeacherClassSubjectGroups
                                    join c in dB.Tt_ClassGroup on g.GroupId equals c.Id
                                    where g.teacherClassSubjectId == item.ID
                                    select c.groupName;
                var groupId = counter;

                //Creates a activity for the defined number of lessons per week
                for (var i = 1; i <= item.NumberOfLlessonsPerWeek; i++)
                {
                    var xElement = new XElement("Activity",
                             new XElement("Teacher", item.TeacherID),
                             new XElement("Subject", item.SubjectID),
                             new XElement("Id", counter),
                             new XElement("Activity_Group_Id", groupId),
                             new XElement("Duration", item.NumberOfLlessonsPerDay),
                             new XElement("Total_Duration", item.NumberOfLlessonsPerWeek * item.NumberOfLlessonsPerDay));

                    //Checks if there are subgroups defined, if not, then the classname will be used
                    if (studentsQuery.Count() > 0)
                    {
                        studentsQuery.ToList().ForEach(x => xElement.Add(new XElement("Students", x)));
                    }
                    else
                    {
                        xElement.Add(new XElement("Students", item.ClassName));
                    }
                    List.Add(xElement);

                    counter++;
                }
            }

        }
    }

}
