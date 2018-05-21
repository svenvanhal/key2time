using System.Data.Entity;

namespace Timetabling.DB
{

    public partial class DataModel : DbContext
    {
        public DataModel() : base("name=DataModel")
        {

        }

        public virtual DbSet<HR_MasterData_Employees> HR_MasterData_Employees { get; set; }
        public virtual DbSet<Lookup_Month> Lookup_Month { get; set; }
        public virtual DbSet<Lookup_Year> Lookup_Year { get; set; }
        public virtual DbSet<School_Lookup_Class> School_Lookup_Class { get; set; }
        public virtual DbSet<School_Lookup_Grade> School_Lookup_Grade { get; set; }
        public virtual DbSet<School_Lookup_Section> School_Lookup_Section { get; set; }
        public virtual DbSet<School_Lookup_Stage> School_Lookup_Stage { get; set; }
        public virtual DbSet<School_TeacherClass_Subjects> School_TeacherClass_Subjects { get; set; }
        public virtual DbSet<School_TeacherSubjects> School_TeacherSubjects { get; set; }
        public virtual DbSet<Section_WeekEnd> Section_WeekEnd { get; set; }
        public virtual DbSet<Subject_MasterData_Subject> Subject_MasterData_Subject { get; set; }
        public virtual DbSet<Subject_SubjectGrade> Subject_SubjectGrade { get; set; }
        public virtual DbSet<SubjectClassLesson> SubjectClassLessons { get; set; }
        public virtual DbSet<TeacherClassSubjectGroup> TeacherClassSubjectGroups { get; set; }
		public virtual DbSet<Tt_Class> Tt_Class { get; set; }
        public virtual DbSet<Tt_ClassGroup> Tt_ClassGroup { get; set; }
        public virtual DbSet<Tt_GradeLesson> Tt_GradeLesson { get; set; }
        public virtual DbSet<Tt_SectionLessonConfiguration> Tt_SectionLessonConfiguration { get; set; }
        public virtual DbSet<Tt_TeacherAcademicInfo> Tt_TeacherAcademicInfo { get; set; }
        public virtual DbSet<Tt_TimeOff> Tt_TimeOff { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HR_MasterData_Employees>()
                .Property(e => e.TotalWorkedTime)
                .IsFixedLength();

            modelBuilder.Entity<HR_MasterData_Employees>()
                .HasMany(e => e.HR_MasterData_Employees1)
                .WithOptional(e => e.HR_MasterData_Employees2)
                .HasForeignKey(e => e.SupervisorID);

            modelBuilder.Entity<HR_MasterData_Employees>()
                .HasMany(e => e.School_Lookup_Class)
                .WithOptional(e => e.HR_MasterData_Employees)
                .HasForeignKey(e => e.SupervisorID);

            modelBuilder.Entity<HR_MasterData_Employees>()
                .HasMany(e => e.School_TeacherSubjects)
                .WithRequired(e => e.HR_MasterData_Employees)
                .HasForeignKey(e => e.TeacherID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<School_Lookup_Grade>()
                .HasMany(e => e.Subject_SubjectGrade)
                .WithOptional(e => e.School_Lookup_Grade)
                .HasForeignKey(e => e.GradeID);

            modelBuilder.Entity<School_Lookup_Grade>()
                .HasMany(e => e.Subject_SubjectGrade1)
                .WithOptional(e => e.School_Lookup_Grade1)
                .HasForeignKey(e => e.GradeID);

            modelBuilder.Entity<Subject_MasterData_Subject>()
                .HasMany(e => e.School_TeacherSubjects)
                .WithRequired(e => e.Subject_MasterData_Subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TeacherClassSubjectGroup>()
                .Property(e => e.GroupId)
                .IsFixedLength();

         

            modelBuilder.Entity<Tt_Class>()
                .HasMany(e => e.Tt_ClassGroup)
                .WithRequired(e => e.Tt_Class)
                .HasForeignKey(e => e.classId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tt_TeacherAcademicInfo>()
                .HasOptional(e => e.Tt_TeacherAcademicInfo1)
                .WithRequired(e => e.Tt_TeacherAcademicInfo2);
        }   
    }   
}
