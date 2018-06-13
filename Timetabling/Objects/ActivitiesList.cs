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
                        join sub in dB.Subject_MasterData_Subject on activity.subjectId equals sub.SubjectID
                        where s.GradeID == activity.gradeId && t.IsActive == true
                        select new { activity.ActivityRefID, activity.teacherId, activity.subjectId, c.ClassName, activity.Id, s.NumberOfLlessonsPerWeek, s.NumberOfLlessonsPerDay, sub.CollectionID };



            var group = from a in query
                        group new { a.ActivityRefID, a.teacherId, a.subjectId, a.ClassName, a.Id, a.NumberOfLlessonsPerWeek, a.NumberOfLlessonsPerDay, a.CollectionID }
                 by a.ActivityRefID into g
                        select g;

            foreach (var item in group)
            {

                var distinctClass = item.Select(x => x.ClassName).Distinct().Count();

                if( item.Count() == 1){
                    var studentsList = item.Select(x => x.ClassName).ToList();
                    var teachersList = item.Select(x => (int) x.teacherId).ToList();
                    AddActivity(item.First(), studentsList, teachersList, false);
                }
                if (distinctClass == 1)
                {
                    foreach (var act in item)
                    {
                        var studentsList = new List<string>();
                        studentsList.Add(act.ClassName);
                        var teachersList = new List<int>();
                        teachersList.Add((int) act.teacherId);

                        AddActivity(act, studentsList, teachersList, true);
                    }
                }


                if (distinctClass == 1)
                {
                    var studentsList = item.Select(x => x.ClassName).ToList();

                }
                System.Console.WriteLine("group");
                item.ToList().ForEach(x => System.Console.WriteLine(x));
            }

            foreach (var item in query)
            {


                // Build students list

                //if (studentsQuery.Any()) studentsList = studentsQuery.ToList();
                //else 

                // Add activities
                AddActivity(item);
            }
        }

        private void AddActivity(dynamic item, List<string> studentsList, List<int> teachersList, bool IsColl)
        {
            // Add activities

            var groupId = counter;

            for (var i = 1; i <= item.NumberOfLlessonsPerWeek; i++)
            {

                Activities.Add(counter, new Activity
                {
                    LessonGroupId = item.ActivityRefID,
                    Teachers = teachersList,
                    Subject = item.subjectId,
                    Students = studentsList,
                    Id = counter,
                    GroupId = groupId,
                    Duration = item.NumberOfLlessonsPerDay,
                    TotalDuration = item.NumberOfLlessonsPerWeek * item.NumberOfLlessonsPerDay,
                    NumberLessonOfWeek = i,
                    IsCollection = IsColl,
                    CollectionId = item.CollectionID,
                });

                counter++;
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
