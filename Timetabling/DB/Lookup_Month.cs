namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lookup_Month
    {
        [Key]
        public int MonthID { get; set; }

        [Required]
        [StringLength(50)]
        public string MonthName { get; set; }

        public bool? IsActive { get; set; }

        public int? Code { get; set; }
    }
}
