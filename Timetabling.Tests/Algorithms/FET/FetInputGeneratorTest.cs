using System;
using System.IO;
using NUnit.Framework;
using Timetabling.Algorithms.FET;
using Timetabling.Tests.Objects;

namespace Timetabling.Tests.Algorithms.FET
{
    class FetInputGeneratorTest
    {

        [Test]
        public void ConstructorTest()
        {
            var inputGenerator = new FetInputGenerator(null);

            // Verify that list objects are not null
            Assert.IsNotNull(inputGenerator.ActivitiesList);
            Assert.IsNotNull(inputGenerator.DaysList);
            Assert.IsNotNull(inputGenerator.HoursList);
            Assert.IsNotNull(inputGenerator.RoomsList);
            Assert.IsNotNull(inputGenerator.SpaceConstraintsList);
            Assert.IsNotNull(inputGenerator.SubjectsList);
            Assert.IsNotNull(inputGenerator.TeachersList);
            Assert.IsNotNull(inputGenerator.TimeConstraintsList);
            Assert.IsNotNull(inputGenerator.YearsList);
        }

        [Test]
        public void GetActivitiesTest()
        {
            var model = new TestDataModel().MockDataModel;
            var inputGenerator = new FetInputGenerator(model.Object);

            // Generate FET file
            Directory.CreateDirectory("testdir");
            inputGenerator.GenerateFetFile("testdir");
            Directory.Delete("testdir", true);

            // Verify that the activities are generated
            Assert.IsTrue(inputGenerator.GetActivities().Count > 0);
        }

        [Test]
        public void GetActivitiesNullTest()
        {
            var inputGenerator = new FetInputGenerator(null);

            // The activities can't be retrieved if the FET file has not been generated
            Assert.Throws<InvalidOperationException>(() => inputGenerator.GetActivities());
        }

        [Test]
        public void GenerateFetFileTest()
        {

            var model = new TestDataModel().MockDataModel;
            var inputGenerator = new FetInputGenerator(model.Object);

            Directory.CreateDirectory("testdir");
            var fetPath = inputGenerator.GenerateFetFile("testdir");
            Assert.IsTrue(File.Exists(fetPath));

            // Clean up
            Directory.Delete("testdir", true);
        }

    }
}
