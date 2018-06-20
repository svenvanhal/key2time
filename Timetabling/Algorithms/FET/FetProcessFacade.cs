using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Timetabling.Algorithms.FET
{

    /// <summary>
    /// Interfaces with the FET-CL command line program.
    /// </summary>
    /// <remarks>Inspired by <a href="https://gist.github.com/jvshahid/6fb2f91fa7fb1db23599">this gist</a>.</remarks>
    internal class FetProcessFacade
    {

        /// <summary>
        /// FET-CL process.
        /// </summary>
        protected readonly Process Process;

        /// <summary>
        /// Task source for process.
        /// </summary>
        protected internal readonly TaskCompletionSource<bool> TaskCompletionSource;

        [DllImport("Kernel32")]
        private static extern bool GenerateConsoleCtrlEvent(int dwCtrlEvent, uint dwProcessGroupId);

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(CtrlHandlerRoutine handler, bool add);

        // A delegate type to be used as the handler routine for SetConsoleCtrlHandler.
        private delegate bool CtrlHandlerRoutine(int ctrlCode);

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private bool _stopped;

        /// <summary>
        /// Create new process interface.
        /// </summary>
        /// <param name="fetProcess">FET-CL process</param>
        /// <param name="t">Cancellation token</param>
        public FetProcessFacade(Process fetProcess, CancellationToken t)
        {
            Process = fetProcess;

            TaskCompletionSource = new TaskCompletionSource<bool>(t);
            t.Register(StopProcess);
        }

        /// <summary>
        /// Starts process, handles result and logs stdout.
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

            try
            {
                // Set parent control handler to prevent terminating the entire program when generating a CtrlEvent
                SetConsoleCtrlHandler(_ => true, true);

                // Start process
                Process.Start();
                Process.BeginOutputReadLine();
            }
            catch (InvalidOperationException ex) { TaskCompletionSource.TrySetException(ex); }

            return TaskCompletionSource.Task;
        }

        /// <summary>
        /// Gracefully stops process. Kills process if it has not stopped after five seconds.
        /// </summary>
        public virtual void StopProcess()
        {
            Logger.Info("Stopping FET process");

            _stopped = true;

            // Send CTRL+BREAK event (code 1) to FET-CL and for the process to terminate
            SetConsoleCtrlHandler(null, true);
            GenerateConsoleCtrlEvent(1, 0);
            SetConsoleCtrlHandler(null, false);

            Process.WaitForExit(5000);

            // Return if process has exited. The task result will be set by the Exited handler.
            if (Process.HasExited) return;

            // If the process is still active after 5 seconds, force kill it
            TaskCompletionSource.SetException(new InvalidOperationException("The fet-cl process will be forcefully closed, because it did not exit gracefully within five seconds."));
            KillProcess();
        }

        /// <summary>
        /// Kill process.
        /// </summary>
        public virtual void KillProcess()
        {
            Logger.Info("Killing FET process");

            Process.Kill();
            Process.Dispose();
        }

        /// <summary>
        /// Checks the FET process exit code and throws an exception if the exit code is non-zero.
        /// </summary>
        protected void CheckProcessExitCode()
        {
            // Check if process has exited
            if (!Process.HasExited) TaskCompletionSource.TrySetException(new InvalidOperationException("The process has not yet exited."));

            // Check exit code
            if (Process.ExitCode != 0 && !_stopped) TaskCompletionSource.TrySetException(new InvalidOperationException($"The fet-cl process has exited with a non-zero exit code ({Process.ExitCode})."));
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
