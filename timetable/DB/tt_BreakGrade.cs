namespace Timetable.timetable.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tt_BreakGrade
    {
        public int Id { get; set; }

        public int breakId { get; set; }

        public int gradeId { get; set; }

        public virtual tt_Break tt_Break { get; set; }
    }
}
