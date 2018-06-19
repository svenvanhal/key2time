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
            var data = new List<Subject_SubjectGrade>{
                new Subject_SubjectGrade{BuildingUnitTypeID = 1, GradeID = 1, SubjectID = 2
                },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Subject_SubjectGrade>>();
            mockSet.As<IQueryable<Subject_SubjectGrade>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Subject_SubjectGrade>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Subject_SubjectGrade>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Subject_SubjectGrade>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataClass = new List<School_BuildingsUnits>{
                new School_BuildingsUnits{ID =1, IsActive = true, TypeID = 1
                },
            }.AsQueryable();

            var mockSetClass = new Mock<DbSet<School_BuildingsUnits>>();
            mockSetClass.As<IQueryable<School_BuildingsUnits>>().Setup(m => m.Provider).Returns(dataClass.Provider);
            mockSetClass.As<IQueryable<School_BuildingsUnits>>().Setup(m => m.Expression).Returns(dataClass.Expression);
            mockSetClass.As<IQueryable<School_BuildingsUnits>>().Setup(m => m.ElementType).Returns(dataClass.ElementType);
            mockSetClass.As<IQueryable<School_BuildingsUnits>>().Setup(m => m.GetEnumerator()).Returns(dataClass.GetEnumerator());

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
