namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class School_Lookup_Grade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public School_Lookup_Grade()
        {
            School_Lookup_Class = new HashSet<School_Lookup_Class>();
            Subject_SubjectGrade = new HashSet<Subject_SubjectGrade>();
            Subject_SubjectGrade1 = new HashSet<Subject_SubjectGrade>();
        }

        [Key]
        public int GradeID { get; set; }

        [Required]
        [StringLength(50)]
        public string GradeName { get; set; }

        public int? StageID { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(50)]
        public string GradeCode { get; set; }

        public int? GradeStock { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<School_Lookup_Class> School_Lookup_Class { get; set; }

        public virtual School_Lookup_Stage School_Lookup_Stage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subject_SubjectGrade> Subject_SubjectGrade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subject_SubjectGrade> Subject_SubjectGrade1 { get; set; }
    }
}
