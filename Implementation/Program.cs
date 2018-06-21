using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetabling;
using Timetabling.Algorithms.FET;
using Timetabling.DB;
using Timetabling.Helper;
using Timetabling.Objects;

namespace Implementation
{
    class Program
    {
        public static int StageId = 2;

        static void Main(string[] args)
        {

            // Get information about academic year, quarter and section
            var academicYear = GetAcademicYear();

            // Start program by creating a Task<Timetable>
            var task = new Program().Start();

            // Attach handlers
            task.ContinueWith(t => OnSuccess(t, academicYear, StageId, 0), TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(OnCanceled, TaskContinuationOptions.OnlyOnCanceled);
            task.ContinueWith(OnError, TaskContinuationOptions.OnlyOnFaulted);

            // Debug
            task.Wait();
        }

        public Task<Timetable> Start()
        {
            // Create algorithm task
            using (var model = new DataModel(StageId))
            using (var generator = new TimetableGenerator())
            {
                return generator.RunAlgorithm(new FetAlgorithm(), model);
            }

        }

        private static int GetAcademicYear()
        {
            using (var model = new DataModel(StageId))
            {

                // Get academic year id, section id and quarter id.
                var row = from ay in model.AcademicYear
                          where ay.IsActive == true
                          select ay.AcademicYearId;

                if (!row.Any()) throw new InvalidOperationException("No active academic year found.");

                return row.First();
            }
        }

        public static void OnSuccess(Task<Timetable> t, int academicYearId, int stageId, int quarterId)
        {

            var tt = t.Result;

            // Update timetable with metadata
            tt.AcademicYearId = academicYearId;
            tt.SectionId = stageId;
            tt.QuarterId = quarterId;

            Console.WriteLine("The timetable has been generated sucessfully.");
            Console.WriteLine(tt);

            // Save to database here
            using (var dbHelper = new DatabaseHelper())
            {
                dbHelper.SaveTimetable(tt);
            }

        }

        public static void OnError(Task<Timetable> t)
        {
            Console.WriteLine("The timetable could not be generated.");
            foreach (var ex in t.Exception.InnerExceptions) { Console.WriteLine(ex.Message); }
        }

        public static void OnCanceled(Task<Timetable> t)
        {
            Console.WriteLine("The timetable task has been canceled.");
        }

    }
}
