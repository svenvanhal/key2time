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

        private int counter = 1;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.Objects.ActivitiesList"/> class.
        /// </summary>
        /// <param name="_db">Database model</param>
        public ActivitiesList(DataModel _db) : base(_db)
        {
            SetListElement("Activities_List");
            Activities = new Dictionary<int, Activity>();
        }

        /// <summary>
        /// Creates the collection activities.
        /// </summary>
        private void CreateCollectionActivities()
        {
            var query = from activity in dB.tt_ActitvityGroup
                        join c in dB.School_Lookup_Class on activity.classId equals c.ClassID
                        join s in dB.Subject_SubjectGrade on activity.subjectId equals s.SubjectID
                        join t in dB.HR_MasterData_Employees on activity.teacherId equals t.EmployeeID
                        join grade in dB.School_Lookup_Grade on activity.gradeId equals grade.GradeID
                        join sub in dB.Subject_MasterData_Subject on activity.subjectId equals sub.SubjectID
                        where s.GradeID == activity.gradeId && t.IsActive == true
                        group new { activity.ActivityRefID, activity.teacherId, grade.GradeName, activity.subjectId, c.ClassName, c.ClassID, activity.Id, s.NumberOfLlessonsPerWeek, s.NumberOfLlessonsPerDay, s.CollectionID }
                        by activity.ActivityRefID into g
                        select g;
            foreach (var item in query)
            {
                var activity = item.First();
                var students = new Dictionary<string, int>();
                item.Select(x => new { x.ClassName, x.ClassID }).ToList().ForEach(x => students.Add(x.ClassName, x.ClassID));

                ActivityBuilder activityBuilder = new ActivityBuilder
                {
                    StudentsList = students,
                    TeachersList = item.Select(x => (int)x.teacherId).ToList(),
                    SubjectId = activity.subjectId,
                    NumberOfLessonsPerDay = activity.NumberOfLlessonsPerDay,
                    NumberOfLessonsPerWeek = activity.NumberOfLlessonsPerWeek,
                    CollectionID = activity.CollectionID,  
                    GradeName = activity.GradeName,
                    builderCounter = counter
                };

                if (item.Count() == 1) //Checks if there are more than one item in a group, if not, than it is not a collection
                    activityBuilder.IsCollection = false;
                else
                    activityBuilder.IsCollection = true;

                activityBuilder.GetResults().ForEach(x => Activities.Add(x.Id, x));
                counter = activityBuilder.builderCounter;
            }
        }

        /// <summary>
        /// Creates the single activities.
        /// </summary>
        private void CreateSingleActivities()
        {
            var query = from activity in dB.School_ClassTeacherSubjects
                        join c in dB.School_Lookup_Class on activity.ClassID equals c.ClassID
                        join s in dB.Subject_SubjectGrade on activity.SubjectID equals s.SubjectID
                        join t in dB.HR_MasterData_Employees on activity.TeacherID equals t.EmployeeID
                        join grade in dB.School_Lookup_Grade on c.GradeID equals grade.GradeID
                        select new { activity.TeacherID, activity.SubjectID, c.ClassName, c.ClassID, s.NumberOfLlessonsPerWeek, s.NumberOfLlessonsPerDay };

            foreach (var item in query.Distinct())
            {

                ActivityBuilder activityBuilder = new ActivityBuilder
                {
                    StudentsList = new Dictionary<string, int> { { item.ClassName, item.ClassID } },
                    TeachersList = new List<int> { (int)item.TeacherID },
                    SubjectId = item.SubjectID,
                    NumberOfLessonsPerDay = item.NumberOfLlessonsPerDay,
                    NumberOfLessonsPerWeek = item.NumberOfLlessonsPerWeek,
                    IsCollection = false,
                    builderCounter = counter
                };

                activityBuilder.GetResults().ForEach(x => Activities.Add(x.Id, x));
                counter = activityBuilder.builderCounter;
            }
        }

        /// <summary>
        /// Create the list with Activity XElements
        /// </summary>
        public override XElement Create()
        {
            CreateCollectionActivities();
            CollectionMerge();
            CreateSingleActivities();

            Activities.ToList().ForEach(item => List.Add(item.Value.ToXElement()));
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
            var query = clone.Values.Select(item => item.CollectionId).Distinct();

            foreach (var item in query)
            {
                var list = Activities.Values.Where(x => x.CollectionId.Equals(item));

                //Groups the lessons on the number of lesson of the week. 
                var group = from a in list
                            group a by a.NumberLessonOfWeek into g
                            select g;

                foreach (var i in group)
                {
                    var students = new Dictionary<string, int>();
                    var teachers = new List<int>();

                    i.Select(x => x.Students).ToList().ForEach(l => students = students.Union(l).ToDictionary(s => s.Key, s => s.Value));
                    i.Select(x => x.Teachers).ToList().ForEach(teachers.AddRange);

                    var act = new Activity
                    {
                        Teachers = teachers.Distinct().ToList(),
                        Students = students,
                        Id = i.First().Id,
                        GroupId = i.First().GroupId,
                        Duration = i.First().Duration,
                        TotalDuration = i.First().TotalDuration,
                        NumberLessonOfWeek = i.First().NumberLessonOfWeek,
                        IsCollection = true,
                        CollectionId = i.First().CollectionId
                    };

                    i.Select(x => x.Id).ToList().ForEach(x => Activities.Remove(x));
                    Activities.Add(act.Id, act);
                }
            }
        }
    }
}
