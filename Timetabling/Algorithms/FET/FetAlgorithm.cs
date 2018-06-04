using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Timetabling.Config;
using Timetabling.Resources;

namespace Timetabling.Algorithms.FET
{

    /// <inheritdoc />
    /// <summary>
    /// The FET-CL wrapper. Visit the <a href="https://lalescu.ro/liviu/fet/">official FET website</a> for more information about the program and the algorithm.
    /// </summary>
    public class FetAlgorithm : TimetablingStrategy
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
        internal FetProcessFacade ProcessFacade;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private string _inputFile;
        private TaskCompletionSource<Timetable> _tcs;

        /// <inheritdoc />
        protected internal override async Task<Timetable> GenerateTask(string identifier, string input, CancellationToken t)
        {
            Identifier = identifier;
            _tcs = new TaskCompletionSource<Timetable>(t);

            // Initialize algorithm
            Initialize(input, t);

            // Create algorithm task
            await ProcessFacade.StartProcess()

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
            OutputDir = CreateOutputDirectory(Identifier);

            // Create process interface and register exit handler
            ProcessFacade = new FetProcessFacade(CreateProcess(), t);
        }

        /// <summary>
        /// Process FET algorithm output.
        /// </summary>
        /// <returns>Timetable</returns>
        protected internal Timetable GetResult()
        {
            Logger.Info("Retrieving FET algorithm results");

            var outputProcessor = new FetOutputProcessor(InputName, Path.Combine(OutputDir, "timetables"));
            return outputProcessor.GetTimetable();
        }

        /// <summary>
        /// Configure and create a new FET-CL process.
        /// </summary>
        /// <returns></returns>
        protected internal Process CreateProcess()
        {
            var fetExecutablePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TimetablingConfig.GetSetting("FetExecutableLocation"));
            var processBuilder = new FetProcessBuilder(fetExecutablePath);

            // Set input and output dir
            processBuilder.SetInputFile(InputFile);
            processBuilder.SetOutputDir(OutputDir);

            // Change default language
            processBuilder.SetLanguage(TimetablingConfig.GetFetLanguage());

            return processBuilder.CreateProcess();
        }

        /// <summary>
        /// Creates a temporary directory to store the intermediary algorithm output.
        /// </summary>
        /// <param name="outputDir">Name of the subdirectory.</param>
        /// <returns>Full path to the output directory.</returns>
        protected static string CreateOutputDirectory(string outputDir)
        {
            // Get working dir (default: %TEMP%/timetabling)
            var workingDir = TimetablingConfig.GetSetting("FetWorkingDir");
            if (string.IsNullOrEmpty(workingDir)) workingDir = Path.Combine(Path.GetTempPath(), "timetabling");

            // Create new directory and return path
            var dir = Directory.CreateDirectory(Path.Combine(workingDir, outputDir));
            return dir.FullName;
        }

    }
}
