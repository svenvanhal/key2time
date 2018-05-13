namespace Timetable.Algorithm
{

    /// <summary>
    /// Timetable generation algorithm interface.
    /// </summary>
    interface IAlgorithm
    {

        /// <summary>
        /// Runs the algorithm on a data set.
        /// 
        /// TODO: change string to DataRepository input argument, generate FET inputfile via FetAlgorithm class.
        /// </summary>
        /// <returns>A Timetable object.</returns>
        /// <exception cref="AlgorithmException">Error message during algorithm execution.</exception>
        Timetable Run(string inputFileLocation);

    }
}
