using SimpleMechanicStationApp.GeneralMethods.DBMethods.DBConnection;
using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderButtonVMM.ViewModel;
using SimpleMechanicStationApp.GeneralVMM.OrderM.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands
{
    public class DBCommands : Connection, IDBCommands
    {
        private static readonly DBCommands instance = new DBCommands();
        private DBCommands()
        {

        }

        public static DBCommands Instance
        {
            get { return instance; }
        }

        public int AuthUser(string UserName, string Password) //return 0 - no connection db; 1 - wrong log pass; 2 - connection established
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
                        cmd.Parameters.Add("Login", System.Data.SqlDbType.VarChar).Value = UserName;
                        cmd.Parameters.Add("Password", System.Data.SqlDbType.VarChar).Value = Password;
                        ValidConnection = cmd.ExecuteScalar() == null ? 1 : 2;
                    }
                }
                catch (SqlException ex)
                {
                    ValidConnection = 0;
                }
                finally { con.Close(); }
            }
            return ValidConnection;
        }

        public List<OrderButtonViewModel> DownloadOrders()
        {
            var orders = new List<OrderButtonViewModel>();
            using (var con = GetConnection())
            {
                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "select OrderId, (CarMake +' '+ CarModel +' '+ VIN +' '+ Convert(varchar, CarYear) +' '+ CarPlate) as \"Summary\" " +
                            "from dbo.[Order] inner join dbo.[CarInfo] on dbo.[Order].CarId = dbo.[CarInfo].CarId";
                        using (var reader = cmd.ExecuteReader())
                        {
                            //If needed can be orginized as automated looker for index of column, Example: var IndexOrderId = reader.GetOrdinal("OrderId");

                            while (reader.Read())
                            {
                                orders.Add(new OrderButtonViewModel
                                {
                                    OrderId = reader.GetInt32(0),
                                    Summary = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally { con.Close(); }
            }
            return orders;
        }

        public void DownloadUserAccount(CurrentUser currentUserModel)
        {
            using (var con = GetConnection())
            {
                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "select Name, LastName, Email, PhoneNumber from LogPass where Log = @Login";
                        cmd.Parameters.Add("Login", System.Data.SqlDbType.VarChar).Value = currentUserModel.Username;
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentUserModel.Name = reader.IsDBNull(0) ? "" : reader.GetString(0);
                                currentUserModel.LastName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                currentUserModel.Email = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                currentUserModel.PhoneNumber = reader.IsDBNull(3) ? "" : reader.GetString(3);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally { con.Close(); }
            }
        }

        public Order UpdateOrder(int orderId)
        {
            var order = new Order();
            using (var con = GetConnection())
            {
                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "select [Order].CarId, CarMake, CarModel,CarOdometerStart, CarOdometerFinish, " +
                            "CarPlate, CarYear, CustomerName, CustomerPhone, CustomerAddress, OrderOpenDate, OrderCloseDate, " +
                            "[Order].VIN, (CarMake +' '+ CarModel) as CarName " +
                            "from Customer inner join [Order] on Customer.CustomerId=[Order].CustomerId " +
                            "inner join [CarInfo] on [Order].CarId = [CarInfo].CarId " +
                            "inner join VINList on VINList.VIN=[Order].VIN " +
                            "where OrderId=@OrderId";
                        cmd.Parameters.Add("OrderId", System.Data.SqlDbType.Int).Value = orderId;
                        order.OrderId = orderId;
                        using (var reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                order.CarId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                order.CarMake = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                order.CarModel = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                order.CarOdometerStart = reader.IsDBNull(3) ? 0 : reader.GetDouble(3);
                                order.CarOdometerFinish = reader.IsDBNull(4) ? 0 : reader.GetDouble(4);
                                order.CarPlate = reader.IsDBNull(5) ? "" : reader.GetString(5);
                                order.CarYear = reader.IsDBNull(6) ? 0 : reader.GetInt16(6);
                                order.CustomerName = reader.IsDBNull(7) ? "" : reader.GetString(7);
                                order.CustomerPhone = reader.IsDBNull(8) ? "" : reader.GetString(8);
                                order.CustomerAddress = reader.IsDBNull(9) ? "" : reader.GetString(9);
                                order.OrderOpenDate = reader.IsDBNull(10) ? DateTime.MinValue : reader.GetDateTime(10);
                                order.OrderCloseDate = reader.IsDBNull(11) ? DateTime.MinValue : reader.GetDateTime(11);
                                order.VIN = reader.IsDBNull(12) ? "" : reader.GetString(12);
                                order.CarName = reader.IsDBNull(13) ? "" : reader.GetString(13);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally { con.Close(); }
            }
            return order;
        }

        public List<T> GetItemsForOrder<T>(int orderId, string commandText)
        {
            var items = new List<T>();

            using (SqlConnection con = GetConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = commandText;
                        cmd.Parameters.Add("OrderId", System.Data.SqlDbType.Int).Value = orderId;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                T item = Activator.CreateInstance<T>();

                                var properties = typeof(T).GetProperties();
                                foreach (var property in properties)
                                {
                                    if (reader[property.Name] != DBNull.Value)
                                    {
                                        var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                        property.SetValue(item, value);
                                    }
                                    else
                                    {

                                    }
                                }

                                items.Add(item);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally { con.Close(); }
            }
            return items;
        }
    }
}