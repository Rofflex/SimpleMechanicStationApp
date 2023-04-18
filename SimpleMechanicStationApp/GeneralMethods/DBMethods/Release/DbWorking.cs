using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Media.Animation;
using SimpleMechanicStationApp.GeneralMethods.DBMethods.Abstract;


namespace SimpleMechanicStationApp.GeneralMethods.DBMethods.Release
{
    public class DbWorking : AbstractDbWorking, IDbCommands
    {
        public bool AuthUser(string Login, string Password)
        {
            bool ValidConnection;
            using (var con = GetConnection())
            using (var cmd = new SqlCommand())
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select Log, Pass from LogPass where Log = @Login and Pass = @Password";
                cmd.Parameters.Add("Login", System.Data.SqlDbType.NVarChar).Value = Login;
                cmd.Parameters.Add("Password", System.Data.SqlDbType.NVarChar).Value = Password;
                ValidConnection = cmd.ExecuteScalar() == null ? false : true;
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