using System;
using Timetabling.Algorithms;
using Timetabling.Resources;

namespace Timetabling
{

    /// <summary>
    /// Program entrypoint.
    /// </summary>
    public class TimetableGenerator
    {

        /// <summary>
        /// Unique string to identify the current run of an algorithm.
        /// </summary>
        public string CurrentRunIdentifier { get; private set; }

        /// <summary>
        /// Temporary method used for development purposes.
        /// </summary>
        public Timetable RunAlgorithm(Algorithm algorithm, string inputfile)
        {

            // Generate new ID for this algorithm run
            RefreshIdentifier();

            // Generate timetable
            var tt = algorithm.Execute(CurrentRunIdentifier, inputfile);

            return tt;
        }

        /// <summary>
        /// Generates a new identifier.
        /// </summary>
        public void RefreshIdentifier()
        {
            CurrentRunIdentifier = Guid.NewGuid().ToString("B");
        }


    }
}
