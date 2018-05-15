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
        private string currentRunIdentifier;

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
        /// Defines a new input file for the algorithm.
        /// </summary>
        /// <param name="inputFileLocation">Location of the FET input data file.</param>
        public void Initialize(string inputFileLocation)
        {

            // Create unique identifier
            RefreshIdentifier();

            inputFile = inputFileLocation;
            SetArgument("inputfile", inputFile);

            outputDir = Util.CreateTempFolder(currentRunIdentifier);
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

                Console.WriteLine("Started fet-cl with the following arguments:");
                Util.WriteError(fetProcess.StartInfo.Arguments);

                fetProcess.WaitForExit();

                if (fetProcess.ExitCode != 0)
                {
                    Util.WriteError("Process exited with error code " + fetProcess.ExitCode);

                    // TODO: read from stderr/stdout to retrieve actual error message
                }

            }
            catch (InvalidOperationException)
            {
                Util.WriteError("Error: no filename provided or invalid StartProcessInfo arguments.");
                throw;
            }
            catch (Win32Exception)
            {
                Util.WriteError("Error: FET binary not found at location: " + fetProcess.StartInfo.FileName);
                throw;
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

            ProcessStartInfo startInfo = new ProcessStartInfo
            {

                // Hide window
                UseShellExecute = false,

                // Set executable location and arguments
                FileName = executableLocation,
                Arguments = ConstructCommandLineArguments(args)
            };

            Process fetProcess = new Process
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true
            };

            return fetProcess;
        }

        private string ConstructCommandLineArguments(CommandLineArguments cla)
        {

            // Defaults
            var defaults = new CommandLineArguments();

            var args = defaults.Combine(cla);

            // Construct argument string
            var sb = new StringBuilder();
            foreach (KeyValuePair<string, string> arg in args)
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
            currentRunIdentifier = Guid.NewGuid().ToString("B");
        }

    }
}
