namespace Timetable.timetable.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<School_BuildingaUnitType> School_BuildingaUnitType { get; set; }
        public virtual DbSet<School_BuildingsUnits> School_BuildingsUnits { get; set; }
        public virtual DbSet<Subject_Category> Subject_Category { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
