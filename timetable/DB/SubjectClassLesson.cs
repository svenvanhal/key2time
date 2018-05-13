namespace Timetable.timetable.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubjectClassLesson")]
    public partial class SubjectClassLesson
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int classId { get; set; }

        public int numberOfLessonPerWeek { get; set; }
    }
}
