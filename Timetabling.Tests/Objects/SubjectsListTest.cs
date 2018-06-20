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
    internal class SubjectListTest
    {

        XElement test;

        [SetUp]
        public void Init()
        {
            var data = new List<SubjectModel>{
                new SubjectModel{SubjectId = 0,  IsActive = true},
                new SubjectModel{SubjectId = 1, IsActive = false},
                new SubjectModel{SubjectId = 2, IsActive = true},

            }.AsQueryable();

            var mockSet = new Mock<DbSet<SubjectModel>>();
            mockSet.As<IQueryable<SubjectModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<SubjectModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<SubjectModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<SubjectModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var data3 = new List<SubjectGradeModel>{
                new SubjectGradeModel{GradeId = 60, NumberOfLessonsPerWeek = 4, SubjectId =0, CollectionId = 1
                },
                new SubjectGradeModel{GradeId = 60, NumberOfLessonsPerWeek = 6, SubjectId =1
                },
            }.AsQueryable();

            var mockSet3 = new Mock<DbSet<SubjectGradeModel>>();
            mockSet3.As<IQueryable<SubjectGradeModel>>().Setup(m => m.Provider).Returns(data3.Provider);
            mockSet3.As<IQueryable<SubjectGradeModel>>().Setup(m => m.Expression).Returns(data3.Expression);
            mockSet3.As<IQueryable<SubjectGradeModel>>().Setup(m => m.ElementType).Returns(data3.ElementType);
            mockSet3.As<IQueryable<SubjectGradeModel>>().Setup(m => m.GetEnumerator()).Returns(data3.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.Subjects).Returns(mockSet.Object);
            mockDB.Setup(item => item.SubjectGrades).Returns(mockSet3.Object);

            var list = new SubjectsList(mockDB.Object);
            test = list.Create();
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Subjects_List", test.Name.ToString());
        }


        [Test]
        public void SubjectRightTest()
        {
            Assert.AreEqual(1, test.Elements("Subject").Elements("Name").Count(item => item.Value.Equals("0")));

        }

        [Test]
        public void SubjectNotInDB()
        {
            Assert.AreEqual(0, test.Elements("Subject").Elements("Name").Count(item => item.Value.Equals("4")));

        }

        [Test]
        public void SubjectNotActive()
        {
            Assert.AreEqual(0, test.Elements("Subject").Elements("Name").Count(item => item.Value.Equals("1")));

        }
    }
}
