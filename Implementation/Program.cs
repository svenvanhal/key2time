using Timetabling;
using Timetabling.Algorithms.FET;
using Timetabling.DB;

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
			var data = new DataModel();
			var input = new FetInputGenerator();

			input.GenerateFetFile(data, "");

            //var generator = new TimetableGenerator();
            //var timetable = generator.RunAlgorithm(algorithm, input);

        }
    }
}
