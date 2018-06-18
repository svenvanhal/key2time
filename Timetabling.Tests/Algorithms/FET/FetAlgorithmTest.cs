using NUnit.Framework;
using System.IO;
using Timetabling.Algorithms.FET;

namespace Timetabling.Tests.Algorithms.FET
{

    [TestFixture]
    internal class FetAlgorithmTest
    {

        [Test]
        public void CreateProcessTest()
        {

            var path = FetAlgorithm.CreateOutputDirectory("test");
            var filepath = Path.Combine(path, "test.fet");
            using (File.Create(filepath))
            {

                var algo = new FetAlgorithm
                {
                    InputFile = filepath,
                    OutputDir = path
                };

                var proc = algo.CreateProcess();
                Assert.IsNotNull(proc);

            }

            File.Delete(filepath);
            Directory.Delete(path);
        }

        [Test]
        public void CreateOutputDirectoryTest()
        {
            var outputdir = FetAlgorithm.CreateOutputDirectory("test");
            Assert.IsTrue(Directory.Exists(outputdir));
            Directory.Delete(outputdir);
        }

    }
}
