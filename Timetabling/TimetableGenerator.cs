﻿using System;
using System.Threading;
using System.Threading.Tasks;
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

        protected readonly CancellationTokenSource TokenSource = new CancellationTokenSource();

        /// <inheritdoc />
        ~TimetableGenerator() => TokenSource.Dispose(); // Dispose of CancellationTokenSource on object destruction

        /// <summary>
        /// Run an algorithm on an input file.
        /// </summary>
        public Task<Timetable> RunAlgorithm(Algorithm algorithm, string inputfile)
        {
            Logger.Info($"Starting {algorithm.GetType().Name} algorithm run");

            // Generate new ID for this algorithm run
            RefreshIdentifier();

            // Generate timetable
            return algorithm.Execute(CurrentRunIdentifier, inputfile, TokenSource.Token);
        }

        /// <summary>
        /// Terminate the algorithm that is currently running.
        /// </summary>
        public void TerminateAlgorithm()
        {
            Logger.Info("Terminating algorithm");
            TokenSource.Cancel();
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
