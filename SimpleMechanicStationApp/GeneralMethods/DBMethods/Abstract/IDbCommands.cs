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

        /// <summary>
        /// returns int: 0 - no connection db; 1 - connection established but wrong log pass; 2 - connection established and Login Password are correct
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        int AuthUser(string UserName, string Password);
        //SqlCommand ExecuteSqlQuery();
        //List<string> SqlQueryOutput(SqlCommand cmd, List<string> ValuesList);
    }
}
