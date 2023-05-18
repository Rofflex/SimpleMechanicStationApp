using System;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json.Nodes;

namespace SimpleMechanicStationApp.GeneralMethods.DBMethods.Abstract
{
    public abstract class AbstractDbWorking
    {
        private readonly string connectionString;

        public AbstractDbWorking() 
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
                ConString = $"Data Source={Source};Initial Catalog={InitialCatalog};User ID={Login};Password={Password}";
            }
            return ConString;
        }

        private string getUserName() 
        {
            return Environment.UserName;
        }
    }
}

