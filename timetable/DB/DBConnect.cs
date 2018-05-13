using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;

namespace Timetable.timetable.DB
{
    public class DBConnect : System.Data.Entity.DbContext
    {
 
        public DBConnect() : base("name = DataModel")
        {
                
        }

        public void Connect(){
          
        }

        public virtual DbSet<tt_Class> Classes { get; set; }



        public static void Main()
        {
            DBConnect dB = new DBConnect();
            tt_Class _Class = new tt_Class { Id = 4, className = "test3", color = 0, gradeId = 1 , IsHome = true, IsShared = false, shortName = "testShort", supervisorId = 0 };
            dB.Classes.Add(_Class);
            dB.SaveChanges();

           
        }
    }
}
