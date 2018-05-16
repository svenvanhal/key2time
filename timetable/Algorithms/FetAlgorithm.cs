using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Timetabling.Exceptions;
using Timetabling.Helper;
using Timetabling.Resources;

namespace Timetabling.Algorithm
{
    class FetAlgorithm : IAlgorithm
    {

        /// <summary>
        /// Location of the FET program.
        /// </summary>
        private readonly string executableLocation;

        /// <summary>
        /// FET-CL command line arguments.
        /// </summary>
        private CommandLineArguments args;

        /// <summary>
        /// Algorithm input file.
        /// </summary>
        private string inputFile;

        /// <summary>
        /// Algorithm output directory.
        /// </summary>
        private string outputDir;

        /// <summary>
        /// Unique string to identify the current run of the algorithm. The FET output is stored using this identifier.
        /// </summary>
        public string CurrentRunIdentifier { get; private set; }

        /// <summary>
        /// Instantiate new FET Algorithm instance.
        /// <param name="executableLocation">Location of the FET binary.</param>
        /// <param name="args">Additional command line arguments.</param>
        /// </summary>
        public FetAlgorithm(string executableLocation, CommandLineArguments args = null)
        {
            this.executableLocation = executableLocation;
            this.args = args ?? new CommandLineArguments();
        }

        /// <summary>
        /// Set a FET-CL command line argument.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <param name="value">Value of the argument. If null, the argument is removed.</param>
        public void SetArgument(string name, string value)
        {
            if (value == null)
            {
                args.Remove(name);
            }
            else
            {
                args[name] = value;
            }
        }

        /// <summary>
        /// Get a FET-CL command line argument.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <return>Value of the argument. Null if argument not set.</return>
        /// <exception cref="KeyNotFoundException">Throws exception if <paramref name="name"/> not found.</exception>
        public string GetArgument(string name)
        {
            return args[name];
        }

        /// <summary>
        /// Defines a new input file for the algorithm.
        /// </summary>
        /// <param name="inputFileLocation">Location of the FET input data file.</param>
        public void Initialize(string inputFileLocation)
        {

            // Create unique identifier
            RefreshIdentifier();

            inputFile = inputFileLocation;
            SetArgument("inputfile", inputFile);

            outputDir = Util.CreateTempFolder(CurrentRunIdentifier);
            SetArgument("outputdir", outputDir);
        }

        /// <summary>
        /// Executes the FET algorithm.
        /// </summary>
        /// <exception cref="AlgorithmException">Error message during algorithm execution.</exception>
        public void Run()
        {

            // Run FET
            StartProcess();

        }

        /// <summary>
        /// Fetches the FET output files and generates a Timetable object.
        /// </summary>
        /// <returns>A Timetable object.</returns>
        public Timetable GetResult()
        {
            return new Timetable();
        }

        /// <summary>
        /// Creates and starts a new FET process.
        /// </summary>
        private void StartProcess()
        {

            // Create new FET process
            Process fetProcess = CreateProcess();

            // Run the FET program
            try
            {
                fetProcess.Start();
                fetProcess.BeginOutputReadLine();

                Console.WriteLine("Started fet-cl with the following arguments:");
                Util.WriteError(fetProcess.StartInfo.Arguments);

                fetProcess.WaitForExit();

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
        /// Set-up process info for the FET program.
        /// </summary>
        /// <returns>ProcessStartInfo object</returns>
        private Process CreateProcess()
        {

            var startInfo = new ProcessStartInfo
            {

                // Hide window
                UseShellExecute = false,

                // Set executable location and arguments
                FileName = executableLocation,
                Arguments = ConstructCommandLineArguments(args),

                // Redirect stdout and stderr
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            var fetProcess = new Process
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true,
            };

            // Add listenerse
            fetProcess.OutputDataReceived += LogConsoleOutput;
            fetProcess.Exited += CheckProcessExitCode;

            return fetProcess;
        }

        private string ConstructCommandLineArguments(CommandLineArguments cla)
        {

            // Defaults (empty for now)
            var defaults = new CommandLineArguments();

            var arguments = defaults.Combine(cla);

            // Construct argument string
            var sb = new StringBuilder();
            foreach (KeyValuePair<string, string> arg in arguments)
            {
                sb.AppendFormat(
                    " --{0}={1}",
                    CommandLineArguments.EncodeArgument(arg.Key),
                    CommandLineArguments.EncodeArgument(arg.Value)
                );
            }
            return sb.ToString();

        }

        /// <summary>
        /// Generates a new identifier.
        /// </summary>
        private void RefreshIdentifier()
        {
            CurrentRunIdentifier = Guid.NewGuid().ToString("B");
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

                // TODO: Log console output lines here

            }

        }

        /// <summary>
        /// Checks the FET process exit code and throws an exception if the exit code is non-zero.
        /// </summary>
        /// <param name="sender">Originating process.</param>
        /// <param name="eventArgs">Event data.</param>
        private static void CheckProcessExitCode(object sender, EventArgs eventArgs)
        {

            var proc = (Process)sender;
            if (proc.HasExited && proc.ExitCode > 0)
            {
                throw new AlgorithmException("The FET process has exited with a non-zero exit code. Please check the logs for information about this error.");
            }

        }

    }
}
