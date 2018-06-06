namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tt_ActitvityGroup
    {
        public int Id { get; set; }

        public int ActivityRefID { get; set; }

        public long teacherId { get; set; }

        public int subjectId { get; set; }

        public int roomId { get; set; }

        public int groupId { get; set; }

        public int classId { get; set; }

        public int gradeId { get; set; }
    }
}
