using SimpleMechanicStationApp.GeneralMethods.DBMethods.Release;
using System;
using System.IO;
using System.Text.Json.Nodes;
using System.Windows;

namespace SimpleMechanicStationApp.LogInForm.Methods
{
    class dbFunctions
    {
        public short CheckLogPass(string Login, string Password)
        {
            short flag = 0;//0 - no connection with db; 1 - logpass are correct; 2 - logpass are incorrect
            try
            {
                DbWorking dbConnection= new DbWorking();
                
                flag = dbConnection.AuthUser(Login, Password) ? (short)1 : (short)2;
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
    }
}
