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
        public virtual DbSet<EmployeeModel> Employees { get; set; }

		/// <summary>
		/// Gets or sets LookupClassModel.
		/// </summary>
		/// <value>LookupClassModel.</value>
		public virtual DbSet<LookupClassModel> ClassesLookup { get; set; }

        /// <summary>
        /// Gets or sets ClassTeacherSubjectsModel.
        /// </summary>
        /// <value>ClassTeacherSubjectsModel.</value>
        public virtual DbSet<ClassTeacherSubjectsModel> ClassTeacherSubjects { get; set; }

        /// <summary>
        /// Gets or sets LookupGradeModel.
        /// </summary>
        /// <value>LookupGradeModel.</value>
        public virtual DbSet<LookupGradeModel> GradesLookup { get; set; }

		/// <summary>
		/// Gets or sets LookupStageModel.
		/// </summary>
		/// <value>LookupStageModel.</value>
		public virtual DbSet<LookupStageModel> StagesLookup { get; set; }

		/// <summary>
		/// Gets or sets the section week end.
		/// </summary>
		/// <value>The section week end.</value>
		public virtual DbSet<SectionWeekendModel> Weekends { get; set; }

		/// <summary>
		/// Gets or sets SubjectModel.
		/// </summary>
		/// <value>SubjectModel.</value>
		public virtual DbSet<SubjectModel> Subjects { get; set; }

		/// <summary>
		/// Gets or sets SubjectGradeModel.
		/// </summary>
		/// <value>The subject subject grade.</value>
		public virtual DbSet<SubjectGradeModel> SubjectGrades { get; set; }

        /// <summary>
        /// Timetable table. 
        /// </summary>
	    public virtual DbSet<TimetableModel> Timetables { get; set; }

        /// <summary>
        /// Classes for timetable activity.
        /// </summary>
	    public virtual DbSet<TimetableActivityClassModel> TimetableActivityClasses { get; set; }

        /// <summary>
        /// Teachers per timetable activity.
        /// </summary>
	    public virtual DbSet<TimetableActivityTeacherModel> TimetableActivityTeachers { get; set; }

        /// <summary>
        /// Activities in timetable.
        /// </summary>
	    public virtual DbSet<TimetableActivityModel> TimetableActivities { get; set; }

        /// <summary>
        /// Gets or sets GradeLessonModel.
        /// </summary>
        /// <value>GradeLessonModel.</value>
        public virtual DbSet<GradeLessonModel> GradeLessons { get; set; }

		/// <summary>
		/// Gets or sets TimeOffModel.
		/// </summary>
		/// <value>TimeOffModel.</value>
		public virtual DbSet<TimeOffModel> TimesOff { get; set; }

		/// <summary>
		/// Gets or sets BuildingModel.
		/// </summary>
		/// <value>The school buildings units.</value>
		public virtual DbSet<BuildingModel> Buildings { get; set; }

        /// <summary>
        /// Gets or sets ActivityGroupModel.
        /// </summary>
        /// <value></value>
	    public virtual DbSet<ActivityGroupModel> ActitvityGroups { get; set; }

	}
}
