using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Timetabling.DB;
using System.Linq;
using System.Data.Entity;
using Timetabling.Objects.Constraints.SpaceConstraints;

namespace Timetabling.Tests.Objects.Constraints.SpaceConstraints.Tests
{
    [TestFixture()]
    public class ConstraintSubjectPrefferedRoomsTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            var data = new List<SubjectGradeModel>{
                new SubjectGradeModel{BuildingUnitTypeId = 1, GradeId = 1, SubjectId = 2
                },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<SubjectGradeModel>>();
            mockSet.As<IQueryable<SubjectGradeModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<SubjectGradeModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<SubjectGradeModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<SubjectGradeModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataClass = new List<BuildingModel>{
                new BuildingModel{Id =1, IsActive = true, TypeId = 1
                },
            }.AsQueryable();

            var mockSetClass = new Mock<DbSet<BuildingModel>>();
            mockSetClass.As<IQueryable<BuildingModel>>().Setup(m => m.Provider).Returns(dataClass.Provider);
            mockSetClass.As<IQueryable<BuildingModel>>().Setup(m => m.Expression).Returns(dataClass.Expression);
            mockSetClass.As<IQueryable<BuildingModel>>().Setup(m => m.ElementType).Returns(dataClass.ElementType);
            mockSetClass.As<IQueryable<BuildingModel>>().Setup(m => m.GetEnumerator()).Returns(dataClass.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.SubjectGrades).Returns(mockSet.Object);
            mockDB.Setup(item => item.Buildings).Returns(mockSetClass.Object);

            test = mockDB;
        }

        [Test()]
        public void TestConstruct()
        {
            var constraint = new ConstraintSubjectPreferredRooms();
            Assert.AreEqual("ConstraintSubjectPreferredRooms", constraint.ToXelement().Name.ToString());
        }


        [Test()]
        public void TestCreate(){
            var constraint = new ConstraintSubjectPreferredRooms();
            var constraintTest = new ConstraintSubjectPreferredRooms {Rooms = {1}, SubjectID = 2 };
            var constraintTest2 = new ConstraintSubjectPreferredRooms { Rooms = { 1 }, SubjectID = 3 };

            var result = constraint.Create(test.Object);
            Assert.AreEqual(1, result.Count(item => item.ToString().Equals(constraintTest.ToXelement().ToString())));
            Assert.AreEqual(0, result.Count(item => item.ToString().Equals(constraintTest2.ToXelement().ToString())));
        }
    }
}
