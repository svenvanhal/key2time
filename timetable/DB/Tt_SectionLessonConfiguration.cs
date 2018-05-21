namespace Timetable.timetable.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tt_SectionLessonConfiguration
    {
        public int Id { get; set; }

        public int lessonPerDay { get; set; }

        public int sectionId { get; set; }
    }
}
