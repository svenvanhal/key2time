namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class School_Lookup_Class
    {
        [Key]
        public int ClassID { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }

        [StringLength(50)]
        public string ClassCode { get; set; }

        public int? GradeID { get; set; }

        public int? FloorID { get; set; }

        public bool? IsActive { get; set; }

        public long? SupervisorID { get; set; }

        public int? colorId { get; set; }

		public int timeOffConstraint { get; set; }

        public bool? IsShared { get; set; }

        public bool? IsHome { get; set; }

        public virtual HR_MasterData_Employees HR_MasterData_Employees { get; set; }

        public virtual School_Lookup_Grade School_Lookup_Grade { get; set; }
    }
}
