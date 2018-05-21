namespace Timetable.timetable.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subject_SubjectGrade
    {
        [Key]
        public int SubjectGradeID { get; set; }

        public int? SubjectID { get; set; }

        public int? GradeID { get; set; }

		public int NumberOfLlessonsPerWeek { get; set; }

        public virtual School_Lookup_Grade School_Lookup_Grade { get; set; }

        public virtual School_Lookup_Grade School_Lookup_Grade1 { get; set; }

        public virtual Subject_MasterData_Subject Subject_MasterData_Subject { get; set; }
    }
}
