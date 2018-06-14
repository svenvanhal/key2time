using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using Timetabling.Helper;

namespace Timetabling.Algorithms.FET
{

    /// <summary>
    /// Creates a new FET-CL process instance.
    /// </summary>
    internal class FetProcessBuilder
    {

        /// <summary>
        /// Path to the FET-CL executable.
        /// </summary>
        public readonly string ExecutableLocation;

        /// <summary>
        /// FET-CL command line arguments.
        /// </summary>
        protected readonly CommandLineArguments Arguments = new CommandLineArguments();

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
            { "warnifusingnotperfectconstraints", "false" },
            { "warnifusingstudentsminhoursdailywithallowemptydays", "false" },
            { "warnsubgroupswiththesameactivities", "false" }
        };

        private readonly IFileSystem _fs;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initialize builder.
        /// </summary>
        /// <param name="executableLocation"></param>
        public FetProcessBuilder(string executableLocation = null) : this(executableLocation, new FileSystem()) { }

        internal FetProcessBuilder(string executableLocation, IFileSystem fileSystem)
        {
            ExecutableLocation = executableLocation;
            _fs = fileSystem;
        }

        /// <summary>
        /// Creates a new Process.
        /// </summary>
        /// <returns>FET-CL process</returns>
        public Process CreateProcess()
        {
            var fetProcess = new Process
            {
                StartInfo = CreateStartInfo(),

                // Bubble process events (e.g. Exited)
                EnableRaisingEvents = true
            };

            return fetProcess;
        }

        /// <summary>
        /// Set the FET-CL input file.
        /// </summary>
        /// <param name="inputFile">Path to file.</param>
        public void SetInputFile(string inputFile)
        {
            if(!_fs.File.Exists(inputFile)) throw new FileNotFoundException("The FET-CL input file could not be found.");

            SetArgument("inputfile", inputFile);
        }

        /// <summary>
        /// Set the FET-CL output directory.
        /// </summary>
        /// <param name="outputDir">Path to directory.</param>
        public void SetOutputDir(string outputDir)
        {
            if (!_fs.Directory.Exists(outputDir)) throw new DirectoryNotFoundException("The FET-CL output directory does not exist.");

            SetArgument("outputdir", outputDir);
        }

        /// <summary>
        /// Set the timetable generation timeout.
        /// </summary>
        /// <param name="seconds">Amount of seconds before timeout. Default: 2000000000</param>
        public void SetTimeout(int seconds)
        {
            if (seconds < 1) throw new ArgumentOutOfRangeException(nameof(seconds), "The timetable generation timeout cannot be less than one second.");

            SetArgument("timelimitseconds", seconds.ToString());
        }

        /// <summary>
        /// Set the FET output language. Also applies to error messages.
        /// </summary>
        /// <param name="language">Language. Default: en_US.</param>
        public void SetLanguage(FetLanguage language)
        {
            if (language == null) throw new ArgumentOutOfRangeException(nameof(language), "Invalid language passed. Defaulting to en_US.");

            SetArgument("language", language.ToString());
        }


        /// <summary>
        /// Enable or disable verbose output.
        /// </summary>
        /// <param name="enable">Whether or not to enable verbose output. Default: false.</param>
        public void Debug(bool enable)
        {
            SetArgument("verbose", enable ? "true" : "false");
        }

        /// <summary>
        /// Create the metadata object to start a FET-CL process.
        /// </summary>
        /// <returns>A ProcessStartInfo object.</returns>
        protected ProcessStartInfo CreateStartInfo()
        {
            var startInfo = new ProcessStartInfo
            {
                // Suppress window
                UseShellExecute = false,

                // Set executable location and arguments
                FileName = ExecutableLocation,
                Arguments = DefaultArgs.Combine(Arguments).ToString(),

                // Redirect stdin, stdout
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };

            return startInfo;
        }

        /// <summary>
        /// Get a FET-CL command line argument.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <return>Value of the argument. Null if argument not set.</return>
        /// <exception cref="KeyNotFoundException">Throws exception if <paramref name="name"/> not found.</exception>
        protected string GetArgument(string name)
        {
            return Arguments[name];
        }

        /// <summary>
        /// Set a FET-CL command line argument.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <param name="value">Value of the argument. If null, the argument is removed.</param>
        protected void SetArgument(string name, string value)
        {
            Logger.Debug($"Process argument set with name {name} and value {value}.");

            if (value == null) Arguments.Remove(name);
            else Arguments[name] = value;
        }

    }
}
