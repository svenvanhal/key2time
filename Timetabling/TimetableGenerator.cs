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

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Unique string to identify the current run of an algorithm.
        /// </summary>
        public string CurrentRunIdentifier { get; private set; }

        /// <summary>
        /// Temporary method used for development purposes.
        /// </summary>
        public Timetable RunAlgorithm(Algorithm algorithm, string inputfile)
        {

            Logger.Info($"Starting algorithm run - {algorithm.GetType()}");

            // Generate new ID for this algorithm run
            RefreshIdentifier();

            // Generate timetable
            var tt = algorithm.Execute(CurrentRunIdentifier, inputfile);

            return tt;
        }

        /// <summary>
        /// Temporary method used for development purposes.
        /// </summary>
        public void StopAlgorithm(Algorithm algorithm)
        {
            algorithm.Interrupt();
        }

        /// <summary>
        /// Generates a new identifier.
        /// </summary>
        public void RefreshIdentifier()
        {
            CurrentRunIdentifier = Guid.NewGuid().ToString("B");

            Logger.Info($"Generated new identifier - {CurrentRunIdentifier}");
        }


    }
}
