using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Timetable.Algorithm;

namespace Timetable
{
    public class Timetable
    {
        public static void Main(string[] args)
        {

            var fetPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Util.GetAppSetting("FetBinaryLocation"));

            // Instantiate algorithm
            var fetAlgo = new FetAlgorithm(fetPath);

            // Generate timetable
            Generate(fetAlgo, "testdata/fet/United-Kingdom/Hopwood/Hopwood.fet");

#if DEBUG
            // Keep console window open
            Console.Read();
#endif
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
