using NUnit.Framework;
using Moq;
using System.Data.Entity;
using Timetabling.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Timetabling.DB;

namespace Timetabling.Tests.Objects
{
    [TestFixture()]
    internal class TeachersListTest
    {

        XElement test;

        [SetUp]
        public void Init()
        {
            var data = new List<EmployeeModel>{
                new EmployeeModel{EmployeeId = 0, IsTeacher = true, IsActive = true},
                new EmployeeModel{EmployeeId = 1, IsTeacher = false, IsActive = true},
                new EmployeeModel{EmployeeId = 2, IsTeacher = true, IsActive = false},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<EmployeeModel>>();
            mockSet.As<IQueryable<EmployeeModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<EmployeeModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<EmployeeModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<EmployeeModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Employees).Returns(mockSet.Object);

            var list = new TeachersList(mockDB.Object);
            test = list.Create();
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Teachers_List", test.Name.ToString());
        }

        [Test]
        public void TeacherRightTest()
        {
            Assert.AreEqual(1, test.Elements("Teacher").Elements("Name").Count(item => item.Value.Equals("0")));
        }

        [Test]
        public void TeacherNotTeacher()
        {
            Assert.AreEqual(0, test.Elements("Teacher").Elements("Name").Count(item => item.Value.Equals("1")));
        }

        [Test]
        public void TeacherNotActive()
        {
            Assert.AreEqual(0, test.Elements("Teacher").Elements("Name").Count(item => item.Value.Equals("2")));
        }

    }
}
