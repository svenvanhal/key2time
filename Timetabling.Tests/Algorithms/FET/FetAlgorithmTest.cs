using NUnit.Framework;
using System;
using System.IO;
using System.Threading;
using Timetabling.Algorithms.FET;
using Timetabling.Resources;

namespace Timetabling.Tests.Algorithms.FET
{

    internal class FetAlgorithmExposer : FetAlgorithm
    {
        public new void Initialize(string input, CancellationToken t) => base.Initialize(input, t);
        public new Timetable GetResult() => base.GetResult();
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

    }
}
