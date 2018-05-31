using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Timetabling.Helper;
using Timetabling.Resources;

namespace Timetabling.Algorithms.FET
{

    /// <inheritdoc />
    /// <summary>
    /// The FET-CL wrapper. Visit the <a href="https://lalescu.ro/liviu/fet/">official FET website</a> for more information about the program and the algorithm.
    /// </summary>
    public class FetAlgorithm : Algorithm
    {

        /// <summary>
        /// Current run identifier.
        /// </summary>
        protected internal string Identifier { get; set; }

        /// <summary>
        /// Name of the input file (filename without extension). FET-CL uses this as base for all generated output files.
        /// </summary>
        protected internal string InputName { get; set; }

        /// <summary>
        /// Path to the .fet input file.
        /// </summary>
        protected internal string InputFile
        {
            get => _inputFile;
            set
            {
                _inputFile = value;
                InputName = Path.GetFileNameWithoutExtension(value);
            }
        }

        /// <summary>
        /// Current run identifier.
        /// </summary>
        protected internal string OutputDir { get; private set; }

        /// <summary>
        /// FET-CL process interface.
        /// </summary>
        internal FetProcessInterface ProcessInterface;

        /// <summary>
        /// TaskCompletionSource to generate the algorithm execution task.
        /// </summary>
        private TaskCompletionSource<Timetable> _tcs;

        private string _inputFile;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        /// <inheritdoc />
        protected internal override async Task<Timetable> GenerateTask(string identifier, string input, CancellationToken t)
        {
            Identifier = identifier;
            _tcs = new TaskCompletionSource<Timetable>(t);

            // Initialize algorithm
            Initialize(input, t);

            await ProcessInterface.StartProcess()

                // Gather the Timetable results when the algorithm process has finished
                .ContinueWith(task => _tcs.TrySetResult(GetResult()), TaskContinuationOptions.NotOnFaulted)

                // Bubble exceptions
                .ContinueWith(task => _tcs.TrySetException(task.Exception), TaskContinuationOptions.OnlyOnFaulted);

            return await _tcs.Task;
        }

        /// <summary>
        /// Build FET process and create process interface.
        /// </summary>
        /// <param name="input">Path to input file</param>
        /// <param name="t">Cancellation token</param>
        protected internal void Initialize(string input, CancellationToken t)
        {
            Logger.Info("Initializing FET algorithm");

            // Set parameters
            InputFile = input;
            OutputDir = Util.CreateTempFolder(Identifier);

            // Initialize process builder
            var fetExecutablePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Util.GetAppSetting("FetExecutableLocation"));
            var processBuilder = new FetProcessBuilder(fetExecutablePath);

            // Configure process
            processBuilder.SetInputFile(InputFile);
            processBuilder.SetOutputDir(OutputDir);

            // Create process interface and register exit handler
            ProcessInterface = new FetProcessInterface(processBuilder.CreateProcess(), t);
        }

        /// <summary>
        /// Process FET algorithm output.
        /// </summary>
        /// <returns>Timetable</returns>
        protected internal Timetable GetResult()
        {
            Logger.Info("Retrieving FET algorithm results");

            var fop = new FetOutputProcessor(InputName, Path.Combine(OutputDir, "timetables"));
            return fop.GetTimetable();
        }

    }
}
