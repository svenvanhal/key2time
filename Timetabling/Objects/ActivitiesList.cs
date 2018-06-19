using Timetabling.DB;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Timetabling.Objects
{

    /// <inheritdoc />
    /// <summary>
    /// Activities list.
    /// </summary>
    public class ActivitiesList : AbstractList
    {

        /// <summary>
        /// The activities.
        /// </summary>
        public IDictionary<int, Activity> Activities { get; }

        private int _counter = 1;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new ActivitiesList.
        /// </summary>
        /// <param name="model">Database model</param>
        public ActivitiesList(DataModel model) : base(model)
        {
            SetListElement("Activities_List");
            Activities = new Dictionary<int, Activity>();
        }

        /// <inheritdoc />
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
        /// Creates the collection activities.
        /// </summary>
        private void CreateCollectionActivities()
        {
            // Retrieve all collection activities
            var query = from activity in dB.tt_ActitvityGroup
                        join c in dB.School_Lookup_Class on activity.ClassId equals c.ClassId
                        join s in dB.Subject_SubjectGrade on activity.SubjectId equals s.SubjectID
                        join t in dB.HR_MasterData_Employees on activity.TeacherId equals t.EmployeeId
                        join grade in dB.School_Lookup_Grade on activity.GradeId equals grade.GradeId
                        join sub in dB.Subject_MasterData_Subject on activity.SubjectId equals sub.SubjectId
                        where s.GradeID == activity.GradeId && t.IsActive == true
                        group new { ActivityRefID = activity.ActivityRefId, teacherId = activity.TeacherId, grade.GradeName, subjectId = activity.SubjectId, c.ClassName, ClassID = c.ClassId, activity.Id, s.NumberOfLlessonsPerWeek, s.NumberOfLlessonsPerDay, s.CollectionID }
                        by activity.ActivityRefId into g
                        select g;

            // Iterate over collections
            foreach (var item in query)
            {
                var activity = item.First();
                var students = new Dictionary<string, int>();
                item.Select(x => new { x.ClassName, x.ClassID }).ToList().ForEach(x => students[x.ClassName] = x.ClassID);

                // Create all activities for this collection
                var activityBuilder = new ActivityBuilder
                {
                    StudentsList = students,
                    TeachersList = item.Select(x => (int)x.teacherId).ToList(),
                    SubjectId = activity.subjectId,
                    NumberOfLessonsPerDay = activity.NumberOfLlessonsPerDay,
                    NumberOfLessonsPerWeek = activity.NumberOfLlessonsPerWeek,
                    GradeName = activity.GradeName,
                    ActivityCounter = _counter,

                    // Activity is a collection if there are more than one items in a group
                    IsCollection = item.Count() > 1,
                    CollectionId = activity.CollectionID
                };

                activityBuilder.GetResults().ForEach(x => Activities.Add(x.Id, x));
                _counter = activityBuilder.ActivityCounter;
            }
        }

        /// <summary>
        /// Creates the single activities.
        /// </summary>
        private void CreateSingleActivities()
        {
            // Retrieve all single activities to be created
            var query = from activity in dB.School_ClassTeacherSubjects
                        join c in dB.School_Lookup_Class on activity.ClassId equals c.ClassId
                        join s in dB.Subject_SubjectGrade on activity.SubjectId equals s.SubjectID
                        join t in dB.HR_MasterData_Employees on activity.TeacherId equals t.EmployeeId
                        join grade in dB.School_Lookup_Grade on c.GradeId equals grade.GradeId
                        where c.GradeId == s.GradeID
                        select new { TeacherID = activity.TeacherId, SubjectID = activity.SubjectId, c.ClassName, ClassID = c.ClassId, s.NumberOfLlessonsPerWeek, s.NumberOfLlessonsPerDay };

            // Iterate over single activities
            foreach (var item in query.Distinct())
            {

                var activityBuilder = new ActivityBuilder
                {
                    StudentsList = new Dictionary<string, int> { { item.ClassName, item.ClassID } },
                    TeachersList = new List<int> { (int)item.TeacherID },
                    SubjectId = item.SubjectID,
                    NumberOfLessonsPerDay = item.NumberOfLlessonsPerDay,
                    NumberOfLessonsPerWeek = item.NumberOfLlessonsPerWeek,
                    IsCollection = false,
                    ActivityCounter = _counter
                };

                activityBuilder.GetResults().ForEach(x => Activities.Add(x.Id, x));
                _counter = activityBuilder.ActivityCounter;
            }
        }

        /// <summary>
        /// Merges the activities if they are in a collection
        /// </summary>
        private void CollectionMerge()
        {

            // Duplicate activities collection to allow modifications
            var clone = Activities.ToDictionary(entry => entry.Key, entry => entry.Value);

            // Select distinct collections from the collectionstring
            var query = clone.Values.Select(item => item.CollectionId).Distinct();

            foreach (var item in query)
            {
                var list = Activities.Values.Where(x => x.CollectionId.Equals(item));

                // Groups the lessons by the number of lesson of the week. 
                var group = from a in list
                            group a by a.NumberLessonOfWeek into g
                            select g;

                foreach (var i in group)
                {
                    var students = new Dictionary<string, int>();
                    var teachers = new List<int>();

                    // Select all students and teachers for this group
                    i.Select(x => x.Students).ToList().ForEach(l => students = students.Union(l).ToDictionary(s => s.Key, s => s.Value));
                    i.Select(x => x.Teachers).ToList().ForEach(teachers.AddRange);

                    // Create collection activity
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

                    // Remove the original activities
                    i.Select(x => x.Id).ToList().ForEach(x => Activities.Remove(x));
                    Activities.Add(act.Id, act);
                }
            }
        }
    }
}
