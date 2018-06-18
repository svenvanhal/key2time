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
        public virtual DbSet<AcademicQuarterTable> AcademicQuarter { get; set; }

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
        /// Gets or sets School_ClassTeacherSubjects.
        /// </summary>
        /// <value>School_ClassTeacherSubjects.</value>
        public virtual DbSet<School_ClassTeacherSubjects> School_ClassTeacherSubjects { get; set; }

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
		/// Gets or sets Subject_SubjectGrade.
		/// </summary>
		/// <value>The subject subject grade.</value>
		public virtual DbSet<Subject_SubjectGrade> Subject_SubjectGrade { get; set; }

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
        /// Gets or sets Tt_GradeLesson.
        /// </summary>
        /// <value>Tt_GradeLesson.</value>
        public virtual DbSet<Tt_GradeLesson> Tt_GradeLesson { get; set; }

		/// <summary>
		/// Gets or sets Tt_TimeOff.
		/// </summary>
		/// <value>Tt_TimeOff.</value>
		public virtual DbSet<Tt_TimeOff> Tt_TimeOff { get; set; }

		/// <summary>
		/// Gets or sets School_BuildingsUnits.
		/// </summary>
		/// <value>The school buildings units.</value>
		public virtual DbSet<School_BuildingsUnits> School_BuildingsUnits { get; set; }

        /// <summary>
        /// Gets or sets Tt_ActitvityGroup.
        /// </summary>
        /// <value></value>
	    public virtual DbSet<Tt_ActitvityGroup> tt_ActitvityGroup { get; set; }

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
