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
            var data = new List<School_Lookup_Grade>{
                new School_Lookup_Grade{GradeID = 0,  GradeName = "test", IsActive = true},
                new School_Lookup_Grade{GradeID = 1, GradeName = "test2", IsActive = true},

            }.AsQueryable();

            var mockSet = new Mock<DbSet<School_Lookup_Grade>>();
            mockSet.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var dataClass = new List<School_Lookup_Class>{
                new School_Lookup_Class{GradeID = 0, ClassID = 0 ,IsActive = true, ClassName = "classTest1"},
                new School_Lookup_Class{GradeID = 1, ClassID = 1, IsActive = true, ClassName = "classTest2"},

            }.AsQueryable();

            var mockSetClass = new Mock<DbSet<School_Lookup_Class>>();
            mockSetClass.As<IQueryable<School_Lookup_Class>>().Setup(m => m.Provider).Returns(dataClass.Provider);
            mockSetClass.As<IQueryable<School_Lookup_Class>>().Setup(m => m.Expression).Returns(dataClass.Expression);
            mockSetClass.As<IQueryable<School_Lookup_Class>>().Setup(m => m.ElementType).Returns(dataClass.ElementType);
            mockSetClass.As<IQueryable<School_Lookup_Class>>().Setup(m => m.GetEnumerator()).Returns(dataClass.GetEnumerator());

            var dataClassGroup = new List<Tt_ClassGroup>{
                new Tt_ClassGroup{groupName = "sub1",  classId = 1},
                new Tt_ClassGroup{groupName = "sub2", classId = 0},

            }.AsQueryable();

            var mockSetClassGroup = new Mock<DbSet<Tt_ClassGroup>>();
            mockSetClassGroup.As<IQueryable<Tt_ClassGroup>>().Setup(m => m.Provider).Returns(dataClassGroup.Provider);
            mockSetClassGroup.As<IQueryable<Tt_ClassGroup>>().Setup(m => m.Expression).Returns(dataClassGroup.Expression);
            mockSetClassGroup.As<IQueryable<Tt_ClassGroup>>().Setup(m => m.ElementType).Returns(dataClassGroup.ElementType);
            mockSetClassGroup.As<IQueryable<Tt_ClassGroup>>().Setup(m => m.GetEnumerator()).Returns(dataClassGroup.GetEnumerator());



            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.School_Lookup_Grade).Returns(mockSet.Object);
            mockDB.Setup(item => item.School_Lookup_Class).Returns(mockSetClass.Object);
            mockDB.Setup(item => item.Tt_ClassGroup).Returns(mockSetClassGroup.Object);



            var list = new YearsList(mockDB.Object);
            list.Create();

            test = list.GetList();
            System.Console.WriteLine(test);

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

        [Test]
        public void SubGroupRightTest()
        {
            Assert.AreEqual(1, test.Elements("Year").Elements("Group").Elements("Subgroup").Count(item => item.Value.Equals("sub1")));
            Assert.AreEqual(1, test.Elements("Year").Elements("Group").Elements("Subgroup").Count(item => item.Value.Equals("sub2")));

        }

    }
}
