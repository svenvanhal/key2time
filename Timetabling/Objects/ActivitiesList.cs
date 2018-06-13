﻿using Timetabling.DB;
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

                if (item.Count() == 1)
                    AddActivity(item.First(), studentsList, teachersList, false);
                else
                    AddActivity(item.First(), studentsList, teachersList, true);
            }
        }

        private void AddActivity(dynamic item, List<string> studentsList, List<int> teachersList, bool IsColl)
        {
            // Add activities

            var groupId = counter;

            for (var i = 1; i <= item.NumberOfLlessonsPerWeek; i++)
            {
                Activity act = new Activity();
                act.LessonGroupId = item.ActivityRefID;
                act.Teachers = teachersList;
                act.Subject = item.subjectId;
                act.Students = studentsList;
                act.Id = counter;
                act.GroupId = groupId;
                act.Duration = item.NumberOfLlessonsPerDay;
                act.TotalDuration = item.NumberOfLlessonsPerWeek * item.NumberOfLlessonsPerDay;
                act.NumberLessonOfWeek = i;
                act.IsCollection = IsColl;

                if (IsColl && item.CollectionID != null)
                    act.SetCollection(item.CollectionID, item.GradeName);
                
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

        private void CollectionMerge()
        {
            var clone = Activities.ToDictionary(entry => entry.Key,
                                               entry => entry.Value);
           
            var query = clone.Values.Where(item => item.CollectionString != "").Select(item => item.CollectionString).Distinct();
            foreach (var item in query)
            {
                var list = Activities.Values.Where(x => x.CollectionString.Equals(item));

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

                    Activity act = new Activity();
                    act.Teachers = teachers;
                    act.Students = students;
                    act.Id = i.First().Id; ;
                    act.GroupId = i.First().GroupId;
                    act.Duration = i.First().Duration;
                    act.TotalDuration = i.First().TotalDuration;
                    act.NumberLessonOfWeek = i.First().NumberLessonOfWeek;
                    act.IsCollection = true;
                    act.CollectionString = item;

                    i.Select(x => x.Id).ToList().ForEach(x => Activities.Remove(x));
                    Activities.Add(act.Id,act);
                }
            }
        }

    }

}
