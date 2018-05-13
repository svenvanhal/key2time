using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable;
using Timetable.Exceptions;

namespace Timetable.Algorithm
{
    class FetAlgorithm : IAlgorithm
    {

        /// <summary>
        /// Location of the FET program.
        /// </summary>
        private string executableLocation;

        /// <summary>
        /// Prepares the input data for the FET algorithm.
        /// </summary>
        public FetAlgorithm(string executableLocation)
        {

            this.executableLocation = executableLocation;

        }

        /// <summary>
        /// Executes the GET algorithm.
        /// </summary>
        /// <returns>A Timetable object.</returns>
        /// <exception cref="AlgorithmException">Error message during algorithm execution.</exception>
        public Timetable Run(string inputFileLocation)
        {

            // Define additional arguments
            var args = new NameValueCollection()
            {
                { "inputfile", inputFileLocation },
                { "verbose", "true" }
            };

            // Create process info
            var startInfo = CreateProcessSettings(args);

            try
            {
                // Start the FET program
                using (Process proc = Process.Start(startInfo))
                {
                    proc.WaitForExit();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;

        }

        private ProcessStartInfo CreateProcessSettings(NameValueCollection args)
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
                startInfo.Arguments += String.Format(" --{0}={1}", Util.EncodeParameterArgument(item.key), Util.EncodeParameterArgument(item.value));
            }

            return startInfo;
        }

    }
}
