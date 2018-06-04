using System;
using System.IO;
using System.Threading.Tasks;
using Timetabling;
using Timetabling.Algorithms;
using Timetabling.Algorithms.FET;
using Timetabling.DB;

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

            TimetablingStrategy algorithm = new FetAlgorithm();

            // TODO: improve FET input file generation
            var inputFile = FetInputGenerator.GenerateFetFile(new DataModel(), Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "fetInputGenerator")).FullName);

            var generator = new TimetableGenerator();
            var task = generator.RunAlgorithm(algorithm, inputFile);

            // On success
            task.ContinueWith(t =>
            {
                Console.WriteLine("The timetable has been generated sucessfully.");
                Console.WriteLine(task.Result);
            }, TaskContinuationOptions.NotOnFaulted);

            // On error
            task.ContinueWith(t =>
            {
                Console.WriteLine("The timetable could not be generated.");
                foreach (var ex in t.Exception.InnerExceptions) { Console.WriteLine(ex.Message); }
            }, TaskContinuationOptions.OnlyOnFaulted);

            Console.Read();
        }
    }
}
