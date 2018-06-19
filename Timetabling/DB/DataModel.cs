using System.Data.Common;
using System.Data.Entity;

namespace Timetabling.DB
{
	
	/// <summary>
	/// Data model.
	/// </summary>
	public class DataModel : DbContext
	{
		/// <inheritdoc />
		/// <summary>
		/// Constructs a new DataModel based on the connection string in app.config.
		/// </summary>
		public DataModel() : base("name=DataModel") {}

	    /// <inheritdoc />
	    /// <summary>
	    /// Constructs a new DataModel based on the provider database connection.
	    /// </summary>
        public DataModel(DbConnection connection) : base(connection, true) {}

        /// <summary>
        /// Information about the current academic year.
        /// </summary>
        /// <value>The hr master data employees.</value>
        public virtual DbSet<AcademicQuarterModel> AcademicQuarter { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeModel
        /// </summary>
        /// <value>The hr master data employees.</value>
        public virtual DbSet<EmployeeModel> HR_MasterData_Employees { get; set; }

		/// <summary>
		/// Gets or sets LookupClassModel.
		/// </summary>
		/// <value>LookupClassModel.</value>
		public virtual DbSet<LookupClassModel> School_Lookup_Class { get; set; }

        /// <summary>
        /// Gets or sets ClassTeacherSubjectsModel.
        /// </summary>
        /// <value>ClassTeacherSubjectsModel.</value>
        public virtual DbSet<ClassTeacherSubjectsModel> School_ClassTeacherSubjects { get; set; }

        /// <summary>
        /// Gets or sets School_Lookup_Grade.
        /// </summary>
        /// <value>School_Lookup_Grade.</value>
        public virtual DbSet<School_Lookup_Grade> School_Lookup_Grade { get; set; }

		/// <summary>
		/// Gets or sets School_Lookup_Stage.
		/// </summary>
		/// <value>School_Lookup_Stage.</value>
		public virtual DbSet<School_Lookup_Stage> School_Lookup_Stage { get; set; }

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
		/// Gets or sets SubjectGradeTable.
		/// </summary>
		/// <value>The subject subject grade.</value>
		public virtual DbSet<SubjectGradeTable> Subject_SubjectGrade { get; set; }

        /// <summary>
        /// Timetable table. 
        /// </summary>
	    public virtual DbSet<TimetableTable> Timetables { get; set; }

        /// <summary>
        /// Classes for timetable activity.
        /// </summary>
	    public virtual DbSet<TimetableActivityClassTable> TimetableActivityClasses { get; set; }

        /// <summary>
        /// Teachers per timetable activity.
        /// </summary>
	    public virtual DbSet<TimetableActivityTeacherTable> TimetableActivityTeachers { get; set; }

        /// <summary>
        /// Activities in timetable.
        /// </summary>
	    public virtual DbSet<TimetableActivityTable> TimetableActivities { get; set; }

        /// <summary>
        /// Gets or sets GradeLessonModel.
        /// </summary>
        /// <value>GradeLessonModel.</value>
        public virtual DbSet<GradeLessonModel> Tt_GradeLesson { get; set; }

		/// <summary>
		/// Gets or sets TimeOffTable.
		/// </summary>
		/// <value>TimeOffTable.</value>
		public virtual DbSet<TimeOffTable> Tt_TimeOff { get; set; }

		/// <summary>
		/// Gets or sets BuildingModel.
		/// </summary>
		/// <value>The school buildings units.</value>
		public virtual DbSet<BuildingModel> School_BuildingsUnits { get; set; }

        /// <summary>
        /// Gets or sets ActivityGroupModel.
        /// </summary>
        /// <value></value>
	    public virtual DbSet<ActivityGroupModel> tt_ActitvityGroup { get; set; }

        /// <summary>
        /// Creates the datamodel
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{

            modelBuilder.Entity<School_Lookup_Grade>()
					.HasMany(e => e.Subject_SubjectGrade)
					.WithOptional(e => e.School_Lookup_Grade)
					.HasForeignKey(e => e.GradeID);

			modelBuilder.Entity<School_Lookup_Grade>()
					.HasMany(e => e.Subject_SubjectGrade1)
					.WithOptional(e => e.School_Lookup_Grade1)
					.HasForeignKey(e => e.GradeID);
		}
	}
}
