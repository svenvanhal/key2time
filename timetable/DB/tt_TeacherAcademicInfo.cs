namespace Timetable.timetable.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tt_TeacherAcademicInfo
    {
        public int Id { get; set; }

        public int lessonsPerWeek { get; set; }

        public long teacherId { get; set; }

        public int colorId { get; set; }

        public bool IsShared { get; set; }

        public bool IsHomeClassTeacher { get; set; }

        public int substitutionlessonsPerWeek { get; set; }

        public virtual tt_TeacherAcademicInfo tt_TeacherAcademicInfo1 { get; set; }

        public virtual tt_TeacherAcademicInfo tt_TeacherAcademicInfo2 { get; set; }
    }
}
