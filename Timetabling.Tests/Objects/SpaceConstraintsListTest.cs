using NUnit.Framework;
using Moq;
using Timetabling.Objects;
using System.Xml.Linq;
using Timetabling.DB;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Timetabling.Tests.Objects
{
    [TestFixture()]
    internal class SpaceConstraintsListTest
    {

        XElement test;
        SpaceConstraintsList spaceConstraintsList;

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

            var data3 = new List<SubjectGradeModel>{
                new SubjectGradeModel{GradeId = 60, NumberOfLessonsPerWeek = 4, NumberOfLessonsPerDay =1, SubjectId =1, CollectionId = 1
                },
                new SubjectGradeModel{GradeId = 60, NumberOfLessonsPerWeek = 6, NumberOfLessonsPerDay = 4,SubjectId =0
                },
            }.AsQueryable();

            var mockSet3 = new Mock<DbSet<SubjectGradeModel>>();
            mockSet3.As<IQueryable<SubjectGradeModel>>().Setup(m => m.Provider).Returns(data3.Provider);
            mockSet3.As<IQueryable<SubjectGradeModel>>().Setup(m => m.Expression).Returns(data3.Expression);
            mockSet3.As<IQueryable<SubjectGradeModel>>().Setup(m => m.ElementType).Returns(data3.ElementType);
            mockSet3.As<IQueryable<SubjectGradeModel>>().Setup(m => m.GetEnumerator()).Returns(data3.GetEnumerator());

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

            var data5 = new List<LookupGradeModel>{
                new LookupGradeModel{GradeId = 60, IsActive = true, GradeName = "gradeTest", StageId = 4
                 }
             }.AsQueryable();

            var mockSet5 = new Mock<DbSet<LookupGradeModel>>();
            mockSet5.As<IQueryable<LookupGradeModel>>().Setup(m => m.Provider).Returns(data5.Provider);
            mockSet5.As<IQueryable<LookupGradeModel>>().Setup(m => m.Expression).Returns(data5.Expression);
            mockSet5.As<IQueryable<LookupGradeModel>>().Setup(m => m.ElementType).Returns(data5.ElementType);
            mockSet5.As<IQueryable<LookupGradeModel>>().Setup(m => m.GetEnumerator()).Returns(data5.GetEnumerator());

            var data6 = new List<SubjectModel>{
                new SubjectModel{SubjectId = 1,  IsActive = true
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

            var dataTimeOff = new List<TimeOffModel>{
                new TimeOffModel{ItemId = 4, Day = 2, LessonIndex = 3, ItemType = 4},
            }.AsQueryable();

            var mockSetTimeOff = new Mock<DbSet<TimeOffModel>>();
            mockSetTimeOff.As<IQueryable<TimeOffModel>>().Setup(m => m.Provider).Returns(dataTimeOff.Provider);
            mockSetTimeOff.As<IQueryable<TimeOffModel>>().Setup(m => m.Expression).Returns(dataTimeOff.Expression);
            mockSetTimeOff.As<IQueryable<TimeOffModel>>().Setup(m => m.ElementType).Returns(dataTimeOff.ElementType);
            mockSetTimeOff.As<IQueryable<TimeOffModel>>().Setup(m => m.GetEnumerator()).Returns(dataTimeOff.GetEnumerator());

            var dataEmp = new List<BuildingModel>{
                new BuildingModel{Id = 4, IsActive = true},
            }.AsQueryable();

            var mockSetEmp = new Mock<DbSet<BuildingModel>>();
            mockSetEmp.As<IQueryable<BuildingModel>>().Setup(m => m.Provider).Returns(dataEmp.Provider);
            mockSetEmp.As<IQueryable<BuildingModel>>().Setup(m => m.Expression).Returns(dataEmp.Expression);
            mockSetEmp.As<IQueryable<BuildingModel>>().Setup(m => m.ElementType).Returns(dataEmp.ElementType);
            mockSetEmp.As<IQueryable<BuildingModel>>().Setup(m => m.GetEnumerator()).Returns(dataEmp.GetEnumerator());

            var mockDB = new Mock<DataModel>(4);
            mockDB.Setup(item => item.ActitvityGroups).Returns(mockSet.Object);
            mockDB.Setup(item => item.ClassesLookup).Returns(mockSet2.Object);
            mockDB.Setup(item => item.SubjectGrades).Returns(mockSet3.Object);
            mockDB.Setup(item => item.Employees).Returns(mockSet4.Object);
            mockDB.Setup(item => item.GradesLookup).Returns(mockSet5.Object);
            mockDB.Setup(item => item.Subjects).Returns(mockSet6.Object);
            mockDB.Setup(item => item.ClassTeacherSubjects).Returns(mockSet7.Object);
            mockDB.Setup(item => item.TimesOff).Returns(mockSetTimeOff.Object);
            mockDB.Setup(item => item.Buildings).Returns(mockSetEmp.Object);

            spaceConstraintsList = new SpaceConstraintsList(mockDB.Object);
            test = spaceConstraintsList.Create();
        }

        [Test]
        public void ElementNameTest()
        {
            Assert.AreEqual("Space_Constraints_List", test.Name.ToString());
        }

        [Test]
        public void CreateTest(){
            Assert.AreEqual(2, spaceConstraintsList.Constraints.Count());
        }

    }
}
