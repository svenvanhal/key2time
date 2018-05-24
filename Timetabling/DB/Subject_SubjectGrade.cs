namespace Timetabling.DB
{
	using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Subject subject grade.
    /// </summary>
	public partial class Subject_SubjectGrade
    {
		/// <summary>
        /// Gets or sets the subject grade identifier.
        /// </summary>
        /// <value>The subject grade identifier.</value>
        [Key]
        public int SubjectGradeID { get; set; }

        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>The subject identifier.</value>
        public int? SubjectID { get; set; }

        /// <summary>
        /// Gets or sets the grade identifier.
        /// </summary>
        /// <value>The grade identifier.</value>
        public int? GradeID { get; set; }

        /// <summary>
        /// Gets or sets the number of llessons per week.
        /// </summary>
        /// <value>The number of llessons per week.</value>
		public int NumberOfLlessonsPerWeek { get; set; }

        /// <summary>
        /// Gets or sets the number of llessons per day.
        /// </summary>
        /// <value>The number of llessons per day.</value>
		public int NumberOfLlessonsPerDay { get; set; }

        /// <summary>
		/// Gets or sets School_Lookup_Grade.
        /// </summary>
		/// <value>School_Lookup_Grade.</value>
        public virtual School_Lookup_Grade School_Lookup_Grade { get; set; }

        /// <summary>
		/// Gets or sets School_Lookup_Grade1.
        /// </summary>
        /// <value>The school lookup grade1.</value>
        public virtual School_Lookup_Grade School_Lookup_Grade1 { get; set; }

        /// <summary>
		/// Gets or sets Subject_MasterData_Subject.
        /// </summary>
		/// <value>Subject_MasterData_Subject.</value>
        public virtual Subject_MasterData_Subject Subject_MasterData_Subject { get; set; }
    }
}
