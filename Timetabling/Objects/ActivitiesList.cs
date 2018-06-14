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
        public IDictionary<int, Activity> Activities { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.ActivitiesList"/> class.
        /// </summary>
        /// <param name="_db">Database model</param>
        public ActivitiesList(DataModel _db) : base(_db)
        {
            SetListElement("Activities_List");
            Activities = new Dictionary<int, Activity>();
        }

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
                        join grade in dB.School_Lookup_Grade on activity.gradeId equals grade.GradeID
                        join sub in dB.Subject_MasterData_Subject on activity.subjectId equals sub.SubjectID
                        where s.GradeID == activity.gradeId && t.IsActive == true
                        group new { activity.ActivityRefID, activity.teacherId, grade.GradeName, activity.subjectId, c.ClassName, activity.Id, s.NumberOfLlessonsPerWeek, s.NumberOfLlessonsPerDay, sub.CollectionID }
                        by activity.ActivityRefID into g
                        select g;

            foreach (var item in query)
            {
                var studentsList = item.Select(x => x.ClassName).ToList();
                var teachersList = item.Select(x => (int)x.teacherId).ToList();

                if (item.Count() == 1) //Checks if there are more than one item in a group, if not, than it is not a collection
                    AddActivity(item.First(), studentsList, teachersList, false);
                else
                    AddActivity(item.First(), studentsList, teachersList, true);
            }
        }
        /// <summary>
        /// Adds the activity to the list.
        /// </summary>
        /// <param name="item">The item used in the query.</param>
        /// <param name="studentsList">A list of all the distinct studentssets.</param>
        /// <param name="teachersList">A list of all the distinct teachersets.</param>
        /// <param name="IsColl">If set to <c>true</c>, the activity is a collection.</param>
        private void AddActivity(dynamic item, List<string> studentsList, List<int> teachersList, bool IsColl)
        {

            var groupId = counter;

            for (var i = 1; i <= item.NumberOfLlessonsPerWeek; i++)
            {
                var act = new Activity
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
                    IsCollection = IsColl
                };

                if (IsColl && item.CollectionID != null)
                {
                    act.SetCollection(item.CollectionID, item.GradeName);
                }

                Activities.Add(counter, act);
                counter++;
            }
        }

        /// <summary>
        /// Create the list with Activity XElements
        /// </summary>
        public override XElement Create()
        {
            CreateActivities();
            CollectionMerge();

            foreach (var item in Activities)
            {
                List.Add(item.Value.ToXElement());
            }
            return List;
        }

        /// <summary>
        /// Merges the activities if they are in a collection
        /// </summary>
        private void CollectionMerge()
        {
            var clone = Activities.ToDictionary(entry => entry.Key,
                                               entry => entry.Value);
            //Select distinct collections on the colletionstring
            var query = clone.Values.Where(item => item.CollectionString != "").Select(item => item.CollectionString).Distinct();
            foreach (var item in query)
            {
                var list = Activities.Values.Where(x => x.CollectionString.Equals(item));

                //Groups the lessons on the number of lesson of the week. 
                var group = from a in list
                            group a by a.NumberLessonOfWeek into g
                            select g;

                foreach (var i in group)
                {
                    var students = new List<string>();
                    var teachers = new List<int>();
                    i.Select(x => x.Students).ToList().ForEach(students.AddRange);
                    i.Select(x => x.Teachers).ToList().ForEach(teachers.AddRange);
                    students = students.Distinct().ToList();
                    teachers = teachers.Distinct().ToList();

                    var act = new Activity
                    {
                        Teachers = teachers,
                        Students = students,
                        Id = i.First().Id,
                        GroupId = i.First().GroupId,
                        Duration = i.First().Duration,
                        TotalDuration = i.First().TotalDuration,
                        NumberLessonOfWeek = i.First().NumberLessonOfWeek,
                        IsCollection = true,
                        CollectionString = item,
                        CollectionId = i.First().CollectionId
                    };

                    i.Select(x => x.Id).ToList().ForEach(x => Activities.Remove(x));
                    Activities.Add(act.Id,act);
                }
            }
        }

    }

}
