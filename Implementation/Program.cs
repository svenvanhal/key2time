using System;
using System.IO;
using System.Threading.Tasks;
using Timetabling;
using Timetabling.Algorithms;
using Timetabling.Algorithms.FET;
using Timetabling.DB;
using Timetabling.Resources;

namespace Implementation
{
    class Program
    {
        static void Main(string[] args)
        {

            // Example usage:
            //   1 - Instantiate and configure algorithm to use
            //   2 - Specify input data for algorithm
            //   3 - Create a TimetableGenerator
            //   4 - Let the TimetableGenerator generate a Task<Timetable>
            //   5 - Do something with the Timetable output object when the Task finishes


            var task = new Program().Start();

            task.ContinueWith(OnSuccess, TaskContinuationOptions.OnlyOnRanToCompletion);
            task.ContinueWith(OnCanceled, TaskContinuationOptions.OnlyOnCanceled);
            task.ContinueWith(OnError, TaskContinuationOptions.OnlyOnFaulted);

            task.Wait();

            Console.Read();
        }

        public Task<Timetable> Start()
        {

            // Generate input
            var inputGen = new FetInputGenerator(new DataModel());
            var inputFile = inputGen.GenerateFetFile(Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "fetInputGenerator")).FullName);
            var activities = inputGen.GetActivities();

            // Create algorithm task
            var generator = new TimetableGenerator();
            return generator.RunAlgorithm(new FetAlgorithm(), inputFile, activities);
        }

        public static void OnSuccess(Task<Timetable> t)
        {
            Console.WriteLine("The timetable has been generated sucessfully.");
            Console.WriteLine(t.Result);

            // Save to database here
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
