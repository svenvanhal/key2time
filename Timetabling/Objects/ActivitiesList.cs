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

        /// <summary>
        /// The activities.
        /// </summary>
        public IDictionary<int, Activity> Activities = new Dictionary<int, Activity>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.ActivitiesList"/> class.
        /// </summary>
        /// <param name="_db">Database model</param>
        public ActivitiesList(DataModel _db) : base(_db) => SetListElement("Activities_List");

        private int counter = 1;

        /// <summary>
        /// Construct activity objects from database.
        /// </summary>
        private void CreateActivities()
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

                // Build students list
                var studentsList = new List<string>();
                 studentsList.Add(item.ClassName);

                // Add activities
                for (var i = 1; i <= item.NumberOfLlessonsPerWeek; i++)
                {

                    Activities.Add(counter, new Activity
                    {
                        LessonGroupId = item.ActivityRefID,
                        Teacher = (int)item.teacherId,
                        Subject = item.subjectId,
                        Students = studentsList,
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
        /// Create the list with Activity XElements
        /// </summary>
        public override XElement Create()
        {
            CreateActivities();

            foreach (var item in Activities)
            {
                List.Add(item.Value.ToXElement());
            }
            return List;
        }

    }

}
