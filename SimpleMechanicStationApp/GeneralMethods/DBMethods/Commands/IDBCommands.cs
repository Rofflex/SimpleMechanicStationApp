using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands
{
    public interface IDBCommands
    {
        /// <summary>
        /// Check username and password in DB.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>return 0 - no connection db; 1 - wrong log pass; 2 - connection established</returns>
        int AuthUser(string UserName, string Password);
        /// <summary>
        /// Download full information about user account.
        /// </summary>
        /// <param name="currentUserModel"></param>
        void DownloadUserAccount(CurrentUser currentUserModel);
        /// <summary>
        /// Get list of items from DB with one parameter.
        /// </summary>
        /// <typeparam name="T">item type</typeparam>
        /// <typeparam name="K">id type</typeparam>
        /// <param name="id">item id</param>
        /// <param name="commandText">Query to take list of items type T. In query should be variable @id</param>
        /// <returns>List with items type T</returns>
        List<T> GetItemsForList<T, K>(K id, string commandText);
        /// <summary>
        /// Get list of items from DB with multiply parameters.
        /// </summary>
        /// <typeparam name="T">item type</typeparam>
        /// <param name="commandText">Query to take list of items type T.</param>
        /// <param name="nameIdPair">Dictionary of mulitply parameters</param>
        /// <returns>List with items type T</returns>
        List<T> GetItemsForList<T>(string commandText, Dictionary<string, object> nameIdPair);
        /// <summary>
        /// Get list of items from DB without parameters.
        /// </summary>
        /// <typeparam name="T">item type</typeparam>
        /// <param name="commandText">Query to take list of items type T.</param>
        /// <returns>List with items type T</returns>
        List<T> GetItemsForList<T>(string commandText);
        /// <summary>
        /// Get SQLConnection. It uses ConFile.json ConFile which is located on Documents\\Mechanic Station\\Client
        /// </summary>
        /// <returns>Connection</returns>
        SqlConnection GetConnection();
        /// <summary>
        /// Save item in DB. Check item type. Take properties and their values to save item.
        /// 1) Check the same ItemId with assigning a value to a variable "result" if found.
        /// 2) Check the same Item properties with assigning a value to a variable "result" if found
        /// 3) if result is not null it means that something found. Then use updateQuery
        /// 4) if result is null it means that nothing found. Then use uploadQuery
        /// </summary>
        /// <param name="item">item which should be saved</param>
        /// <param name="selectQueryId">Query to check the same Item ID.</param>
        /// <param name="selectQuery">Query to check the same Item ID, name and other parameters.</param>
        /// <param name="updateQuery">Query to update values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="uploadQuery">Query to upload values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="nameIdPairs">Dictionary <string, object> where string - name of property, object - value</param>
        /// <returns>0 - no connection, 1 - DB exception, 2 - queries executed</returns>
        int SaveItem(object item, string selectQueryId, string selectQuery, string updateQuery, string uploadQuery, Dictionary<string, object> nameIdPairs);
        /// <summary>
        /// Save item in DB. Check item type. Take properties and their values to save item.
        /// 1) Check the same ItemId with assigning a value to a variable "result" if found.
        /// 2) Check the same Item properties with assigning a value to a variable "result" if found
        /// 3) if result is not null it means that something found. Then use updateQuery
        /// 4) if result is null it means that nothing found. Then use uploadQuery
        /// </summary>
        /// <param name="item">item which should be saved</param>
        /// <param name="selectQueryId">Query to check the same Item ID.</param>
        /// <param name="selectQuery">Query to check the same Item ID, name and other parameters.</param>
        /// <param name="updateQuery">Query to update values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="uploadQuery">Query to upload values into the table. It can be update sql command or insert and delete.</param>
        /// <returns>0 - no connection, 1 - DB exception, 2 - queries executed</returns>
        int SaveItem(object item, string selectQueryId, string selectQuery, string updateQuery, string uploadQuery);
        /// <summary>
        /// Save item in DB. Check item type. Take properties and their values to save item.
        /// 1) Check the same ItemId with assigning a value to a variable "result" if found.
        /// 2) if result is not null it means that something found. Then use updateQuery
        /// 3) if result is null it means that nothing found. Then use uploadQuery
        /// </summary>
        /// <param name="item">item which should be saved</param>
        /// <param name="selectQueryId">Query to check the same Item ID.</param>
        /// <param name="updateQuery">Query to update values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="uploadQuery">Query to upload values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="nameIdPairs">Dictionary <string, object> where string - name of property, object - value</param>
        /// <returns>0 - no connection, 1 - DB exception, 2 - queries executed</returns>
        int SaveItem(object item, string selectQueryId, string updateQuery, string uploadQuery, Dictionary<string, object> nameIdPairs);
        /// <summary>
        /// Save item in DB. Check item type. Take properties and their values to save item.
        /// 1) Check the same ItemId with assigning a value to a variable "result" if found.
        /// 2) if result is not null it means that something found. Then use updateQuery
        /// 3) if result is null it means that nothing found. Then use uploadQuery
        /// </summary>
        /// <param name="item">item which should be saved</param>
        /// <param name="selectQueryId">Query to check the same Item ID.</param>
        /// <param name="updateQuery">Query to update values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="uploadQuery">Query to upload values into the table. It can be update sql command or insert and delete.</param>
        /// <returns>0 - no connection, 1 - DB exception, 2 - queries executed</returns>
        int SaveItem(object item, string selectQueryId, string updateQuery, string uploadQuery);
        /// <summary>
        /// Get item from database without parameters. That means it gets just last item id
        /// </summary>
        /// <typeparam name="T">item type</typeparam>
        /// <param name="selectQuery">Query to get last item Id</param>
        /// <returns>item with type T</returns>
        T GetItem<T>(string selectQuery) where T : class;
        /// <summary>
        /// Get item from database with parameter item Id.
        /// </summary>
        /// <typeparam name="T">item type</typeparam>
        /// <param name="Id">item Id</param>
        /// <param name="selectQuery">Query to get item with definate Id</param>
        /// <returns>item with type T</returns>
        T GetItem<T>(object Id, string selectQuery) where T : class;
        /// <summary>
        /// Get item from database with multiply parameters from Dictionary nameIdPairs.
        /// </summary>
        /// <typeparam name="T">item type</typeparam>
        /// <param name="selectQuery">Query to get item with multiply where</param>
        /// <param name="nameIdPairs">Dictionary <string, object> where string - name of property, object - value</param>
        /// <returns></returns>
        T GetItem<T>(string selectQuery, Dictionary<string, object> nameIdPairs) where T : class;
    }
}
