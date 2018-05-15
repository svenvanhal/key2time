using Timetabling.Exceptions;
using Timetabling.Resources;

namespace Timetabling.Algorithm
{

    /// <summary>
    /// Timetable generation algorithm interface.
    /// </summary>
    interface IAlgorithm
    {

        /// <summary>
        /// Algorithm initialization phase.
        /// </summary>
        /// <param name="input">Input to run the algorithm on.</param>
        void Initialize(string input);

        /// <summary>
        /// Runs the algorithm on a data set. Implementation should be asynchronous.
        /// 
        /// TODO: change string to DataRepository input argument, generate FET inputfile via FetAlgorithm class.
        /// </summary>
        /// <exception cref="AlgorithmException">Thrown when an error occurred during algorithm execution.</exception>
        void Run();

        /// <summary>
        /// Generates a <see cref="Timetable"/> from the algorithm output.
        /// </summary>
        /// <returns>A Timetable object.</returns>
        Timetable GetResult();
    }
}
