using System;
using System.Collections.Generic;

namespace Timetabling.Objects
{
    public class ActivityBuilder
    {
        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>The subject identifier.</value>
        public int SubjectId { get; set; }

        /// <summary>
        /// Gets or sets the students list.
        /// </summary>
        /// <value>The students list.</value>
        public Dictionary<string, int> StudentsList { get; set; }

        /// <summary>
        /// Gets or sets the teachers list.
        /// </summary>
        /// <value>The teachers list.</value>
        public List<int> TeachersList { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the activity is a collection.
        /// </summary>
        /// <value><c>true</c> if is collection; otherwise, <c>false</c>.</value>
        public bool IsCollection { get; set; }

        /// <summary>
        /// Gets or sets the number of lessons per day.
        /// </summary>
        /// <value>The number of lessons per day.</value>
        public int NumberOfLessonsPerDay { get; set; }

        /// <summary>
        /// Gets or sets the number of lessons per week.
        /// </summary>
        /// <value>The number of lessons per week.</value>
        public int NumberOfLessonsPerWeek { get; set; }

        /// <summary>
        /// Gets or sets the collection identifier.
        /// </summary>
        /// <value>The collection identifier.</value>
        public int? CollectionID { get; set; }

        /// <summary>
        /// Gets or sets the name of the grade.
        /// </summary>
        /// <value>The name of the grade.</value>
        public string GradeName { get; set; }

        /// <summary>
        /// Gets or sets the builder counter for the id.
        /// </summary>
        /// <value>The builder counter.</value>
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
                    IsCollection = IsCollection,
                    CollectionId = CollectionID,
                };

                activitiesList.Add(act);
                builderCounter++;
            }
        }

        /// <summary>
        /// Creates the activities and returns the result 
        /// </summary>
        /// <returns>The list of activities created.</returns>
        public List<Activity> GetResults()
        {
            CreateActivities();
            return activitiesList;
        }
    }
}
