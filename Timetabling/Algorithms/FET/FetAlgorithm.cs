using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Timetabling.DB;
using Timetabling.Exceptions;
using Timetabling.Helper;
using Timetabling.Resources;

namespace Timetabling.Algorithms.FET
{

    /// <summary>
    /// The FET-CL wrapper. Visit the <a href="https://lalescu.ro/liviu/fet/">official FET website</a> for more information about the program and the algorithm.
    /// </summary>
    public class FetAlgorithm : Algorithm
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Location of the FET program.
        /// </summary>
        private static readonly string ExecutableLocation = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Helper.Util.GetAppSetting("FetBinaryLocation"));

        /// <summary>
        /// Algorithm .fet input file.
        /// </summary>
        protected string InputFile;

        /// <summary>
        /// Algorithm output directory.
        /// </summary>
        protected string OutputDir;

        /// <summary>
        /// FET-CL command line arguments.
        /// </summary>
        protected readonly CommandLineArguments Arguments;

        /// <summary>
        /// FET-CL default command line arguments. Limits the output to only the necessary.
        /// </summary>
        protected static readonly CommandLineArguments DefaultArgs = new CommandLineArguments
        {
            { "htmllevel", "0" },
            { "writetimetablesdayshorizontal", "false" },
            { "writetimetablesdaysvertical", "false" },
            { "writetimetablestimehorizontal", "false" },
            { "writetimetablestimevertical", "false" },
            { "writetimetablessubgroups", "false" },
            { "writetimetablesgroups", "false" },
            { "writetimetablesyears", "false" },
            { "writetimetablesteachers", "false" },
            { "writetimetablesteachersfreeperiods", "false" },
            { "writetimetablesrooms", "false" },
            { "writetimetablessubjects", "false" },
            { "verbose", "true" },
        };

        /// <summary>
        /// Instantiate new FET Algorithm instance.
        /// <param name="executableLocation">Location of the FET binary.</param>
        /// <param name="args">Additional command line arguments.</param>
        /// </summary>
        public FetAlgorithm(CommandLineArguments args = null)
        {
            Arguments = args ?? new CommandLineArguments();
        }

        /// <summary>
        /// Set a FET-CL command line argument.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <param name="value">Value of the argument. If null, the argument is removed.</param>
        public void SetArgument(string name, string value)
        {
            if (value == null) Arguments.Remove(name);
            else Arguments[name] = value;
        }

        /// <summary>
        /// Get a FET-CL command line argument.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <return>Value of the argument. Null if argument not set.</return>
        /// <exception cref="KeyNotFoundException">Throws exception if <paramref name="name"/> not found.</exception>
        public string GetArgument(string name)
        {
            return Arguments[name];
        }

        /// <inheritdoc />
        public override Timetable Execute(string identifier, string input)
        {

            // Set output dir
            OutputDir = Util.CreateTempFolder(identifier);

            Initialize(input);
            Run();

            try
            {
                return GetResult();
            }
            catch (FileNotFoundException ex)
            {
                throw new AlgorithmException("No timetable is generated: the FET output file could not be found.", ex);
            }

        }

        /// <summary>
        /// Defines a new input file for the algorithm.
        /// </summary>
        /// <param name="input">Location of the FET input data file.</param>
        protected override void Initialize(string input)
        {

            Logger.Info("Initializing FET algorithm");

            InputFile = input;

            SetArgument("inputfile", InputFile);
            SetArgument("outputdir", OutputDir);

            Logger.Debug("Inputfile: " + InputFile);
            Logger.Debug("Outputdir: " + OutputDir);
        }

        /// <summary>
        /// Executes the FET algorithm.
        /// </summary>
        /// <exception cref="AlgorithmException">Error message during algorithm execution.</exception>
        protected override void Run()
        {

            Logger.Info("Running FET algorithm");
            Logger.Debug("FET-CL executable location: " + ExecutableLocation);

            // Create new FET process
            var fetProcess = CreateProcess();

            try
            {

                Logger.Info("Starting FET process");

                // Run the FET program
                fetProcess.Start();
                fetProcess.BeginOutputReadLine();
                fetProcess.WaitForExit();

                // Verify that FET executed successfully
                CheckProcessExitCode(fetProcess.ExitCode);

            }
            catch (Exception ex)
            {
                throw new AlgorithmException("Could not execute FET algorithm.", ex);
            }
            finally
            {
                fetProcess.Dispose();
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
            var inputName = Path.GetFileNameWithoutExtension(InputFile); // TODO might need this name at more locations, so could make a classvar at the start

            var fop = new FetOutputProcessor(inputName, Path.Combine(OutputDir, "timetables", inputName));
            return fop.GetTimetable();

        }

        /// <summary>
        /// Set-up process info for the FET program.
        /// </summary>
        /// <returns>ProcessStartInfo object</returns>
        private Process CreateProcess()
        {

            Logger.Info("Creating FET process");

            var startInfo = new ProcessStartInfo
            {

                // Hide window
                UseShellExecute = false,

                // Set executable location and arguments
                FileName = ExecutableLocation,
                Arguments = DefaultArgs.Combine(Arguments).ToString(),

                // Redirect stdout and stderr
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var fetProcess = new Process
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true
            };

            // Add listeners
            fetProcess.OutputDataReceived += LogConsoleOutput;

            Logger.Debug("Process arguments: " + startInfo.Arguments);

            return fetProcess;
        }

        /// <summary>
        /// Logs FET console output line.
        /// </summary>
        /// <param name="sender">Originating process.</param>
        /// <param name="eventArgs">Event data.</param>
        private static void LogConsoleOutput(object sender, DataReceivedEventArgs eventArgs)
        {

            var data = eventArgs.Data;
            if (!string.IsNullOrWhiteSpace(data))
            {
                Logger.Debug(data);
            }

        }

        /// <summary>
        /// Checks the FET process exit code and throws an exception if the exit code is non-zero.
        /// </summary>
        /// <param name="exitCode">The exit code of a process.</param>
        /// <exception cref="AlgorithmException">Throws AlgorithmException if non-zero error code.</exception>
        private static void CheckProcessExitCode(int exitCode)
        {
            if (exitCode != 0) throw new AlgorithmException($"The FET process has exited with a non-zero exit code ({exitCode}).");
        }

    }
}
