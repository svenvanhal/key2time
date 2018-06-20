using System;
using System.Collections.Generic;

namespace Timetabling.Objects
{

    /// <summary>
    /// Generates a number of lessons for a single configuration.
    /// </summary>
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
        /// Gets or sets the number of lessons per Day.
        /// </summary>
        /// <value>The number of lessons per Day.</value>
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
        public int? CollectionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the grade.
        /// </summary>
        /// <value>The name of the grade.</value>
        public string GradeName { get; set; }

        /// <summary>
        /// Gets or sets the builder counter for the id.
        /// </summary>
        /// <value>The builder counter.</value>
        public int ActivityCounter { get; set; }

        /// <summary>
        /// Internal list to keep track of the generated activities.
        /// </summary>
        protected readonly List<Activity> Activities = new List<Activity>();

        /// <summary>
        /// Creates the activities and returns the result 
        /// </summary>
        /// <returns>The list of activities created.</returns>
        public List<Activity> GetResults()
        {
            CreateActivities();
            return Activities;
        }

        /// <summary>
        /// Adds the activity to the list.
        /// </summary>
        private void CreateActivities()
        {
            var groupId = ActivityCounter;
            var numberOfActivities = Math.Ceiling((double)NumberOfLessonsPerWeek / (double)NumberOfLessonsPerDay);
            // Generate the required amount of activities for this subject
            for (var i = 1; i <= numberOfActivities; i++)
            {
                var act = new Activity
                {
                    Teachers = TeachersList,
                    Subject = SubjectId,
                    Students = StudentsList,
                    Id = ActivityCounter,
                    GroupId = groupId,
                    Duration = NumberOfLessonsPerDay,
                    TotalDuration = NumberOfLessonsPerWeek,
                    NumberLessonOfWeek = i,
                    IsCollection = IsCollection,
                    CollectionId = CollectionId,
                };
              

                // Update the duration if this lesson is the final lesson on the week and not all hours have been filled
                if (i == numberOfActivities)
                {
                    var LastDuration = NumberOfLessonsPerWeek % NumberOfLessonsPerDay;
                  
                    act.Duration = LastDuration == 0 ? NumberOfLessonsPerDay : LastDuration;
                  

                }
                    // Store activity
                    Activities.Add(act);
                    ActivityCounter++;
                }
            }

        }
    }
