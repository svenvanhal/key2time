namespace Timetabling.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    public partial class Tt_TimeOff
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int day { get; set; }

        public int lessonIndex { get; set; }

        public int status { get; set; }

        public int ItemType { get; set; }

        public int ItemId { get; set; }
    }
}
