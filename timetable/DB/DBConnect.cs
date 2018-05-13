using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Timetable.timetable.DB
{
    public class DBConnect
    {
        string connectionString;

        SqlConnection sqlConnection;

        public DBConnect()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
        }

        public void Connect(){
            sqlConnection.Open();
            Console.Write("Connection Open  !");

            SqlCommand myCommand = new SqlCommand( "SELECT * FROM sysdiagrams", sqlConnection);

            SqlDataReader dataReader = myCommand.ExecuteReader();
            while (dataReader.Read())
            {
                Console.Write(dataReader.GetString(0));
              
            }
            sqlConnection.Close();
        }



        public static void Main(){
            DBConnect dB = new DBConnect();
            dB.Connect();
           
        }
    }
}
