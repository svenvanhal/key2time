﻿using NUnit.Framework;
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
    internal class ActivitiesListTest
    {

        XElement test;

        [SetUp]
        public void Init()
        {

            var data = new List<School_TeacherClass_Subjects>{
                new School_TeacherClass_Subjects{ ClassID = 1, SubjectID = 1,  TeacherID = 0},
                new School_TeacherClass_Subjects{ ClassID = 3, SubjectID = 1,  TeacherID = 0, ID =1},
                new School_TeacherClass_Subjects{ ClassID = 2, SubjectID = 0,  TeacherID = 4},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<School_TeacherClass_Subjects>>();
            mockSet.As<IQueryable<School_TeacherClass_Subjects>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<School_TeacherClass_Subjects>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<School_TeacherClass_Subjects>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<School_TeacherClass_Subjects>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var data2 = new List<School_Lookup_Class>{
                new School_Lookup_Class{ClassName = "test", ClassID = 1, GradeID = 60},
                new School_Lookup_Class{ClassName = "test2", ClassID = 2, GradeID = 60},
                new School_Lookup_Class{ClassName = "test3", ClassID = 3, GradeID = 60},
            }.AsQueryable();

            var mockSet2 = new Mock<DbSet<School_Lookup_Class>>();
            mockSet2.As<IQueryable<School_Lookup_Class>>().Setup(m => m.Provider).Returns(data2.Provider);
            mockSet2.As<IQueryable<School_Lookup_Class>>().Setup(m => m.Expression).Returns(data2.Expression);
            mockSet2.As<IQueryable<School_Lookup_Class>>().Setup(m => m.ElementType).Returns(data2.ElementType);
            mockSet2.As<IQueryable<School_Lookup_Class>>().Setup(m => m.GetEnumerator()).Returns(data2.GetEnumerator());

            var data3 = new List<Subject_SubjectGrade>{
                new Subject_SubjectGrade{GradeID = 60, NumberOfLlessonsPerWeek = 4, SubjectID =1
                },
                new Subject_SubjectGrade{GradeID = 60, NumberOfLlessonsPerWeek = 6, SubjectID =0
                },
            }.AsQueryable();

            var mockSet3 = new Mock<DbSet<Subject_SubjectGrade>>();
            mockSet3.As<IQueryable<Subject_SubjectGrade>>().Setup(m => m.Provider).Returns(data3.Provider);
            mockSet3.As<IQueryable<Subject_SubjectGrade>>().Setup(m => m.Expression).Returns(data3.Expression);
            mockSet3.As<IQueryable<Subject_SubjectGrade>>().Setup(m => m.ElementType).Returns(data3.ElementType);
            mockSet3.As<IQueryable<Subject_SubjectGrade>>().Setup(m => m.GetEnumerator()).Returns(data3.GetEnumerator());

            var data4 = new List<TeacherClassSubjectGroup>{
                new TeacherClassSubjectGroup{GroupId = 2, teacherClassSubjectId= 1
                }
            }.AsQueryable();

            var mockSet4 = new Mock<DbSet<TeacherClassSubjectGroup>>();
            mockSet4.As<IQueryable<TeacherClassSubjectGroup>>().Setup(m => m.Provider).Returns(data4.Provider);
            mockSet4.As<IQueryable<TeacherClassSubjectGroup>>().Setup(m => m.Expression).Returns(data4.Expression);
            mockSet4.As<IQueryable<TeacherClassSubjectGroup>>().Setup(m => m.ElementType).Returns(data4.ElementType);
            mockSet4.As<IQueryable<TeacherClassSubjectGroup>>().Setup(m => m.GetEnumerator()).Returns(data4.GetEnumerator());


            var data5 = new List<Tt_ClassGroup>{
                new Tt_ClassGroup{Id = 2,groupName = "sub1"
                }
            }.AsQueryable();

            var mockSet5 = new Mock<DbSet<Tt_ClassGroup>>();
            mockSet5.As<IQueryable<Tt_ClassGroup>>().Setup(m => m.Provider).Returns(data5.Provider);
            mockSet5.As<IQueryable<Tt_ClassGroup>>().Setup(m => m.Expression).Returns(data5.Expression);
            mockSet5.As<IQueryable<Tt_ClassGroup>>().Setup(m => m.ElementType).Returns(data5.ElementType);
            mockSet5.As<IQueryable<Tt_ClassGroup>>().Setup(m => m.GetEnumerator()).Returns(data5.GetEnumerator());

            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.School_TeacherClass_Subjects).Returns(mockSet.Object);
            mockDB.Setup(item => item.School_Lookup_Class).Returns(mockSet2.Object);
            mockDB.Setup(item => item.Subject_SubjectGrade).Returns(mockSet3.Object);
            mockDB.Setup(item => item.TeacherClassSubjectGroups).Returns(mockSet4.Object);
            mockDB.Setup(item => item.Tt_ClassGroup).Returns(mockSet5.Object);

            var list = new ActivitiesList(mockDB.Object);
            list.Create();
            test = list.GetList();
            System.Console.WriteLine(test);
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Activities_List", test.Name.ToString());
        }

        [Test]
        public void ActivityIDRightTest()
        {
            Assert.AreEqual(1, test.Elements("Activity").Elements("Id").Count(item => item.Value.Equals("1")));

        }

        [Test]
        public void ActivityGroupIDTest()
        {
            Assert.AreEqual(4, test.Elements("Activity").Elements("Activity_Group_Id").Count(item => item.Value.Equals("1")));

        }

        [Test]
        public void ActivityTeacherRightTest()
        {
            Assert.AreEqual(8, test.Elements("Activity").Elements("Teacher").Count(item => item.Value.Equals("0")));

        }

        [Test]
        public void ActivityTeacherWrongTest()
        {
            Assert.AreEqual(0, test.Elements("Activity").Elements("Teacher").Count(item => item.Value.Equals("3")));

        }

        [Test]
        public void ActivitySubjectRightTest()
        {
            Assert.AreEqual(6, test.Elements("Activity").Elements("Subject").Count(item => item.Value.Equals("0")));

        }

        [Test]
        public void ActivitySubjectWrongTest()
        {
            Assert.AreEqual(0, test.Elements("Activity").Elements("Subject").Count(item => item.Value.Equals("3")));

        }

        [Test]
        public void ActivityClassRightTest()
        {
            Assert.AreEqual(4, test.Elements("Activity").Elements("Students").Count(item => item.Value.Equals("test")));

        }

        [Test]
        public void ActivityClassWrongTest()
        {
            Assert.AreEqual(0, test.Elements("Activity").Elements("Students").Count(item => item.Value.Equals("wrong")));

        }

        [Test]
        public void ActivitySubgroupTest()
        {
            Assert.AreEqual(4, test.Elements("Activity").Elements("Students").Count(item => item.Value.Equals("sub1")));

        }
    }
}
