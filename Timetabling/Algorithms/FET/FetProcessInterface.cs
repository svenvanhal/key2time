using System;
using System.Diagnostics;
using Timetabling.Exceptions;

namespace Timetabling.Algorithms.FET
{

    /// <summary>
    /// Interfaces with the FET-CL command line program.
    /// </summary>
    public class FetProcessInterface
    {

        /// <summary>
        /// Bubble Process.Exited event
        /// </summary>
        public event EventHandler AlgorithmExited
        {
            add => Process.Exited += value;
            remove => Process.Exited -= value;
        }

        /// <summary>
        /// FET-CL process.
        /// </summary>
        protected readonly Process Process;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Create new process interface.
        /// </summary>
        /// <param name="fetProcess">FET-CL process</param>
        public FetProcessInterface(Process fetProcess)
        {
            Process = fetProcess;
        }

        /// <summary>
        /// Starts process and logs output.
        /// </summary>
        public virtual void StartProcess()
        {

            Logger.Info("Starting FET process");

            // Attach listeners to process
            Process.OutputDataReceived += Log;
            Process.Exited += (sender, args) => CheckProcessExitCode();

            // Start process
            if (!Process.Start()) throw new InvalidOperationException("Could not start FET-CL process.");
            Process.BeginOutputReadLine();

        }

        /// <summary>
        /// Wrapper around event delegate registration.
        /// <param name="e">Event handler.</param>
        /// </summary>
        public virtual void RegisterExitHandler(EventHandler e)
        {
            AlgorithmExited += e;
        }

        /// <summary>
        /// Gracefully stops process.
        /// </summary>
        public virtual void StopAlgorithm()
        {

            // Send SIGTERM

            // Close process

            // Wait 5 secs, else kill process

        }

        /// <summary>
        /// Kill process.
        /// </summary>
        public virtual void TerminateProcess()
        {
            Process.Kill();
        }

        /// <summary>
        /// Checks the FET process exit code and throws an exception if the exit code is non-zero.
        /// </summary>
        /// <exception cref="AlgorithmException">Throws AlgorithmException if non-zero error code.</exception>
        protected void CheckProcessExitCode()
        {
            // Check if process has exited
            if (!Process.HasExited) throw new InvalidOperationException("The process has not yet exited.");

            // Check exit code
            if (Process.ExitCode != 0) throw new AlgorithmException($"The FET process has exited with a non-zero exit code ({Process.ExitCode}).");
        }

        /// <summary>
        /// Logs FET console output line.
        /// </summary>
        /// <param name="sender">Originating process.</param>
        /// <param name="eventArgs">Event data.</param>
        protected void Log(object sender, DataReceivedEventArgs eventArgs)
        {
            var data = eventArgs.Data;
            if (!string.IsNullOrWhiteSpace(data)) Logger.Trace(data);
        }

    }
}
