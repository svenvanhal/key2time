using System;
using System.Threading;
using System.Threading.Tasks;
using Timetabling;
using Timetabling.Algorithms.FET;
using Timetabling.Resources;

namespace Implementation
{
    class Program
    {
        static void Main(string[] args)
        {

            // Example usage:
            //  1 - Instantiate and configure algorithm to use
            //  2 - Specify input data for algorithm
            //  3 - Create a TimetableGenerator
            //  3 - Run TimetableGenerator with algoritm and input 

            var algorithm = new FetAlgorithm();
            //var input = @"C:\Users\CodeSupply\Downloads\fet-5.35.6\examples\Italy\2007\difficult\highschool-Ancona.fet";
            var input = @"C:\Users\CodeSupply\Downloads\fet-5.35.6\examples\United-Kingdom\Hopwood\Hopwood.fet";

            var generator = new TimetableGenerator();
            var task = generator.RunAlgorithm(algorithm, input);
            task.Start();
            task.Wait();


        }
    }
}
