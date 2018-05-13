namespace Timetable.timetable.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tt_ClassGroup
    {
        public int Id { get; set; }

        public int classId { get; set; }

        [Required]
        [StringLength(100)]
        public string groupName { get; set; }

        public virtual tt_Class tt_Class { get; set; }
    }
}
