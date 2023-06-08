using System;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json.Nodes;

namespace SimpleMechanicStationApp.GeneralMethods.DBMethods.DBConnection
{
    /// <summary>
    /// Returns established connection
    /// Connection string assembled using credentials from ConFile which is located on Documents\\Mechanic Station\\Client
    /// </summary>
    public abstract class Connection
    {
        private readonly string connectionString;

        public Connection()
        {
            connectionString = GetConnectionString();
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        private string GetConnectionString()
        {
            string ConString;
            string UserName = getUserName();
            string DbConFile = $"C:\\Users\\{UserName}\\Documents\\Mechanic Station\\Client\\ConFile.json";
            using (StreamReader sr = new StreamReader(DbConFile))
            {
                string json = sr.ReadToEnd();
                var result = JsonNode.Parse(json);
                string Login = result["login"].ToString();
                string Password = result["password"].ToString();
                string Source = result["source"].ToString();
                string InitialCatalog = result["initial catalog"].ToString();
                ConString = $"Data Source={Source};Initial Catalog={InitialCatalog};User ID={Login};Password={Password};Connection Timeout=5";
            }
            return ConString;
        }

        private string getUserName()
        {
            return Environment.UserName;
        }
    }
}

