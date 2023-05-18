using SimpleMechanicStationApp.GeneralMethods.DBMethods.Models;
using SimpleMechanicStationApp.GeneralVMM.Model;
using SimpleMechanicStationApp.GeneralVMM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        int AuthUser(DbCurrentUserModel currentUserModel);
        DbCurrentUserModel DownloadUserAccount(string? name);
        List<Order> DownloadOrders();
    }
}
