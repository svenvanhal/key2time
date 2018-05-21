namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class School_Lookup_Section
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public School_Lookup_Section()
        {
            School_Lookup_Stage = new HashSet<School_Lookup_Stage>();
        }

        [Key]
        public int SectionID { get; set; }

        [StringLength(250)]
        public string SectionName { get; set; }

        [StringLength(20)]
        public string ShortName { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsOpen { get; set; }

        public int? maxLessonsPerDay { get; set; }

        public int? daysPerWeek { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<School_Lookup_Stage> School_Lookup_Stage { get; set; }
    }
}
