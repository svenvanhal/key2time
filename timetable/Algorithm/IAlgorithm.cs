using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Algorithm
{
    interface IAlgorithm
    {

        /// <summary>
        /// Executes the algorithm.
        /// 
        /// TODO: change string to DataRepository input argument, generate FET inputfile via FetAlgorithm class.
        /// </summary>
        /// <returns>A Timetable object.</returns>
        /// <exception cref="AlgorithmException">Error message during algorithm execution.</exception>
        Timetable Run(string inputFileLocation);

    }
}
