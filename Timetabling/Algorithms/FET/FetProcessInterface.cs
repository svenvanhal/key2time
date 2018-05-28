using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Timetabling.Exceptions;

namespace Timetabling.Algorithms.FET
{

    /// <summary>
    /// Interfaces with the FET-CL command line program.
    /// </summary>
    internal class FetProcessInterface
    {

        /// <summary>
        /// FET-CL process.
        /// </summary>
        protected readonly Process Process;

        /// <summary>
        /// Task source for process.
        /// </summary>
        protected readonly TaskCompletionSource<bool> TaskCompletionSource;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Create new process interface.
        /// </summary>
        /// <param name="fetProcess">FET-CL process</param>
        public FetProcessInterface(Process fetProcess, CancellationToken t)
        {
            // Check if process task has been cancelled already
            if (t.IsCancellationRequested) t.ThrowIfCancellationRequested();

            Process = fetProcess;

            TaskCompletionSource = new TaskCompletionSource<bool>(t);
            t.Register(StopProcess);
        }

        /// <summary>
        /// Starts process and logs output.
        /// </summary>
        public virtual Task StartProcess()
        {
            Logger.Info("Starting FET process");

            // Attach listeners to process
            Process.OutputDataReceived += Log;
            Process.Exited += (sender, args) =>
            {
                CheckProcessExitCode();

                // Process exited successfully
                TaskCompletionSource.TrySetResult(true);
            };

            // Start process
            if (!Process.Start()) TaskCompletionSource.TrySetException(new InvalidOperationException("Could not start FET-CL process."));
            Process.BeginOutputReadLine();

            return TaskCompletionSource.Task;
        }

        /// <summary>
        /// Gracefully stops process.
        /// </summary>
        public virtual void StopProcess()
        {

            // TODO: Send SIGTERM

            // TODO: Close process

            // TODO: Wait 5 secs, else kill process

            KillProcess();
        }

        /// <summary>
        /// Kill process.
        /// </summary>
        public virtual void KillProcess()
        {
            Process.Kill();
            Process.Dispose();
        }

        /// <summary>
        /// Checks the FET process exit code and throws an exception if the exit code is non-zero.
        /// </summary>
        /// <exception cref="AlgorithmException">Throws AlgorithmException if non-zero error code.</exception>
        protected void CheckProcessExitCode()
        {
            // Check if process has exited
            if (!Process.HasExited)
            {
                TaskCompletionSource.TrySetException(new InvalidOperationException("The process has not yet exited."));
            }

            // Check exit code
            if (Process.ExitCode != 0)
            {
                // TODO: check if this exception type is right
                TaskCompletionSource.TrySetException(new AlgorithmException($"The FET process has exited with a non-zero exit code ({Process.ExitCode})."));
            }
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
