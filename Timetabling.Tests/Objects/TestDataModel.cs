using System.Collections.Generic;
using Timetabling.DB;
using System.Linq;
using Moq;
using System.Data.Entity;
using Effort.Provider;

namespace Timetabling.Tests.Objects
{
    public class TestDataModel
    {
        public Mock<DataModel> MockDataModel { get; set; }

        public TestDataModel()
        {

            EffortProviderConfiguration.RegisterProvider();

            var DataActivityGroup = new List<ActivityGroupModel>{
                new ActivityGroupModel{ ClassId = 1, SubjectId = 1,  TeacherId = 0, GradeId = 60, ActivityRefId = 1, Id = 1},
                new ActivityGroupModel{ ClassId = 2, SubjectId = 0,  TeacherId = 4, GradeId = 60, ActivityRefId = 2 },
                new ActivityGroupModel{ ClassId = 2, SubjectId = 1,  TeacherId = 4, GradeId = 60, ActivityRefId = 1, Id = 3},
            }.AsQueryable();

            var MockSetActivityGroup = new Mock<DbSet<ActivityGroupModel>>();
            MockSetActivityGroup.As<IQueryable<ActivityGroupModel>>().Setup(m => m.Provider).Returns(DataActivityGroup.Provider);
            MockSetActivityGroup.As<IQueryable<ActivityGroupModel>>().Setup(m => m.Expression).Returns(DataActivityGroup.Expression);
            MockSetActivityGroup.As<IQueryable<ActivityGroupModel>>().Setup(m => m.ElementType).Returns(DataActivityGroup.ElementType);
            MockSetActivityGroup.As<IQueryable<ActivityGroupModel>>().Setup(m => m.GetEnumerator()).Returns(DataActivityGroup.GetEnumerator());

            var DataClass = new List<LookupClassModel>{
                new LookupClassModel{ ClassName = "test", ClassId = 1, GradeId = 60, IsActive = true },
                new LookupClassModel{ ClassName = "test2", ClassId = 2, GradeId = 60 },
            }.AsQueryable();

            var MockSetClass = new Mock<DbSet<LookupClassModel>>();
            MockSetClass.As<IQueryable<LookupClassModel>>().Setup(m => m.Provider).Returns(DataClass.Provider);
            MockSetClass.As<IQueryable<LookupClassModel>>().Setup(m => m.Expression).Returns(DataClass.Expression);
            MockSetClass.As<IQueryable<LookupClassModel>>().Setup(m => m.ElementType).Returns(DataClass.ElementType);
            MockSetClass.As<IQueryable<LookupClassModel>>().Setup(m => m.GetEnumerator()).Returns(DataClass.GetEnumerator());

            var DataSubjectGrade = new List<SubjectGradeModel>{
                new SubjectGradeModel{ GradeId = 60, NumberOfLessonsPerWeek = 4, NumberOfLessonsPerDay = 1, SubjectId = 1, CollectionId = 1, BuildingUnitTypeId = 1 },
                new SubjectGradeModel{ GradeId = 2,  NumberOfLessonsPerWeek = 8, NumberOfLessonsPerDay = 1, SubjectId = 1 },
                new SubjectGradeModel{ GradeId = 60, NumberOfLessonsPerWeek = 6, NumberOfLessonsPerDay = 4, SubjectId = 0 },
                new SubjectGradeModel{ GradeId = 60, NumberOfLessonsPerWeek = 4, NumberOfLessonsPerDay = 1, SubjectId = 1, CollectionId = 2, BuildingUnitTypeId = 1 },
                new SubjectGradeModel{ GradeId = 60, NumberOfLessonsPerWeek = 4, NumberOfLessonsPerDay = 1, SubjectId = 1, CollectionId = 2, BuildingUnitTypeId = 1 }
            }.AsQueryable();

            var MockSetSubjectGrade = new Mock<DbSet<SubjectGradeModel>>();
            MockSetSubjectGrade.As<IQueryable<SubjectGradeModel>>().Setup(m => m.Provider).Returns(DataSubjectGrade.Provider);
            MockSetSubjectGrade.As<IQueryable<SubjectGradeModel>>().Setup(m => m.Expression).Returns(DataSubjectGrade.Expression);
            MockSetSubjectGrade.As<IQueryable<SubjectGradeModel>>().Setup(m => m.ElementType).Returns(DataSubjectGrade.ElementType);
            MockSetSubjectGrade.As<IQueryable<SubjectGradeModel>>().Setup(m => m.GetEnumerator()).Returns(DataSubjectGrade.GetEnumerator());

            var DataEmployees = new List<EmployeeModel>{
                new EmployeeModel{ EmployeeId = 0, IsActive = true, IsTeacher = true },
                new EmployeeModel{ EmployeeId = 4, IsActive = true, IsTeacher = true },
            }.AsQueryable();

            var MockSetEmployees = new Mock<DbSet<EmployeeModel>>();
            MockSetEmployees.As<IQueryable<EmployeeModel>>().Setup(m => m.Provider).Returns(DataEmployees.Provider);
            MockSetEmployees.As<IQueryable<EmployeeModel>>().Setup(m => m.Expression).Returns(DataEmployees.Expression);
            MockSetEmployees.As<IQueryable<EmployeeModel>>().Setup(m => m.ElementType).Returns(DataEmployees.ElementType);
            MockSetEmployees.As<IQueryable<EmployeeModel>>().Setup(m => m.GetEnumerator()).Returns(DataEmployees.GetEnumerator());

            var DataGrades = new List<LookupGradeModel>{
                new LookupGradeModel{ GradeId = 60, IsActive = true, GradeName = "gradeTest", StageId = 4 }
            }.AsQueryable();

            var MockSetGrades = new Mock<DbSet<LookupGradeModel>>();
            MockSetGrades.As<IQueryable<LookupGradeModel>>().Setup(m => m.Provider).Returns(DataGrades.Provider);
            MockSetGrades.As<IQueryable<LookupGradeModel>>().Setup(m => m.Expression).Returns(DataGrades.Expression);
            MockSetGrades.As<IQueryable<LookupGradeModel>>().Setup(m => m.ElementType).Returns(DataGrades.ElementType);
            MockSetGrades.As<IQueryable<LookupGradeModel>>().Setup(m => m.GetEnumerator()).Returns(DataGrades.GetEnumerator());

            var DataSubjects = new List<SubjectModel>{
                new SubjectModel{ SubjectId = 0, IsActive = true },
                new SubjectModel{ SubjectId = 1, IsActive = true },
                new SubjectModel{ SubjectId = 2, IsActive = false }
            }.AsQueryable();

            var MockSetSubjects = new Mock<DbSet<SubjectModel>>();
            MockSetSubjects.As<IQueryable<SubjectModel>>().Setup(m => m.Provider).Returns(DataSubjects.Provider);
            MockSetSubjects.As<IQueryable<SubjectModel>>().Setup(m => m.Expression).Returns(DataSubjects.Expression);
            MockSetSubjects.As<IQueryable<SubjectModel>>().Setup(m => m.ElementType).Returns(DataSubjects.ElementType);
            MockSetSubjects.As<IQueryable<SubjectModel>>().Setup(m => m.GetEnumerator()).Returns(DataSubjects.GetEnumerator());

            var DataClassTeacherSubject = new List<ClassTeacherSubjectsModel>{
                new ClassTeacherSubjectsModel{ SubjectId = 1, ClassId = 1, TeacherId = 0 },
                new ClassTeacherSubjectsModel{ SubjectId = 0, ClassId = 2, TeacherId = 4 },
            }.AsQueryable();

            var MockSetClassTeacherSubject = new Mock<DbSet<ClassTeacherSubjectsModel>>();
            MockSetClassTeacherSubject.As<IQueryable<ClassTeacherSubjectsModel>>().Setup(m => m.Provider).Returns(DataClassTeacherSubject.Provider);
            MockSetClassTeacherSubject.As<IQueryable<ClassTeacherSubjectsModel>>().Setup(m => m.Expression).Returns(DataClassTeacherSubject.Expression);
            MockSetClassTeacherSubject.As<IQueryable<ClassTeacherSubjectsModel>>().Setup(m => m.ElementType).Returns(DataClassTeacherSubject.ElementType);
            MockSetClassTeacherSubject.As<IQueryable<ClassTeacherSubjectsModel>>().Setup(m => m.GetEnumerator()).Returns(DataClassTeacherSubject.GetEnumerator());

            var DataTimeOff = new List<TimeOffModel>{
                new TimeOffModel{ ItemId = 4, Day = 2, LessonIndex = 3, ItemType = 4 },
                new TimeOffModel{ ItemId = 1, Day = 2, LessonIndex = 3, ItemType = 2 },
                new TimeOffModel{ ItemId = 1, Day = 2, LessonIndex = 3, ItemType = 1 },
                new TimeOffModel{ ItemId = 4, Day = 2, LessonIndex = 3, ItemType = 3 },
            }.AsQueryable();

            var MockSetTimeOff = new Mock<DbSet<TimeOffModel>>();
            MockSetTimeOff.As<IQueryable<TimeOffModel>>().Setup(m => m.Provider).Returns(DataTimeOff.Provider);
            MockSetTimeOff.As<IQueryable<TimeOffModel>>().Setup(m => m.Expression).Returns(DataTimeOff.Expression);
            MockSetTimeOff.As<IQueryable<TimeOffModel>>().Setup(m => m.ElementType).Returns(DataTimeOff.ElementType);
            MockSetTimeOff.As<IQueryable<TimeOffModel>>().Setup(m => m.GetEnumerator()).Returns(DataTimeOff.GetEnumerator());

            var DataBuildings = new List<BuildingModel>{
                new BuildingModel{ Id = 4, IsActive = true },
                new BuildingModel{ Id = 1, IsActive = true, TypeId = 1 },
            }.AsQueryable();

            var MockSetBuildings = new Mock<DbSet<BuildingModel>>();
            MockSetBuildings.As<IQueryable<BuildingModel>>().Setup(m => m.Provider).Returns(DataBuildings.Provider);
            MockSetBuildings.As<IQueryable<BuildingModel>>().Setup(m => m.Expression).Returns(DataBuildings.Expression);
            MockSetBuildings.As<IQueryable<BuildingModel>>().Setup(m => m.ElementType).Returns(DataBuildings.ElementType);
            MockSetBuildings.As<IQueryable<BuildingModel>>().Setup(m => m.GetEnumerator()).Returns(DataBuildings.GetEnumerator());

            var DataSectionWeekend = new List<SectionWeekendModel>{
                new SectionWeekendModel{ DayIndex = 0, SectionId = 1 },
                new SectionWeekendModel{ DayIndex = 1, SectionId = 1 },
                new SectionWeekendModel{ DayIndex = 3, SectionId = 1 }
            }.AsQueryable();

            var MockSetSectionWeekend = new Mock<DbSet<SectionWeekendModel>>();
            MockSetSectionWeekend.As<IQueryable<SectionWeekendModel>>().Setup(m => m.Provider).Returns(DataSectionWeekend.Provider);
            MockSetSectionWeekend.As<IQueryable<SectionWeekendModel>>().Setup(m => m.Expression).Returns(DataSectionWeekend.Expression);
            MockSetSectionWeekend.As<IQueryable<SectionWeekendModel>>().Setup(m => m.ElementType).Returns(DataSectionWeekend.ElementType);
            MockSetSectionWeekend.As<IQueryable<SectionWeekendModel>>().Setup(m => m.GetEnumerator()).Returns(DataSectionWeekend.GetEnumerator());

            var DataStage = new List<LookupStageModel>{
                new LookupStageModel{ IsActive = true, SectionId = 1, StageId = 4 },
            }.AsQueryable();

            var MockSetStage = new Mock<DbSet<LookupStageModel>>();
            MockSetStage.As<IQueryable<LookupStageModel>>().Setup(m => m.Provider).Returns(DataStage.Provider);
            MockSetStage.As<IQueryable<LookupStageModel>>().Setup(m => m.Expression).Returns(DataStage.Expression);
            MockSetStage.As<IQueryable<LookupStageModel>>().Setup(m => m.ElementType).Returns(DataStage.ElementType);
            MockSetStage.As<IQueryable<LookupStageModel>>().Setup(m => m.GetEnumerator()).Returns(DataStage.GetEnumerator());

            MockDataModel = new Mock<DataModel>(4);
            MockDataModel.Setup(item => item.ActitvityGroups).Returns(MockSetActivityGroup.Object);
            MockDataModel.Setup(item => item.ClassesLookup).Returns(MockSetClass.Object);
            MockDataModel.Setup(item => item.SubjectGrades).Returns(MockSetSubjectGrade.Object);
            MockDataModel.Setup(item => item.Employees).Returns(MockSetEmployees.Object);
            MockDataModel.Setup(item => item.GradesLookup).Returns(MockSetGrades.Object);
            MockDataModel.Setup(item => item.Subjects).Returns(MockSetSubjects.Object);
            MockDataModel.Setup(item => item.ClassTeacherSubjects).Returns(MockSetClassTeacherSubject.Object);
            MockDataModel.Setup(item => item.TimesOff).Returns(MockSetTimeOff.Object);
            MockDataModel.Setup(item => item.Buildings).Returns(MockSetBuildings.Object);
            MockDataModel.Setup(item => item.Weekends).Returns(MockSetSectionWeekend.Object);
            MockDataModel.Setup(item => item.StagesLookup).Returns(MockSetStage.Object);
        }
    }
}
