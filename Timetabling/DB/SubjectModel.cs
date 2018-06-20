using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{
    
    /// <summary>
    /// Subject master data subject.
    /// </summary>
    [Table("Subject_MasterData_Subject")]
    public class SubjectModel
    {

        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>The subject identifier.</value>
        [Key, Column("SubjectId")]
        public int SubjectId { get; set; }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>The is active.</value>
        public bool? IsActive { get; set; }
    }
}
