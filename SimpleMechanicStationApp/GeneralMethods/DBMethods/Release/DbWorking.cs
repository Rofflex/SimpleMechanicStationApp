using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Media.Animation;

class DbWorking:AbstractDbWorking
    {
        public string connectionString { get; set; }
        public string SqlCommandText { get; set; }
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
        public override bool SqlQueryOutput(SqlCommand cmd)
        {
        //Checking Login and Password
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                bool ReaderHasRows = reader.HasRows;
                return ReaderHasRows;
            }
        }
        public override List<string> SqlQueryOutput(SqlCommand cmd, List<string> ValuesList) 
        {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read()) 
                {
                    ValuesList.Add("");
                }
            }
            return ValuesList;
        }
        public override SqlCommand ExecuteSqlQuery()
        {

            SqlConnection con;
            SqlCommand cmd;
            con = GetConnection();
            cmd = GetCommand(con);
            return cmd;
        }
    }
