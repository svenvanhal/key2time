namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class School_TeacherClass_Subjects
    {
        public long ID { get; set; }

        public long? TeacherID { get; set; }

        public int? ClassID { get; set; }

        public int? SubjectID { get; set; }

        public int? LessonsCountPerDay { get; set; }
    }
}
