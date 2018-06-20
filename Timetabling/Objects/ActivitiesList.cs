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
            var query = from activity in dB.ClassTeacherSubjects
                        join c in dB.ClassesLookup on activity.ClassId equals c.ClassId
                        join s in dB.SubjectGrades on activity.SubjectId equals s.SubjectId
                        join t in dB.Employees on activity.TeacherId equals t.EmployeeId
                        join grade in dB.GradesLookup on c.GradeId equals grade.GradeId
                        join sub in dB.Subjects on activity.SubjectId equals sub.SubjectId
                        where s.GradeId == c.GradeId && t.IsActive == true && grade.StageId == dB.StageId && s.CollectionId != null
                        group new { teacherId = activity.TeacherId, grade.GradeId, subjectId = activity.SubjectId, c.ClassName, ClassID = c.ClassId, NumberOfLlessonsPerWeek = s.NumberOfLessonsPerWeek, NumberOfLlessonsPerDay = s.NumberOfLessonsPerDay, CollectionID = s.CollectionId }
            by new { s.CollectionId, s.GradeId, activity.ClassId } into g
                        select g;

            // Iterate over collections
            foreach (var item in query)
            {
                var activity = item.First();
                var students = new Dictionary<string, int>();
                item.Select(x => new { x.ClassName, x.ClassID, x.GradeId }).ToList().ForEach(x => students[x.ClassName] = x.ClassID);

                // Create all activities for this collection
                var activityBuilder = new ActivityBuilder
                {
                    StudentsList = students,
                    TeachersList = item.Select(x => (int)x.teacherId).ToList(),
                    SubjectId = activity.subjectId,
                    NumberOfLessonsPerDay = activity.NumberOfLlessonsPerDay,
                    NumberOfLessonsPerWeek = activity.NumberOfLlessonsPerWeek,
                    ActivityCounter = _counter,
                    GradeId = item.First().GradeId,
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
            var query = from activity in dB.ClassTeacherSubjects
                        join c in dB.ClassesLookup on activity.ClassId equals c.ClassId
                        join s in dB.SubjectGrades on activity.SubjectId equals s.SubjectId
                        join t in dB.Employees on activity.TeacherId equals t.EmployeeId
                        join grade in dB.GradesLookup on c.GradeId equals grade.GradeId
                        where c.GradeId == s.GradeId && grade.StageId == dB.StageId && s.CollectionId == null
                        select new { TeacherID = activity.TeacherId, GradeID = c.GradeId, SubjectID = activity.SubjectId, c.ClassName, ClassID = c.ClassId, NumberOfLlessonsPerWeek = s.NumberOfLessonsPerWeek, NumberOfLlessonsPerDay = s.NumberOfLessonsPerDay };

            // Iterate over single activities
            foreach (var item in query)
            {
                var activityBuilder = new ActivityBuilder
                {
                    StudentsList = new Dictionary<string, int> { { item.ClassName, item.ClassID } },
                    TeachersList = new List<int> { (int)item.TeacherID }.Distinct().ToList(),
                    SubjectId = item.SubjectID,
                    GradeId = (int)item.GradeID,
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

            // Select distinct collection id's
            var query = clone.Values.Select(item => item.CollectionId).Distinct();

            foreach (var item in query)
            {
                var list = Activities.Values.Where(x => x.CollectionId != 0 && x.CollectionId.Equals(item) && x.IsCollection==true);

                // Groups the lessons by the number of lesson of the week. 
                var group = from a in list
                            group a by new { a.NumberLessonOfWeek, a.GradeId } into g
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
