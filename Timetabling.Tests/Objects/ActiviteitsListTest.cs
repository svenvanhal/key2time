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
    internal class ActivitiesListTest
    {

        XElement test;

        ActivitiesList list;

        [SetUp]
        public void Init()
        {

            var data = new List<ActivityGroupModel>{
                new ActivityGroupModel{ ClassId = 1, SubjectId = 1,  TeacherId = 0, GradeId = 60, ActivityRefId = 1, Id = 1},
                new ActivityGroupModel{ ClassId = 2, SubjectId = 0,  TeacherId = 4, GradeId = 60, ActivityRefId = 2},
                new ActivityGroupModel{ ClassId = 2, SubjectId = 1,  TeacherId = 4, GradeId = 60, ActivityRefId = 1, Id = 3},

            }.AsQueryable();

            var mockSet = new Mock<DbSet<ActivityGroupModel>>();
            mockSet.As<IQueryable<ActivityGroupModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ActivityGroupModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ActivityGroupModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ActivityGroupModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var data2 = new List<LookupClassModel>{
                new LookupClassModel{ClassName = "test", ClassId = 1, GradeId = 60},
                new LookupClassModel{ClassName = "test2", ClassId = 2, GradeId = 60},
            }.AsQueryable();

            var mockSet2 = new Mock<DbSet<LookupClassModel>>();
            mockSet2.As<IQueryable<LookupClassModel>>().Setup(m => m.Provider).Returns(data2.Provider);
            mockSet2.As<IQueryable<LookupClassModel>>().Setup(m => m.Expression).Returns(data2.Expression);
            mockSet2.As<IQueryable<LookupClassModel>>().Setup(m => m.ElementType).Returns(data2.ElementType);
            mockSet2.As<IQueryable<LookupClassModel>>().Setup(m => m.GetEnumerator()).Returns(data2.GetEnumerator());

            var data3 = new List<SubjectGradeTable>{
                new SubjectGradeTable{GradeID = 60, NumberOfLlessonsPerWeek = 4, NumberOfLlessonsPerDay =1, SubjectID =1, CollectionID = 1 
                },
                new SubjectGradeTable{GradeID = 60, NumberOfLlessonsPerWeek = 6, NumberOfLlessonsPerDay = 4,SubjectID =0
                },
            }.AsQueryable();

            var mockSet3 = new Mock<DbSet<SubjectGradeTable>>();
            mockSet3.As<IQueryable<SubjectGradeTable>>().Setup(m => m.Provider).Returns(data3.Provider);
            mockSet3.As<IQueryable<SubjectGradeTable>>().Setup(m => m.Expression).Returns(data3.Expression);
            mockSet3.As<IQueryable<SubjectGradeTable>>().Setup(m => m.ElementType).Returns(data3.ElementType);
            mockSet3.As<IQueryable<SubjectGradeTable>>().Setup(m => m.GetEnumerator()).Returns(data3.GetEnumerator());

            var data4 = new List<EmployeeModel>{
                new EmployeeModel{EmployeeId = 0, IsActive = true, IsTeacher =true,
                },
                new EmployeeModel{EmployeeId = 4, IsActive = true, IsTeacher =true
                },
            }.AsQueryable();

            var mockSet4 = new Mock<DbSet<EmployeeModel>>();
            mockSet4.As<IQueryable<EmployeeModel>>().Setup(m => m.Provider).Returns(data4.Provider);
            mockSet4.As<IQueryable<EmployeeModel>>().Setup(m => m.Expression).Returns(data4.Expression);
            mockSet4.As<IQueryable<EmployeeModel>>().Setup(m => m.ElementType).Returns(data4.ElementType);
            mockSet4.As<IQueryable<EmployeeModel>>().Setup(m => m.GetEnumerator()).Returns(data4.GetEnumerator());

            var data5 = new List<School_Lookup_Grade>{
                new School_Lookup_Grade{GradeID = 60, IsActive = true, GradeName = "gradeTest"
                 }
             }.AsQueryable();

            var mockSet5 = new Mock<DbSet<School_Lookup_Grade>>();
            mockSet5.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Provider).Returns(data5.Provider);
            mockSet5.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.Expression).Returns(data5.Expression);
            mockSet5.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.ElementType).Returns(data5.ElementType);
            mockSet5.As<IQueryable<School_Lookup_Grade>>().Setup(m => m.GetEnumerator()).Returns(data5.GetEnumerator());

            var data6 = new List<Subject_MasterData_Subject>{
                new Subject_MasterData_Subject{SubjectID = 1,  IsActive = true
                 },
                new Subject_MasterData_Subject{SubjectID = 0
                 }
             }.AsQueryable();

            var mockSet6 = new Mock<DbSet<Subject_MasterData_Subject>>();
            mockSet6.As<IQueryable<Subject_MasterData_Subject>>().Setup(m => m.Provider).Returns(data6.Provider);
            mockSet6.As<IQueryable<Subject_MasterData_Subject>>().Setup(m => m.Expression).Returns(data6.Expression);
            mockSet6.As<IQueryable<Subject_MasterData_Subject>>().Setup(m => m.ElementType).Returns(data6.ElementType);
            mockSet6.As<IQueryable<Subject_MasterData_Subject>>().Setup(m => m.GetEnumerator()).Returns(data6.GetEnumerator());

            var data7 = new List<ClassTeacherSubjectsModel>{
                new ClassTeacherSubjectsModel{SubjectId = 1, ClassId = 1, TeacherId = 0
                 },
                new ClassTeacherSubjectsModel{SubjectId = 0, ClassId = 2, TeacherId = 4
                 }
             }.AsQueryable();

            var mockSet7 = new Mock<DbSet<ClassTeacherSubjectsModel>>();
            mockSet7.As<IQueryable<ClassTeacherSubjectsModel>>().Setup(m => m.Provider).Returns(data7.Provider);
            mockSet7.As<IQueryable<ClassTeacherSubjectsModel>>().Setup(m => m.Expression).Returns(data7.Expression);
            mockSet7.As<IQueryable<ClassTeacherSubjectsModel>>().Setup(m => m.ElementType).Returns(data7.ElementType);
            mockSet7.As<IQueryable<ClassTeacherSubjectsModel>>().Setup(m => m.GetEnumerator()).Returns(data7.GetEnumerator());



            var mockDB = new Mock<DataModel>();
            mockDB.Setup(item => item.tt_ActitvityGroup).Returns(mockSet.Object);
            mockDB.Setup(item => item.School_Lookup_Class).Returns(mockSet2.Object);
            mockDB.Setup(item => item.Subject_SubjectGrade).Returns(mockSet3.Object);
            mockDB.Setup(item => item.HR_MasterData_Employees).Returns(mockSet4.Object);
            mockDB.Setup(item => item.School_Lookup_Grade).Returns(mockSet5.Object);
            mockDB.Setup(item => item.Subject_MasterData_Subject).Returns(mockSet6.Object);
            mockDB.Setup(item => item.School_ClassTeacherSubjects).Returns(mockSet7.Object);

            list = new ActivitiesList(mockDB.Object);
            test = list.Create();
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
            Assert.AreEqual(2, test.Elements("Activity").Elements("Subject").Count(item => item.Value.Equals("0")));

        }

        [Test]
        public void ActivitySubjectWrongTest()
        {
            Assert.AreEqual(0, test.Elements("Activity").Elements("Subject").Count(item => item.Value.Equals("3")));

        }

        [Test]
        public void ActivityClassRightTest()
        {
            Assert.AreEqual(8, test.Elements("Activity").Elements("Students").Count(item => item.Value.Equals("test2")));

        }

        [Test]
        public void ActivityClassWrongTest()
        {
            Assert.AreEqual(0, test.Elements("Activity").Elements("Students").Count(item => item.Value.Equals("wrong")));

        }

        [Test]
        public void ActivityCollection()
        {
            Assert.AreEqual(true, list.Activities[1].IsCollection);

        }
    }
}
