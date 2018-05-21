namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Section_WeekEnd
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string dayName { get; set; }

        public int dayIndex { get; set; }

        public int sectionId { get; set; }
    }
}
