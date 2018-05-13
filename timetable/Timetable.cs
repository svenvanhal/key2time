using System;
using Timetable.Algorithm;
using Timetable.Generator;

namespace Timetable
{
    public class Timetable
    {
        public static void Main(string[] args)
        {

            // Instantiate algorithm
            var fetAlgo = new FetAlgorithm("lib/fet/fet-cl");

            // Generate timetable
            TimetableGenerator.Generate(fetAlgo, "testdata/fet/Italy/2007/simple/simpler-italian.fet");

            // Keep console window open
            Console.Read();

        }
    }
}
