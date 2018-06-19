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
    internal class YearsListTest
    {

        XElement test;

        [SetUp]
        public void Init()
        {
            var data = new List<LookupGradeModel>{
                new LookupGradeModel{GradeId = 0,  GradeName = "test", IsActive = true},
                new LookupGradeModel{GradeId = 1, GradeName = "test2", IsActive = true},

            }.AsQueryable();

            var mockSet = new Mock<DbSet<LookupGradeModel>>();
            mockSet.As<IQueryable<LookupGradeModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<LookupGradeModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<LookupGradeModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<LookupGradeModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataClass = new List<LookupClassModel>{
                new LookupClassModel{GradeId = 0, ClassId = 0 ,IsActive = true, ClassName = "classTest1"},
                new LookupClassModel{GradeId = 1, ClassId = 1, IsActive = true, ClassName = "classTest2"},

            }.AsQueryable();

            var mockSetClass = new Mock<DbSet<LookupClassModel>>();
            mockSetClass.As<IQueryable<LookupClassModel>>().Setup(m => m.Provider).Returns(dataClass.Provider);
            mockSetClass.As<IQueryable<LookupClassModel>>().Setup(m => m.Expression).Returns(dataClass.Expression);
            mockSetClass.As<IQueryable<LookupClassModel>>().Setup(m => m.ElementType).Returns(dataClass.ElementType);
            mockSetClass.As<IQueryable<LookupClassModel>>().Setup(m => m.GetEnumerator()).Returns(dataClass.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.School_Lookup_Grade).Returns(mockSet.Object);
            mockDB.Setup(item => item.School_Lookup_Class).Returns(mockSetClass.Object);

            var list = new YearsList(mockDB.Object);
            test = list.Create();
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Students_List", test.Name.ToString());
        }


        [Test]
        public void ClassRightTest()
        {

            Assert.AreEqual(1, test.Elements("Year").Elements("Group").Elements("Name").Count(item => item.Value.Equals("classTest1")));
            Assert.AreEqual(0, test.Elements("Year").Elements("Group").Elements("Name").Count(item => item.Value.Equals("notclassTest1")));

        }

        [Test]
        public void GradeRightTest()
        {

            Assert.AreEqual(1, test.Elements("Year").Elements("Name").Count(item => item.Value.Equals("test")));
            Assert.AreEqual(0, test.Elements("Year").Elements("Name").Count(item => item.Value.Equals("noTtest")));

        }
    }
}
