namespace Timetabling.DB
{
	using System.ComponentModel.DataAnnotations;
	/// <summary>
	/// School lookup class.
	/// </summary>
	public partial class School_Lookup_Class
    {
		/// <summary>
        /// Gets or sets the class identifier.
        /// </summary>
        /// <value>The class identifier.</value>
        [Key]
        public int ClassID { get; set; }
        
		/// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>The name of the class.</value>
        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }
        
		/// <summary>
        /// Gets or sets the class code.
        /// </summary>
        /// <value>The class code.</value>
        [StringLength(50)]
        public string ClassCode { get; set; }
        
		/// <summary>
        /// Gets or sets the grade identifier.
        /// </summary>
        /// <value>The grade identifier.</value>
        public int? GradeID { get; set; }
    
        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>The is active.</value>
        public bool? IsActive { get; set; }
        
		/// <summary>
        /// Gets or sets the supervisor identifier.
        /// </summary>
        /// <value>The supervisor identifier.</value>
        public long? SupervisorID { get; set; }

        /// <summary>
        /// Gets or sets the time off constraint.
        /// </summary>
        /// <value>The time off constraint.</value>
		public int timeOffConstraint { get; set; }
        
		/// <summary>
        /// Gets or sets the is shared.
        /// </summary>
        /// <value>The is shared.</value>
        public bool? IsShared { get; set; }
        
		/// <summary>
        /// Gets or sets the is home.
        /// </summary>
        /// <value>The is home.</value>
        public bool? IsHome { get; set; }
        
		/// <summary>
		/// Gets or sets the HR_MasterData_Employees.
        /// </summary>
		/// <value>The HR_MasterData_Employees.</value>
        public virtual HR_MasterData_Employees HR_MasterData_Employees { get; set; }
        
		/// <summary>
		/// Gets or sets the School_Lookup_Grade.
        /// </summary>
		/// <value>The School_Lookup_Grade.</value>
        public virtual School_Lookup_Grade School_Lookup_Grade { get; set; }
    }
}
