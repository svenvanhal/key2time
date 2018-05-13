namespace Timetable.timetable.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class School_Lookup_Stage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public School_Lookup_Stage()
        {
            School_Lookup_Grade = new HashSet<School_Lookup_Grade>();
        }

        [Key]
        public int StageID { get; set; }

        public int? StageCode { get; set; }

        [Required]
        [StringLength(50)]
        public string StageName { get; set; }

        public int? SectionID { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<School_Lookup_Grade> School_Lookup_Grade { get; set; }

        public virtual School_Lookup_Section School_Lookup_Section { get; set; }
    }
}
