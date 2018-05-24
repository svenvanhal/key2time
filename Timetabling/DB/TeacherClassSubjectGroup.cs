namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TeacherClassSubjectGroup")]
    public partial class TeacherClassSubjectGroup
    {
        public int Id { get; set; }

        public int teacherClassSubjectId { get; set; }

        [StringLength(10)]
        public string GroupId { get; set; }
    }
}
