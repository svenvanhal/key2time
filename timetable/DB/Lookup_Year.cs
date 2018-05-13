namespace Timetable.timetable.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lookup_Year
    {
        [Key]
        public int YearID { get; set; }

        [Required]
        [StringLength(50)]
        public string YearName { get; set; }
    }
}
