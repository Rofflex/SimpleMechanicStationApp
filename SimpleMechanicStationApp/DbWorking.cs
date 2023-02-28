using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleMechanicStationApp
{
    class DbWorking:AbstractDbWorking
    {
        public String connectionString { get; set; }
        public String SqlCommandText { get; set; }
        public bool ExecutedSqlQuery;

        public override SqlConnection GetConnection()
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;

        }
        public override SqlCommand GetCommand(SqlConnection con)
        {
            return new SqlCommand(SqlCommandText, con);
        }
        public override SqlDataReader ExecuteCommand(SqlCommand cmd)
        {
            return cmd.ExecuteReader();
        }
        public override bool SqlQueryOutput(SqlDataReader reader)
        {
            //Checking Login and Password
            return reader.HasRows;
        }
        public override SqlDataReader ExecuteSqlQuery()
        {

            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;
            con = GetConnection();
            cmd = GetCommand(con);
            reader = ExecuteCommand(cmd);
            return reader;
        }
    }
}
