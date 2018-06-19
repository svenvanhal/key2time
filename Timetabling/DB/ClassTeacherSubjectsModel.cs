using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetabling.DB
{

    /// <summary>
    /// Table defining the relationships between classes, teachers and subjects.
    /// </summary>
    [Table("School_ClassTeacherSubjects")]
    public class ClassTeacherSubjectsModel
    {

        /// <summary>
        /// Teacher ID.
        /// </summary>
        [Key]
        [Column("TeacherID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TeacherId { get; set; }

        /// <summary>
        /// Subject ID.
        /// </summary>
        [Key]
        [Column("SubjectID", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectId { get; set; }

        /// <summary>
        /// Class ID.
        /// </summary>
        [Key]
        [Column("ClassId", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClassId { get; set; }

    }
}
