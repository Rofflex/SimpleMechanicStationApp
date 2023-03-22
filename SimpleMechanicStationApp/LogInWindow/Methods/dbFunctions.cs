using System;
using System.IO;
using System.Text.Json.Nodes;
using System.Windows;

namespace SimpleMechanicStationApp.LogInForm.Methods
{
    class dbFunctions
    {
        //String ConString = @"Data Source=LaptopAsus;Initial Catalog=dbSimpleMechanicStation;User ID=Test;Password=Fylhtq022";

        public string GetTheConnectionString()
        {
            string ConString;
            string DbConFile = @"ConFile.json";
            using (StreamReader sr = new StreamReader(DbConFile))
            {
                string json = sr.ReadToEnd();
                var result = JsonNode.Parse(json);
                string login = result["login"].ToString();
                string Password = result["password"].ToString();
                string Source = result["source"].ToString();
                string InitialCatalog = result["initial catalog"].ToString();
                ConString = @"Data Source=" + Source + @";Initial Catalog=" + InitialCatalog + @";User ID=" + login + @";Password=" + Password;
            }
            return ConString;
        }

        public short CheckLogPass(string Login, string Password)
        {
            short flag = 0;//0 - no connection with db; 1 - logpass are correct; 2 - logpass are incorrect
            try
            {
                string ConString = GetTheConnectionString();
                string SqlComTxt = SqlCommandText(Login, Password);
                var dbConnection = new DbWorking { connectionString = ConString, SqlCommandText = SqlComTxt };

                flag = dbConnection.SqlQueryOutput(dbConnection.ExecuteSqlQuery()) ? (short)1 : (short)2;
                return flag;
            }
            catch (Exception ex)
            {
                if (ex.Data.Contains("UserMessage"))
                {
                    MessageBox.Show(ex.Data["UserMessage"].ToString());
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
                return flag;
            }


        }
        private string SqlCommandText(string Login, string Password)
        {
            string SqlComTxt = "select Log, Pass from LogPass where Log = '" + Login + "' and Pass = '" + Password + "'";
            return SqlComTxt;
        }
    }
}
