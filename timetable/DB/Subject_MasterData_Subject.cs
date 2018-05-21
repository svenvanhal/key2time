namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subject_MasterData_Subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject_MasterData_Subject()
        {
            School_TeacherSubjects = new HashSet<School_TeacherSubjects>();
            Subject_SubjectGrade = new HashSet<Subject_SubjectGrade>();
        }

        [Key]
        public int SubjectID { get; set; }

        [StringLength(100)]
        public string SubjectName { get; set; }

        [StringLength(10)]
        public string ShortName { get; set; }

        public int? SubjectCategoryID { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        public bool? SecondLanguage { get; set; }

        public bool? IsActive { get; set; }

        public int? Section { get; set; }

        public bool? IsShared { get; set; }

        public bool? IsMultiPerDay { get; set; }

        [StringLength(100)]
        public string arabicName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<School_TeacherSubjects> School_TeacherSubjects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subject_SubjectGrade> Subject_SubjectGrade { get; set; }
    }
}
