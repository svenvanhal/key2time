namespace Timetabling.DB
{

	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Hr master data employees.
    /// </summary>
	public partial class HR_MasterData_Employees
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.DB.HR_MasterData_Employees"/> class.
        /// </summary>
		public HR_MasterData_Employees()
		{
			HR_MasterData_Employees1 = new HashSet<HR_MasterData_Employees>();
			School_Lookup_Class = new HashSet<School_Lookup_Class>();
			School_TeacherSubjects = new HashSet<School_TeacherSubjects>();
		}
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>The employee identifier.</value>
		[Key]
		public long EmployeeID { get; set; }
       
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
		public long? UserID { get; set; }

        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        /// <value>The short name.</value>
		[StringLength(50)]
		public string ShortName { get; set; }
        
        /// <summary>
        /// Gets or sets the work days.
        /// </summary>
        /// <value>The work days.</value>
		public int? WorkDays { get; set; }

        /// <summary>
        /// Gets or sets the work hours.
        /// </summary>
        /// <value>The work hours.</value>
		public double? WorkHours { get; set; }
  

        /// <summary>
        /// Gets or sets if the employee is a teacher.
        /// </summary>
        /// <value>The is teacher.</value>
		public bool? IsTeacher { get; set; }

           
        /// <summary>
		/// Gets or sets if the employee is active.
        /// </summary>
        /// <value>The is active.</value>
		public bool? IsActive { get; set; }

       
        /// <summary>
        /// Gets or sets the time off constraint value.
        /// </summary>
        /// <value>The time off constraint.</value>
		public int timeOffConstraint { get; set; }

        /// <summary>
        /// Gets or sets the supervisor identifier.
        /// </summary>
        /// <value>The supervisor identifier.</value>
		public long? SupervisorID { get; set; }

        /// <summary>
		/// Gets or sets the HR_MasterData_Employees table.
        /// </summary>
		/// <value>The HR_MasterData_Employees.</value>
		public virtual ICollection<HR_MasterData_Employees> HR_MasterData_Employees1 { get; set; }
        /// <summary>
		/// Gets or sets the HR_MasterData_Employees table.
        /// </summary>
		/// <value>The HR_MasterData_Employees.</value>
		public virtual HR_MasterData_Employees HR_MasterData_Employees2 { get; set; }
        /// <summary>
		/// Gets or sets the School_Lookup_Class table.
        /// </summary>
		/// <value>The School_Lookup_Class.</value>
		public virtual ICollection<School_Lookup_Class> School_Lookup_Class { get; set; }
        /// <summary>
		/// Gets or sets the School_TeacherSubjects table.
        /// </summary>
		/// <value>The  School_TeacherSubjects.</value>
		public virtual ICollection<School_TeacherSubjects> School_TeacherSubjects { get; set; }
	}
}
