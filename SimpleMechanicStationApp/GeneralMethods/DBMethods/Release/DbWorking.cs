using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Media.Animation;
using SimpleMechanicStationApp.GeneralMethods.DBMethods.Abstract;


namespace SimpleMechanicStationApp.GeneralMethods.DBMethods.Release
{
    public class DbWorking : AbstractDbWorking, IDbCommands
    {

        public int AuthUser(string Login, string Password) //return 0 - no connection db; 1 - wrong log pass; 2 - connection established
        {
            int ValidConnection;
            using (var con = GetConnection())
            {
                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "select Log, Pass from LogPass where Log = @Login and Pass = @Password";
                        cmd.Parameters.Add("Login", System.Data.SqlDbType.VarChar).Value = Login;
                        cmd.Parameters.Add("Password", System.Data.SqlDbType.VarChar).Value = Password;
                        ValidConnection = cmd.ExecuteScalar() == null ? 1 : 2;
                    }
                }
                catch(SqlException ex) 
                {
                    ValidConnection = 0;
                } 
            }
            return ValidConnection;
        }
        /*public override List<string> SqlQueryOutput(SqlCommand cmd, List<string> ValuesList)
        {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ValuesList.Add("");
                }
            }
            return ValuesList;
        }*/
    }

}