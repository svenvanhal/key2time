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
        internal string Identifier { get; private set; }

        /// <summary>
        /// Name of the input file (filename without extension). FET-CL uses this as base for all generated output files.
        /// </summary>
        internal string InputName { get; private set; }

        /// <summary>
        /// Path to the .fet input file.
        /// </summary>
        internal string InputFile
        {
            get
            {
                return _inputFile;
            }
            set
            {
                _inputFile = value;
                InputName = Path.GetFileNameWithoutExtension(value);
            }
        }

        /// <summary>
        /// Current run identifier.
        /// </summary>
        internal string OutputDir { get; set; }

        /// <summary>
        /// FET-CL process interface.
        /// </summary>
        internal FetProcessInterface ProcessInterface;

        /// <summary>
        /// TaskCompletionSource to generate the algorithm execution task.
        /// </summary>
        internal TaskCompletionSource<Timetable> TaskCompletionSource;

        private string _inputFile;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        /// <inheritdoc />
        protected internal override async Task<Timetable> GenerateTask(string identifier, string input, CancellationToken t)
        {
            // Check if algorithm has been cancelled already
            if (t.IsCancellationRequested) t.ThrowIfCancellationRequested();

            Identifier = identifier;
            TaskCompletionSource = new TaskCompletionSource<Timetable>(t);

            // Initialize algorithm
            Initialize(input, t);
            
            await ProcessInterface.StartProcess()

                // Gather the Timetable results when the algorithm process has finished
                .ContinueWith(task => TaskCompletionSource.TrySetResult(GetResult()), t);

            return await TaskCompletionSource.Task;
        }

        /// <summary>
        /// Build FET process and create process interface.
        /// </summary>
        /// <param name="input">Path to input file</param>
        protected void Initialize(string input, CancellationToken t)
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
        protected Timetable GetResult()
        {
            Logger.Info("Retrieving FET algorithm results");

            var fop = new FetOutputProcessor(InputName, Path.Combine(OutputDir, "timetables", InputName));
            return fop.GetTimetable();
        }

    }
}
