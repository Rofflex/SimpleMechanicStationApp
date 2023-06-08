using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderButtonVMM.ViewModel;
using SimpleMechanicStationApp.GeneralVMM.OrderM.Model;
using System.Collections.Generic;

namespace SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands
{
    public interface IDBCommands
    {
        //SqlCommand GetCommand(SqlConnection con);

        /// <summary>
        /// returns int: 0 - no connection db; 1 - connection established but wrong log pass; 2 - connection established and Login Password are correct
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        int AuthUser(string UserName, string Password);
        void DownloadUserAccount(CurrentUser currentUserModel);
        List<OrderButtonViewModel> DownloadOrders();
        Order UpdateOrder(int orderId);
        List<T> GetItemsForOrder<T>(int orderId, string commandText);
    }
}
