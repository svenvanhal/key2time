namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tt_GradeLesson
    {
        public int Id { get; set; }

        public int numberOfLessons { get; set; }

        public int gradeId { get; set; }
    }
}
