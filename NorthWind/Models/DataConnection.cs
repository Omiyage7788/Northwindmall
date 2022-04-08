using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace NorthWind.Models
{
    public class DataConnection
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString);
        SqlDataAdapter adp = new SqlDataAdapter("", conn);
        SqlCommand cmd = new SqlCommand("", conn);

        public SqlCommand Cmd 
        {
            get
            {
                return cmd;
            }
            set 
            {
                cmd = value;
            }
         }

        public void executeSql(string sql) 
        {
            cmd.CommandText = sql;

            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void executeProc(string proc)
        {
            cmd.CommandText = proc;
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }

    

}