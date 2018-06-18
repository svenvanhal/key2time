using System;
using System.Collections.Generic;
using Timetabling.DB;
using Timetabling.Objects;
using Timetabling.Resources;

namespace Timetabling.Helper
{

    /// <summary>
    /// Helper functions for database interaction.
    /// </summary>
    public class DatabaseHelper
    {

        /// <summary>
        /// Save a timetable to the database.
        /// </summary>
        /// <param name="tt">Timetable object</param>
        public void SaveTimetable(Timetable tt)
        {

            // Use a new data model to track our changes
            using (var model = new DataModel())
            {
                // Create timetable entry
                var id = CreateTimetable(model, tt);

                // Create activities
                CreateActivities(model, id, tt);
            }

        }

        /// <summary>
        /// Creates a new Timetable entry in the database.
        /// </summary>
        /// <param name="model">DataModel to work on.</param>
        /// <param name="tt">Timetable to save.</param>
        /// <returns>Id of newly inserted timetable.</returns>
        protected int CreateTimetable(DataModel model, Timetable tt)
        {

            // Create timetable entry
            var timetableEntry = new TimetableTable
            {
                Name = "Test Timetable",
                CreationDate = DateTime.Now,
                AcademicYearId = 0,
                QuarterId = 0,
                SectionId = 0
            };

            // Save to database
            model.Timetables.Add(timetableEntry);
            model.SaveChanges();

            return timetableEntry.Id;
        }

        /// <summary>
        /// Create activity entries for a timetable in the database.
        /// </summary>
        /// <param name="model">Data model</param>
        /// <param name="ttId">Timetable to link the activities to</param>
        /// <param name="tt">Timetable object</param>
        protected void CreateActivities(DataModel model, int ttId, Timetable tt)
        {

            // Iterate over all activities in timetable
            foreach (var activity in tt.Activities)
            {

                // Create new activity
                var activityEntry = new TimetableActivityTable
                {
                    SubjectId = activity.Resource.Subject,
                    Day = GetDayFromString(activity.Day),
                    Timeslot = GetHourFromString(activity.Hour),

                    CollectionId = activity.Resource.CollectionId,
                    IsCollection = activity.Resource.IsCollection,

                    TimetableId = ttId
                };

                // Save activity to database
                model.TimetableActivities.Add(activityEntry);
                model.SaveChanges();

                // Create activity - teacher relations
                CreateActivityTeacherRelations(model, activityEntry.Id, activity.Resource);

                // Create activity - class relations
                CreateActivityClassRelations(model, activityEntry.Id, activity.Resource);

            }

        }

        /// <summary>
        /// Add teachers to activities by creating activity - teacher relations.
        /// </summary>
        /// <param name="model">Data model</param>
        /// <param name="activityId">ID of activity.</param>
        /// <param name="activity">Source activity.</param>
        protected void CreateActivityTeacherRelations(DataModel model, int activityId, Activity activity)
        {

            // Iterate over teachers in activity
            foreach (var teacherId in activity.Teachers)
            {

                // Create new activity - teacher relation
                model.TimetableActitvityTeachers.Add(new TimetableActivityTeacherTable
                {
                    ActivityId = activityId,
                    TeacherId = teacherId
                });
            }

            // Save changes to database
            model.SaveChanges();

        }

        /// <summary>
        /// Add classes to activities by creating activity - class relations.
        /// </summary>
        /// <param name="model">Data model</param>
        /// <param name="activityId">ID of activity.</param>
        /// <param name="activity">Source activity.</param>
        protected void CreateActivityClassRelations(DataModel model, int activityId, Activity activity)
        {

            // Iterate over teachers in activity
            foreach (var classEntry in activity.Students)
            {

                // Create new activity - teacher relation
                model.TimetableActitvityClasses.Add(new TimetableActivityClassTable
                {
                    ActivityId = activityId,
                    ClassId = classEntry.Value
                });
            }

            // Save changes to database
            model.SaveChanges();

        }

        /// <summary>
        /// Parse string representation of Days to integer.
        /// </summary>
        /// <param name="day">String representation of Days, e.g. "Monday".</param>
        /// <returns>Integer value of Days enum corresponding to the input string.</returns>
        public static int GetDayFromString(string day)
        {
            // Try to parse string to enum and return result
            if (Enum.TryParse(day, true, out Days result)) return (int)result;

            // Throw exception if failed
            throw new ArgumentException("Supplied string value does not represent a valid day.");
        }

        /// <summary>
        /// Parse string representation of hour to integer.
        /// </summary>
        /// <param name="hour">String representation of hour.</param>
        /// <returns>Integer value of hour.</returns>
        public int GetHourFromString(string hour) => int.Parse(hour);
    }
}
