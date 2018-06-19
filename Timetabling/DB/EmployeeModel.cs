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
        [Column("TimeOffConstraint")]
        public int TimeOffConstraint { get; set; }

    }
}
