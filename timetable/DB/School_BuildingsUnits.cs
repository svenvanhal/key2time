namespace Timetable.timetable.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class School_BuildingsUnits
    {
        public int ID { get; set; }

        public int? FloorID { get; set; }

        [StringLength(150)]
        public string UnitName { get; set; }

        public bool? IsActive { get; set; }

        public int? TypeID { get; set; }
    }
}
