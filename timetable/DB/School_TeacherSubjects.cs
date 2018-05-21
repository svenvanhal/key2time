namespace Timetable.timetable.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class School_TeacherSubjects
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TeacherID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectID { get; set; }

        public int? gradeId { get; set; }

        public virtual HR_MasterData_Employees HR_MasterData_Employees { get; set; }

        public virtual Subject_MasterData_Subject Subject_MasterData_Subject { get; set; }
    }
}
