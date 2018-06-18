using System;
using System.Linq;
using System.Threading.Tasks;
using Timetabling;
using Timetabling.Algorithms.FET;
using Timetabling.DB;
using Timetabling.Helper;
using Timetabling.Resources;

namespace Implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // Get information about academic year, quarter and section
            var meta = GetMeta();

            // Start program by creating a Task<Timetable>
            var task = new Program().Start();

            // Attach handlers
            task.ContinueWith(t => OnSuccess(t, meta), TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(OnCanceled, TaskContinuationOptions.OnlyOnCanceled);
            task.ContinueWith(OnError, TaskContinuationOptions.OnlyOnFaulted);

            // Debug
            task.Wait();
            Console.Read();
        }

        public Task<Timetable> Start()
        {
            // Create algorithm task
            var generator = new TimetableGenerator();
            return generator.RunAlgorithm(new FetAlgorithm(), new DataModel());
        }

        private static int[] GetMeta()
        {
            using (var model = new DataModel())
            {

                // Get academic year id, section id and quarter id.
                var res = from row in model.AcademicQuarter
                          where row.IsActive == true && row.AcademicYearID != null && row.QuarterId != null && row.SectionId != null
                          select new[] { row.AcademicYearID ?? 0, row.QuarterId ?? 0, row.SectionId ?? 0 };

                return res.First();
            }
        }

        public static void OnSuccess(Task<Timetable> t, int[] meta)
        {

            var tt = t.Result;

            // Update timetable with metadata
            tt.AcademicYearId = meta[0];
            tt.QuarterId = meta[1];
            tt.SectionId = meta[2];

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
