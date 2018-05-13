using System;
using Timetable.Algorithm;

namespace Timetable
{
    public class Timetable
    {
        public static void Main(string[] args)
        {

            // Instantiate algorithm
            var fetAlgo = new FetAlgorithm("lib/fet/fet-cl");

            // Generate timetable
            Generate(fetAlgo, "testdata/fet/Italy/2007/simple/simpler-italian.fet");

            // Keep console window open
            Console.Read();

        }

        /// <summary>
        /// Run an algorithm on an inputfile to generate a timetable.
        /// </summary>
        /// <param name="algorithm">Any algorithm class implementing <see cref="IAlgorithm"/>IAlgorithm</param>
        /// <param name="inputFileLocation">Path to the input file.</param>
        /// <returns>Timetable object</returns>
        static Timetable Generate(IAlgorithm algorithm, string inputFileLocation) => algorithm.Run(inputFileLocation);
    }
}
