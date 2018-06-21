using System;
using System.Data;
using System.Data.Entity.Migrations;
using Timetabling.DB;
using Timetabling.Objects;

namespace Timetabling.Helper
{

    /// <summary>
    /// Helper functions for database interaction.
    /// </summary>
    public class DatabaseHelper : IDisposable
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Data model to work with.
        /// </summary>
        public DataModel Model { get; set; }

        /// <inheritdoc />
        public DatabaseHelper() : this(new DataModel()) { }

        /// <summary>
        /// Constructs a new DatabaseHelper on a DataModel.
        /// </summary>
        /// <param name="model">Data model to perform database operations on.</param>
        public DatabaseHelper(DataModel model) => Model = model;

        /// <summary>
        /// Save a timetable to the database.
        /// </summary>
        /// <param name="tt">Timetable object</param>
        /// <exception cref="InvalidOperationException">Throws InvalidOperationException when attempting to save a partial timetable.</exception>
        public void SaveTimetable(Timetable tt)
        {

            Logger.Info("Saving timetable.");

            // Cancel if timetable is partial
            if (tt.IsPartial) throw new InvalidOperationException("Partial timetables cannot be saved to the database.");

            // Use a new data model to track our changes
            using (var context = Model.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {

                // Create timetable entry and activities
                var id = CreateTimetable(Model, tt);

                // Create activities (including relations with teachers and classes) for this timetable
                CreateActivities(Model, id, tt);

                // Commit transaction
                context.Commit();
            }

        }

        /// <summary>
        /// Creates a new Timetable entry in the database.
        /// </summary>
        /// <param name="model">DataModel to work on.</param>
        /// <param name="tt">Timetable to save.</param>
        /// <returns>ID of newly inserted timetable.</returns>
        protected int CreateTimetable(DataModel model, Timetable tt)
        {

            Logger.Info("Creating timetable.");

            // Join soft conflicts into one string
            var conflictText = tt.SoftConflicts == null || tt.SoftConflicts.Count == 0 ? "" : string.Join(Environment.NewLine, tt.SoftConflicts.ToArray());

            // Create timetable entry
            var timetableEntry = new TimetableModel
            {
                Name = $"{tt.AcademicYearId} - {tt.QuarterId} - {tt.SectionId}",
                AcademicYearId = tt.AcademicYearId,
                QuarterId = tt.QuarterId,
                SectionId = model.StageIds?[0] ?? 0,
                ConflictWeight = tt.ConflictWeight,
                Conflicts = conflictText,
                OutputDir = tt.OutputFolder
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

            Logger.Info("Creating activities.");

            // Iterate over all activities in timetable
            foreach (var activity in tt.Activities)
            {

                // Create new activity
                var activityEntry = new TimetableActivityModel
                {
                    SubjectId = activity.Resource.Subject,
                    Day = GetDayFromString(activity.Day),
                    Timeslot = int.Parse(activity.Hour),

                    CollectionId = activity.Resource.CollectionId,
                    IsCollection = activity.Resource.IsCollection,

                    TimetableId = ttId
                };

                // Save activity to database
                model.TimetableActivities.Add(activityEntry);
                model.SaveChanges();

                Console.WriteLine(activityEntry.Id);

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
                model.TimetableActivityTeachers.AddOrUpdate(new TimetableActivityTeacherModel
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
                model.TimetableActivityClasses.AddOrUpdate(new TimetableActivityClassModel
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
            throw new ArgumentException("Supplied string value does not represent a valid Day.");
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Model?.Dispose();
            Model = null;
        }

    }
}
