namespace Timetable.timetable.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class School_BuildingaUnitType
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string TypeName { get; set; }

        public bool? IsActive { get; set; }
    }
}
