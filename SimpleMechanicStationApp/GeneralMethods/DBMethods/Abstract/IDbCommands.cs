using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMechanicStationApp.GeneralMethods.DBMethods.Abstract
{
    public interface IDbCommands
    {
        //SqlCommand GetCommand(SqlConnection con);
        bool AuthUser(string UserName, string Password);
        //SqlCommand ExecuteSqlQuery();
        //List<string> SqlQueryOutput(SqlCommand cmd, List<string> ValuesList);
    }
}
