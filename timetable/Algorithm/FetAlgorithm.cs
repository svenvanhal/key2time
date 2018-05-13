using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Timetable.Algorithm
{
    class FetAlgorithm : IAlgorithm
    {

        /// <summary>
        /// Location of the FET program.
        /// </summary>
        private string executableLocation;

        /// <summary>
        /// Instantiate new FET Algorithm instance.
        /// <param name="executableLocation">Location of the FET binary.</param>
        /// </summary>
        public FetAlgorithm(string executableLocation)
        {
            this.executableLocation = executableLocation;
        }

        /// <summary>
        /// Executes the GET algorithm.
        /// </summary>
        /// <param name="inputFileLocation">Location of the FET input data file.</param>
        /// <returns>A Timetable object.</returns>
        /// <exception cref="AlgorithmException">Error message during algorithm execution.</exception>
        public Timetable Run(string inputFileLocation)
        {

            // Define additional arguments
            var args = new NameValueCollection()
            {
                { "inputfile", inputFileLocation },

                #if DEBUG
                { "verbose", "true" }
                #endif
            };

            // Create process info
            var startInfo = CreateProcessInfo(args);

            // Run FET
            StartProcess(startInfo);

            // TODO: read output files and construct timetable object
            var tt = new Timetable();

            return tt;

        }

        /// <summary>
        /// Creates and starts a new FET process.
        /// </summary>
        /// <param name="startInfo">Process information</param>
        private void StartProcess(ProcessStartInfo startInfo)
        {
            try
            {
                // Start the FET program
                using (Process proc = Process.Start(startInfo))
                {
                    proc.WaitForExit();
                }

            }
            catch (InvalidOperationException e1)
            {
                Util.WriteError("Error: no filename provided or invalid StartProcessInfo arguments.");
                throw e1;
            }
            catch (Win32Exception e2)
            {
                Util.WriteError("Error: FET binary not found.");
                throw e2;
            }
        }

        /// <summary>
        /// Set-up process info for the FET program.
        /// </summary>
        /// <param name="args">Command-line arguments</param>
        /// <returns>ProcessStartInfo object</returns>
        private ProcessStartInfo CreateProcessInfo(NameValueCollection args)
        {

            ProcessStartInfo startInfo = new ProcessStartInfo
            {

                // Hide window
                UseShellExecute = false,

                // Set executable location and required inputfile argument
                FileName = executableLocation
            };

            // Add command line parameters
            var items = args.AllKeys.SelectMany(args.GetValues, (k, v) => new { key = k, value = v });
            foreach (var item in items)
            {
                startInfo.Arguments += String.Format("--{0}={1}", Util.EncodeParameterArgument(item.key), Util.EncodeParameterArgument(item.value));
            }

            return startInfo;
        }

    }
}
