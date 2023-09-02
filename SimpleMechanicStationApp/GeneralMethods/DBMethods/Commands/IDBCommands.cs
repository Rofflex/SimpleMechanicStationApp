using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderButtonVMM.ViewModel;
using System.Collections.Generic;

namespace SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands
{
    public interface IDBCommands
    {
        /// <summary>
        /// returns int: 0 - no connection db; 1 - connection established but wrong log pass; 2 - connection established and Login Password are correct
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        int AuthUser(string UserName, string Password);
        void DownloadUserAccount(CurrentUser currentUserModel);
        List<OrderButtonViewModel> DownloadOrders();
        List<T> GetItemsForList<T, K>(K id, string commandText);
        List<T> GetItemsForList<T>(string commandText);

        /// <summary>
        /// Saving item in database
        /// </summary>
        /// <param name="item">Item which needs to be saved</param>
        /// <param name="selectQueryId">Query to check the same item Id</param>
        /// <param name="selectQuery">Query to check the same item names</param>
        /// <param name="updateQuery">Query to update values</param>
        /// <param name="uploadQuery">Query to upload values</param>
        /// <returns></returns>
        int SaveItem(object item, string selectQueryId, string selectQuery, string updateQuery, string uploadQuery);
        int SaveItem(object item, string selectQueryId, string updateQuery, string uploadQuery);
        T GetItem<T>(string selectQuery) where T : class;
        T GetItem<T>(object Id, string selectQuery) where T : class;
    }
}
