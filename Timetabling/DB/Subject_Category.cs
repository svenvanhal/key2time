namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subject_Category
    {
        [Key]
        public int SubjectCategoryID { get; set; }

        [StringLength(50)]
        public string SubjectCategoryName { get; set; }

        public bool? IsActive { get; set; }
    }
}
