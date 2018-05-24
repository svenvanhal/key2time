namespace Timetabling.DB
{
   
    /// <summary>
    /// School teacher class subjects.
    /// </summary>
    public partial class School_TeacherClass_Subjects
    {
		/// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long ID { get; set; }
        
		/// <summary>
        /// Gets or sets the teacher identifier.
        /// </summary>
        /// <value>The teacher identifier.</value>
        public long? TeacherID { get; set; }
       
		/// <summary>
        /// Gets or sets the class identifier.
        /// </summary>
        /// <value>The class identifier.</value>
        public int? ClassID { get; set; }
        
		/// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>The subject identifier.</value>
        public int? SubjectID { get; set; }
       
		/// <summary>
        /// Gets or sets the lessons count per day.
        /// </summary>
        /// <value>The lessons count per day.</value>
        public int? LessonsCountPerDay { get; set; }
    }
}
