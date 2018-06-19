using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Tt actitvity group.
    /// </summary>
    [Table("ActitvityGroups")]
    public class ActivityGroupModel
    {
        
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the activity reference identifier.
        /// </summary>
        /// <value>The activity reference identifier.</value>
        [Column("ActivityRefID")]
        public int ActivityRefId { get; set; }

        /// <summary>
        /// Gets or sets the teacher identifier.
        /// </summary>
        /// <value>The teacher identifier.</value>
        [Column("teacherId")]
        public long TeacherId { get; set; }

        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>The subject identifier.</value>
        [Column("subjectId")]
        public int SubjectId { get; set; }

        /// <summary>
        /// Gets or sets the class identifier.
        /// </summary>
        /// <value>The class identifier.</value>
        [Column("classId")]
        public int ClassId { get; set; }

        /// <summary>
        /// Gets or sets the grade identifier.
        /// </summary>
        /// <value>The grade identifier.</value>
        [Column("gradeId")]
        public int GradeId { get; set; }
    }
}
