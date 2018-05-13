using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Algorithm;

namespace Timetable.Generator
{
    class TimetableGenerator
    {

        public static Timetable Generate(IAlgorithm algorithm, string inputFileLocation) => algorithm.Run(inputFileLocation);

    }
}
