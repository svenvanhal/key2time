using System;
using System.Threading.Tasks;
using Timetabling;
using Timetabling.Algorithms.FET;

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

            var algorithm = new FetAlgorithm();
            var input = @"inputfile.fet";

            var generator = new TimetableGenerator();
            var task = generator.RunAlgorithm(algorithm, input);

            // On success
            task.ContinueWith(t =>
            {
                Console.WriteLine("The timetable has been generated sucessfully.");
                Console.WriteLine(task.Result);
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            // On error
            task.ContinueWith(t =>
            {
                Console.WriteLine("The timetable could not be generated.");
                Console.WriteLine(t.Exception.Message);

            }, TaskContinuationOptions.OnlyOnFaulted);

            Console.Read();
        }
    }
}
