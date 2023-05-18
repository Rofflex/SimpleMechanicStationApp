using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Documents;
using SimpleMechanicStationApp.GeneralMethods.DBMethods.Abstract;
using SimpleMechanicStationApp.GeneralMethods.DBMethods.Models;
using SimpleMechanicStationApp.GeneralVMM.Model;
using SimpleMechanicStationApp.GeneralVMM.ViewModel;

namespace SimpleMechanicStationApp.GeneralMethods.DBMethods.Release
{
    public class DbWorking : AbstractDbWorking, IDbCommands
    {

        public int AuthUser(DbCurrentUserModel currentUserModel) //return 0 - no connection db; 1 - wrong log pass; 2 - connection established
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
                        cmd.Parameters.Add("Login", System.Data.SqlDbType.VarChar).Value = currentUserModel.Username;
                        cmd.Parameters.Add("Password", System.Data.SqlDbType.VarChar).Value = currentUserModel.Password;
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

        public List<Order> DownloadOrders()
        {
           var orders = new List<Order>();
            using (var con = GetConnection())
            {
                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "select OrderId, CarVIN, dbo.[Order].CarId, CarMake, CarModel, CarPlate, " +
                            "OrderOpenDate from dbo.[Order] inner join dbo.[CarInfo] on dbo.[Order].CarId = dbo.[CarInfo].CarId";
                        using (var reader = cmd.ExecuteReader())
                        {
                            //If needed can be orginized as automated looker for index of column, Example: var IndexOrderId = reader.GetOrdinal("OrderId");

                            while (reader.Read()) 
                            { 
                                var _carName = $"{reader.GetString(3)} {reader.GetString(4)}";
                                orders.Add(new Order { OrderId = reader.GetInt32(0), CarVIN = reader.GetString(1), CarId = reader.GetInt32(2), 
                                    CarName = _carName, CarPlate = reader.GetString(5), CarYear = reader.GetDateTime(6)}); 
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return orders;
        }

        public DbCurrentUserModel DownloadUserAccount(string UserName)
        {
            DbCurrentUserModel currentUserModel = new DbCurrentUserModel();
            using (var con = GetConnection())
            {
                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "select Name, LastName, Email, PhoneNumber from LogPass where Log = @Login";
                        cmd.Parameters.Add("Login", System.Data.SqlDbType.VarChar).Value = UserName;
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentUserModel.Username = UserName;
                                currentUserModel.Name = reader.GetString(0);
                                currentUserModel.LastName = reader.GetString(1);
                                currentUserModel.Email = reader.GetString(2);
                                currentUserModel.PhoneNumber = reader.GetString(3);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return currentUserModel;
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