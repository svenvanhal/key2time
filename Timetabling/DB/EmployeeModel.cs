using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Information about employees.
    /// </summary>
    [Table("HR_MasterData_Employees")]
    public class EmployeeModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Timetabling.DB.EmployeeModel"/> class.
        /// </summary>
        public EmployeeModel()
        {
            HR_MasterData_Employees1 = new HashSet<EmployeeModel>();
            School_Lookup_Class = new HashSet<School_Lookup_Class>();
        }
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>The employee identifier.</value>
        [Column("EmployeeID"), Key]
        public long EmployeeId { get; set; }

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
        [Column("timeOffConstraint")]
        public int TimeOffConstraint { get; set; }

        /// <summary>
        /// Gets or sets the supervisor identifier.
        /// </summary>
        /// <value>The supervisor identifier.</value>
        [Column("SupervisorID")]
        public long? SupervisorId { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeModel table.
        /// </summary>
        /// <value>The EmployeeModel.</value>
        public virtual ICollection<EmployeeModel> HR_MasterData_Employees1 { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeModel table.
        /// </summary>
        /// <value>The EmployeeModel.</value>
        public virtual EmployeeModel HR_MasterData_Employees2 { get; set; }

        /// <summary>
        /// Gets or sets the School_Lookup_Class table.
        /// </summary>
        /// <value>The School_Lookup_Class.</value>
        public virtual ICollection<School_Lookup_Class> School_Lookup_Class { get; set; }


    }
}
