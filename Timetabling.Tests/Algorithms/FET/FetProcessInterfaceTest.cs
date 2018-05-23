using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using Timetabling.Algorithms.FET;
using Timetabling.Exceptions;
using Timetabling.Helper;

namespace Timetabling.Tests.Algorithms.FET
{

    class FetProcessInterfaceExposer : FetProcessInterface
    {
        public FetProcessInterfaceExposer(Process fetProcess) : base(fetProcess) { }
        public void CheckProcessExitCode() => base.CheckProcessExitCode();
    }

    [TestFixture]
    class FetProcessInterfaceTest
    {

        private Process _process;
        private FetProcessInterfaceExposer _fpi;

        [SetUp]
        public void Setup()
        {
            // Can't mock sealed class process, so create a real instance here
            var fpb = new FetProcessBuilder(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib", "fet", "fet-cl"));

            _process = fpb.CreateProcess();
            _fpi = new FetProcessInterfaceExposer(_process);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual(_process, _fpi.Process);
        }

        [Test]
        public void StartProcess()
        {

            // Call method
            _fpi.StartProcess();

            // Assertions
            Assert.IsFalse(_fpi.Process.HasExited);

            // Cleanup
            _fpi.TerminateProcess();

        }

        [Test]
        public void TerminateProcess()
        {
            _fpi.StartProcess();

            var expected = _fpi.Process.StartInfo;

            // Call method
            _fpi.TerminateProcess();

            // HasExited fails after process has been closed (why though??)
            // So when this throws an InvalidOperationException, the process has been terminated
            Assert.Throws<InvalidOperationException>(() =>
            {
                var test = _fpi.Process.HasExited;
            });

            // Verify that the ProcessStartInfo matches the original (one of the few unique bits of information left on the Process object)
            Assert.AreEqual(expected, _fpi.Process.StartInfo);

        }

        [Test]
        public void CheckProcessNotYetExitedCode()
        {

            _fpi.StartProcess();

            // Assertions
            var ex = Assert.Throws<InvalidOperationException>(() => _fpi.CheckProcessExitCode());
            Assert.AreEqual("The process has not yet exited.", ex.Message);

        }

        [Test]
        public void CheckProcessZeroExitCode()
        {

            // Create process again with different arguments
            var fpb = new FetProcessBuilder(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib", "fet", "fet-cl"));

            // Hopwood runs usually very fast
            fpb.SetInputFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "United-Kingdom", "Hopwood", "Hopwood.fet"));
            fpb.SetOutputDir(Util.CreateTempFolder("testIdentifier"));

            _process = fpb.CreateProcess();
            _fpi = new FetProcessInterfaceExposer(_process);

            // Start process
            _fpi.StartProcess();
            _fpi.Process.WaitForExit();
            
        }

        [Test]
        public void CheckProcessNonZeroExitCode()
        {

            // Create process again with different arguments
            var fpb = new FetProcessBuilder(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib", "fet", "fet-cl"));

            // Hopwood runs usually very fast
            fpb.SetInputFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "United-Kingdom", "Hopwood", "invalid.xml"));
            fpb.SetOutputDir(Util.CreateTempFolder("testIdentifier"));

            _process = fpb.CreateProcess();
            _fpi = new FetProcessInterfaceExposer(_process);

            // Start process
            _fpi.StartProcess();

            // Assertions
            var ex = Assert.Throws<AlgorithmException>(() =>
            {
                _fpi.Process.WaitForExit();
                _fpi.CheckProcessExitCode();
            });
            Assert.AreEqual("The FET process has exited with a non-zero exit code (1).", ex.Message);


        }

    }
}
