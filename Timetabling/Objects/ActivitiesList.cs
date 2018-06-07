using Timetabling.DB;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Timetabling.Objects
{
    /// <summary>
    /// Activities list.
    /// </summary>
    public class ActivitiesList : AbstractList
    {
        private int counter = 1;
        /// <summary>
        /// The activities.
        /// </summary>
        public List<Activity> Activities = new List<Activity>();
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
            var query = from activity in dB.tt_ActitvityGroup
                        join c in dB.School_Lookup_Class on activity.classId equals c.ClassID
                        join s in dB.Subject_SubjectGrade on activity.subjectId equals s.SubjectID
                        join t in dB.HR_MasterData_Employees on activity.teacherId equals t.EmployeeID
                        where s.GradeID == activity.gradeId && t.IsActive == true
                        select new { activity.ActivityRefID, activity.teacherId, activity.subjectId, c.ClassName, activity.Id, s.NumberOfLlessonsPerWeek, s.NumberOfLlessonsPerDay };

            foreach (var item in query)
            {
                var groupId = counter;

                for (var i = 1; i <= item.NumberOfLlessonsPerWeek; i++)
                {
                    var StudentsList = new List<string>(); //TODO: need be from the db
                    StudentsList.Add(item.ClassName);

                    Activities.Add(new Activity
                    {
                        LessonGroupId = item.ActivityRefID,
                        Teacher = (int)item.teacherId,
                        Subject = item.subjectId,
                        Students = StudentsList,
                        Id = counter,
                        GroupId = groupId,
                        Duration = item.NumberOfLlessonsPerDay,
                        TotalDuration = item.NumberOfLlessonsPerWeek * item.NumberOfLlessonsPerDay,
                        NumberLessonOfWeek = i
                    });

                    counter++;
                }
            }

        }
        /// <summary>
        /// Gets the list as XElement.
        /// </summary>
        /// <returns>The list.</returns>
        public override XElement GetList()
        {
            foreach (var item in Activities)
            {
                List.Add(item.ToXElement());
            }
            return List;
        }
    }

}
