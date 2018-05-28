using NUnit.Framework;
using System;
using System.IO;
using System.Threading;
using Timetabling.Algorithms.FET;

namespace Timetabling.Tests.Algorithms.FET
{

    internal class FetAlgorithmExposer : FetAlgorithm
    {
        public new void Initialize(string input, CancellationToken t) => base.Initialize(input, t);
    }

    [TestFixture]
    internal class FetAlgorithmTest
    {

        [Test]
        public void IntegrationTest()
        {

            var fet = new FetAlgorithm();
            var task = fet.GenerateTask("testIdentifier",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "United-Kingdom", "Hopwood", "Hopwood.fet"),
                CancellationToken.None);

            task.Wait();

            Assert.IsNotNull(task.Result);

        }

        [Test]
        public void InitializeTest()
        {
            var algo = new FetAlgorithmExposer()
            {
                Identifier = "testIdentifier"
            };

            var expectedInputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata", "fet", "United-Kingdom", "Hopwood", "Hopwood.fet");
            
            algo.Initialize(expectedInputFile, CancellationToken.None);

            Assert.AreEqual(expectedInputFile, algo.InputFile);
            Assert.True(Directory.Exists(algo.OutputDir));
            Assert.IsInstanceOf<FetProcessInterface>(algo.ProcessInterface);
        }

        [Test]
        public void CancelPrematurelyTest()
        {
            var fet = new FetAlgorithm();
            var tcs = new CancellationTokenSource();
            var token = tcs.Token;

            tcs.Cancel();

            var task = fet.GenerateTask(null, null, token);

            var ex = Assert.Throws<AggregateException>(() => task.Wait());
            Assert.IsInstanceOf<OperationCanceledException>(ex.InnerException);
        }

    }
}
