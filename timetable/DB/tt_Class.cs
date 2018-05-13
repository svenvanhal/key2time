namespace Timetable.timetable.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Timetable.timetable.XML;

    public partial class tt_Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tt_Class()
        {
            tt_ClassGroup = new HashSet<tt_ClassGroup>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string className { get; set; }

        [Required]
        [StringLength(100)]
        public string shortName { get; set; }

        public int color { get; set; }

        public int gradeId { get; set; }

        public int supervisorId { get; set; }

        public bool IsShared { get; set; }

        public bool IsHome { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_ClassGroup> tt_ClassGroup { get; set; }

      
      
    }
}
