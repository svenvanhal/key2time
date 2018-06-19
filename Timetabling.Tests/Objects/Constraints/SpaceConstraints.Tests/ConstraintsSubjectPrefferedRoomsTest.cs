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
            var data = new List<SubjectGradeTable>{
                new SubjectGradeTable{BuildingUnitTypeID = 1, GradeID = 1, SubjectID = 2
                },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<SubjectGradeTable>>();
            mockSet.As<IQueryable<SubjectGradeTable>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<SubjectGradeTable>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<SubjectGradeTable>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<SubjectGradeTable>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataClass = new List<BuildingModel>{
                new BuildingModel{ID =1, IsActive = true, TypeId = 1
                },
            }.AsQueryable();

            var mockSetClass = new Mock<DbSet<BuildingModel>>();
            mockSetClass.As<IQueryable<BuildingModel>>().Setup(m => m.Provider).Returns(dataClass.Provider);
            mockSetClass.As<IQueryable<BuildingModel>>().Setup(m => m.Expression).Returns(dataClass.Expression);
            mockSetClass.As<IQueryable<BuildingModel>>().Setup(m => m.ElementType).Returns(dataClass.ElementType);
            mockSetClass.As<IQueryable<BuildingModel>>().Setup(m => m.GetEnumerator()).Returns(dataClass.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Subject_SubjectGrade).Returns(mockSet.Object);
            mockDB.Setup(item => item.School_BuildingsUnits).Returns(mockSetClass.Object);

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
