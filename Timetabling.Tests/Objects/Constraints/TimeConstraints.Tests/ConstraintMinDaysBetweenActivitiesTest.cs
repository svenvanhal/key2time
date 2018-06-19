using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using Timetabling.DB;
using Timetabling.Objects.Constraints.TimeConstraints;

namespace Timetabling.Tests.Objects.Constraints.TimeConstraints.Tests
{
    internal class ConstraintMinDaysBetweenActivitiesTest
    {
        Mock<DataModel> test;

        [SetUp]
        public void Init()
        {
            var data = new List<ActivityGroupModel>{
                new ActivityGroupModel{ ClassId = 1, SubjectId = 1,  TeacherId = 0, GradeId = 60, ActivityRefId = 1, Id = 1},
                new ActivityGroupModel{ ClassId = 2, SubjectId = 0,  TeacherId = 4, GradeId = 60, ActivityRefId = 2},
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
                new SubjectGradeTable{GradeID = 60, NumberOfLlessonsPerWeek = 4, NumberOfLlessonsPerDay =1, SubjectID =1,  CollectionID = 1 
                },
                new SubjectGradeTable{GradeID = 60, NumberOfLlessonsPerWeek = 6, NumberOfLlessonsPerDay =2,SubjectID =0
                },
            }.AsQueryable();

            var mockSet3 = new Mock<DbSet<SubjectGradeTable>>();
            mockSet3.As<IQueryable<SubjectGradeTable>>().Setup(m => m.Provider).Returns(data3.Provider);
            mockSet3.As<IQueryable<SubjectGradeTable>>().Setup(m => m.Expression).Returns(data3.Expression);
            mockSet3.As<IQueryable<SubjectGradeTable>>().Setup(m => m.ElementType).Returns(data3.ElementType);
            mockSet3.As<IQueryable<SubjectGradeTable>>().Setup(m => m.GetEnumerator()).Returns(data3.GetEnumerator());

            var data4 = new List<EmployeeModel>{
                new EmployeeModel{EmployeeId = 0, IsActive = true, IsTeacher =true
                },
                new EmployeeModel{EmployeeId = 4, IsActive = true, IsTeacher =true
                },
            }.AsQueryable();

            var mockSet4 = new Mock<DbSet<EmployeeModel>>();
            mockSet4.As<IQueryable<EmployeeModel>>().Setup(m => m.Provider).Returns(data4.Provider);
            mockSet4.As<IQueryable<EmployeeModel>>().Setup(m => m.Expression).Returns(data4.Expression);
            mockSet4.As<IQueryable<EmployeeModel>>().Setup(m => m.ElementType).Returns(data4.ElementType);
            mockSet4.As<IQueryable<EmployeeModel>>().Setup(m => m.GetEnumerator()).Returns(data4.GetEnumerator());

            var data5 = new List<LookupGradeModel>{
                new LookupGradeModel{GradeId = 60, IsActive = true, GradeName = "gradeTest"
                 }
             }.AsQueryable();

            var mockSet5 = new Mock<DbSet<LookupGradeModel>>();
            mockSet5.As<IQueryable<LookupGradeModel>>().Setup(m => m.Provider).Returns(data5.Provider);
            mockSet5.As<IQueryable<LookupGradeModel>>().Setup(m => m.Expression).Returns(data5.Expression);
            mockSet5.As<IQueryable<LookupGradeModel>>().Setup(m => m.ElementType).Returns(data5.ElementType);
            mockSet5.As<IQueryable<LookupGradeModel>>().Setup(m => m.GetEnumerator()).Returns(data5.GetEnumerator());

            var data6 = new List<SubjectModel>{
                new SubjectModel{SubjectId = 1, IsActive = true
                 },
                new SubjectModel{SubjectId = 0
                 }
             }.AsQueryable();

            var mockSet6 = new Mock<DbSet<SubjectModel>>();
            mockSet6.As<IQueryable<SubjectModel>>().Setup(m => m.Provider).Returns(data6.Provider);
            mockSet6.As<IQueryable<SubjectModel>>().Setup(m => m.Expression).Returns(data6.Expression);
            mockSet6.As<IQueryable<SubjectModel>>().Setup(m => m.ElementType).Returns(data6.ElementType);
            mockSet6.As<IQueryable<SubjectModel>>().Setup(m => m.GetEnumerator()).Returns(data6.GetEnumerator());

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

            test = mockDB;
        }

        [Test()]
        public void TestConstruct()
        {
            var constraint = new ConstraintMinDaysBetweenActivities();
            Assert.AreEqual("ConstraintMinDaysBetweenActivities", constraint.ToXelement().Name.ToString());
        }


        [Test]
        public void CreateNumberOfActivitiesTest()
        {
            var constraint = new ConstraintMinDaysBetweenActivities();

            var result = constraint.Create(test.Object);
            Assert.AreEqual("4", result.First().Elements("Number_of_Activities").First().Value);
            Assert.AreNotEqual("6", result.First().Elements("Number_of_Activities").First().Value);
        }

        [Test]
        public void CreateNumberOfActivityElementsTest()
        {
            var constraint = new ConstraintMinDaysBetweenActivities();
            var result = constraint.Create(test.Object);

            Assert.AreEqual(4, result.First().Elements("Activity_Id").Count());
        }
    }

}
