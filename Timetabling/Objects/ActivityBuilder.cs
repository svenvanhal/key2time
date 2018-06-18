using System;
using System.Collections.Generic;

namespace Timetabling.Objects
{
    public class ActivityBuilder
    {
        public int SubjectId { get; set; }
        public Dictionary<string,int> StudentsList { get; set; }
        public List<int> TeachersList { get; set; }
        public bool IsCollection { get; set; }
        public int NumberOfLessonsPerDay { get; set; }
        public int NumberOfLessonsPerWeek { get; set; }
        public int? CollectionID { get; set; }
        public string GradeName { get; set; }
        public int builderCounter { get; set; }

        private List<Activity> activitiesList { set; get; } = new List<Activity>();

        /// <summary>
        /// Adds the activity to the list.
        /// </summary>
        private void CreateActivities()
        {

            var groupId = builderCounter;

            for (var i = 1; i <= NumberOfLessonsPerWeek; i++)
            {
                var act = new Activity
                {
                    Teachers = TeachersList,
                    Subject = SubjectId,
                    Students = StudentsList,
                    Id = builderCounter,
                    GroupId = groupId,
                    Duration = NumberOfLessonsPerDay,
                    TotalDuration = NumberOfLessonsPerWeek * NumberOfLessonsPerDay,
                    NumberLessonOfWeek = i,
                    IsCollection = IsCollection
                };

                if (IsCollection)
                {
                    act.SetCollection((int)CollectionID, GradeName);
                }

                activitiesList.Add(act);
                builderCounter++;
            }
        }

        public List<Activity> GetResults()
        {
            CreateActivities();
            return activitiesList;
        }
    }
}
