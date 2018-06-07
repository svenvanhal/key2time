namespace Timetabling.DB
{
	using System.Data.Entity;
	/// <summary>
	/// Data model.
	/// </summary>
	public partial class DataModel : DbContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Timetabling.DB.DataModel"/> class.
		/// </summary>
		public DataModel()
				: base("name=DataModel")
		{
		}
		/// <summary>
		/// Gets or sets the HR_MasterData_Employees
		/// </summary>
		/// <value>The hr master data employees.</value>
		public virtual DbSet<HR_MasterData_Employees> HR_MasterData_Employees { get; set; }

		/// <summary>
		/// Gets or sets School_Lookup_Class.
		/// </summary>
		/// <value>School_Lookup_Class.</value>
		public virtual DbSet<School_Lookup_Class> School_Lookup_Class { get; set; }

		/// <summary>
		/// Gets or sets School_Lookup_Grade.
		/// </summary>
		/// <value>School_Lookup_Grade.</value>
		public virtual DbSet<School_Lookup_Grade> School_Lookup_Grade { get; set; }

		/// <summary>
		/// Gets or sets the School_Lookup_Section.
		/// </summary>
		/// <value>School_Lookup_Section.</value>
		public virtual DbSet<School_Lookup_Section> School_Lookup_Section { get; set; }

		/// <summary>
		/// Gets or sets School_Lookup_Stage.
		/// </summary>
		/// <value>School_Lookup_Stage.</value>
		public virtual DbSet<School_Lookup_Stage> School_Lookup_Stage { get; set; }

		/// <summary>
		/// Gets or sets School_TeacherClass_Subjects.
		/// </summary>
		/// <value>The school teacher class subjects.</value>
		public virtual DbSet<School_TeacherClass_Subjects> School_TeacherClass_Subjects { get; set; }

		/// <summary>
		/// Gets or sets School_TeacherSubjects.
		/// </summary>
		/// <value>School_TeacherSubjects.</value>
		public virtual DbSet<School_TeacherSubjects> School_TeacherSubjects { get; set; }

		/// <summary>
		/// Gets or sets the section week end.
		/// </summary>
		/// <value>The section week end.</value>
		public virtual DbSet<Section_WeekEnd> Section_WeekEnd { get; set; }

		/// <summary>
		/// Gets or sets Subject_MasterData_Subject.
		/// </summary>
		/// <value>Subject_MasterData_Subject.</value>
		public virtual DbSet<Subject_MasterData_Subject> Subject_MasterData_Subject { get; set; }

		/// <summary>
		/// Gets or sets Subject_SubjectGrade.
		/// </summary>
		/// <value>The subject subject grade.</value>
		public virtual DbSet<Subject_SubjectGrade> Subject_SubjectGrade { get; set; }

		/// <summary>
		/// Gets or sets SubjectClassLesson.
		/// </summary>
		/// <value>SubjectClassLesson.</value>
		public virtual DbSet<SubjectClassLesson> SubjectClassLessons { get; set; }

		/// <summary>
		/// Gets or sets TeacherClassSubjectGroup.
		/// </summary>
		/// <value>TeacherClassSubjectGroup.</value>
		public virtual DbSet<TeacherClassSubjectGroup> TeacherClassSubjectGroups { get; set; }

		/// <summary>
		/// Gets or sets Tt_ClassGroup.
		/// </summary>
		/// <value>Tt_ClassGroup.</value>
		public virtual DbSet<Tt_ClassGroup> Tt_ClassGroup { get; set; }

		/// <summary>
		/// Gets or sets Tt_GradeLesson.
		/// </summary>
		/// <value>Tt_GradeLesson.</value>
		public virtual DbSet<Tt_GradeLesson> Tt_GradeLesson { get; set; }

		/// <summary>
		/// Gets or sets Tt_SectionLessonConfiguration.
		/// </summary>
		/// <value>Tt_SectionLessonConfiguration.</value>
		public virtual DbSet<Tt_SectionLessonConfiguration> Tt_SectionLessonConfiguration { get; set; }

		/// <summary>
		/// Gets or sets Tt_TeacherAcademicInfo.
		/// </summary>
		/// <value>Tt_TeacherAcademicInfo</value>
		public virtual DbSet<Tt_TeacherAcademicInfo> Tt_TeacherAcademicInfo { get; set; }

		/// <summary>
		/// Gets or sets Tt_TimeOff.
		/// </summary>
		/// <value>Tt_TimeOff.</value>
		public virtual DbSet<Tt_TimeOff> Tt_TimeOff { get; set; }

		/// <summary>
		/// Gets or sets School_BuildingaUnitType.
		/// </summary>
		/// <value>The type of the school buildinga unit.</value>
		public virtual DbSet<School_BuildingaUnitType> School_BuildingaUnitType { get; set; }

		/// <summary>
		/// Gets or sets School_BuildingsUnits.
		/// </summary>
		/// <value>The school buildings units.</value>
		public virtual DbSet<School_BuildingsUnits> School_BuildingsUnits { get; set; }

		/// <summary>
		/// Gets or sets Subject_Category.
		/// </summary>
		/// <value>The subject category.</value>
		public virtual DbSet<Subject_Category> Subject_Category { get; set; }

        /// <summary>
        /// Gets or sets tt_ActitvityGroup.
        /// </summary>
        /// <value></value>
	    public virtual DbSet<tt_ActitvityGroup> tt_ActitvityGroup { get; set; }

        /// <summary>
        /// Creates the datamodel
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
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

			modelBuilder.Entity<Tt_TeacherAcademicInfo>()
					.HasOptional(e => e.Tt_TeacherAcademicInfo1)
					.WithRequired(e => e.Tt_TeacherAcademicInfo2);
		}
	}
}
