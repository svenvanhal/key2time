using System;
using System.IO;
using Timetabling.Exceptions;
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
        public string Identifier { get; set; }

        /// <summary>
        /// Name of the input file (filename without extension). FET-CL uses this as base for all generated output files.
        /// </summary>
        public string InputName { get; private set; }

        /// <summary>
        /// Path to the .fet input file.
        /// </summary>
        public string InputFile
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
        public string OutputDir { get; protected set; }

        /// <summary>
        /// FET-CL process interface.
        /// </summary>
        public FetProcessInterface ProcessInterface;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private string _inputFile;

        /// <summary>
        /// Instantiate new FET Algorithm instance.
        /// <param name="args">Additional command line arguments.</param>
        /// </summary>
        public FetAlgorithm()
        {
        }

        /// <inheritdoc />
        public override Timetable Execute(string identifier, string input)
        {

            // Set identifier
            Identifier = identifier;

            // Initialize algorithm
            Initialize(input);

            // Run algorithm
            Run();

            // Get results
            return GetResult();

        }

        /// <summary>
        /// Interrupt algorithm run.
        /// </summary>
        public override void Interrupt()
        {
            ProcessInterface.TerminateProcess();
        }

        /// <summary>
        /// Defines a new input file for the algorithm.
        /// </summary>
        /// <param name="input">Location of the FET input data file.</param>
        protected override void Initialize(string input)
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
            processBuilder.Debug(true);

            // Create process
            var process = processBuilder.CreateProcess();

            // Create process interface
            ProcessInterface = new FetProcessInterface(process);

        }

        /// <summary>
        /// Executes the FET algorithm.
        /// </summary>
        /// <exception cref="AlgorithmException">Error message during algorithm execution.</exception>
        protected override void Run()
        {

            Logger.Info("Running FET algorithm");

            try
            {

                Logger.Info("Starting FET process");

                // Run the FET program
                ProcessInterface.StartProcess();

                ProcessInterface.Process.WaitForExit();

            }
            catch (Exception ex)
            {
                throw new AlgorithmException("Could not execute FET algorithm.", ex);
            }
            finally
            {
                ProcessInterface.TerminateProcess();
            }

        }

        /// <summary>
        /// Fetches the FET output files and generates a Timetable object.
        /// </summary>
        /// <returns>A Timetable object.</returns>
        protected override Timetable GetResult()
        {

            Logger.Info("Retrieving FET algorithm results");

            // Output is stored in the /timetables/[FET file name]/ folder

            var fop = new FetOutputProcessor(InputName, Path.Combine(OutputDir, "timetables", InputName));

            try
            {
                return fop.GetTimetable();
            }
            catch (FileNotFoundException ex)
            {
                throw new AlgorithmException("No timetable is generated: the FET output file could not be found.", ex);
            }

        }

    }
}
