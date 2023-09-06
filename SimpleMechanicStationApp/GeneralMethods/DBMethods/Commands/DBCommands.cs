using SimpleMechanicStationApp.GeneralMethods.DBMethods.DBConnection;
using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderButtonVMM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands
{
    public class DBCommands : Connection, IDBCommands
    {
        // Singleton
        private static readonly DBCommands instance = new DBCommands();
        private DBCommands()
        {

        }
        public static DBCommands Instance => instance;

        // Methods
        public int AuthUser(string userName, string password)
        {
            int validConnection;
            using (var con = GetConnection())
            {
                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "select Log, Pass from LogPass where Log = @Login and Pass = @Password";
                        cmd.Parameters.AddWithValue("Login", userName);
                        cmd.Parameters.AddWithValue("Password", password);
                        validConnection = cmd.ExecuteScalar() == null ? 1 : 2;
                    }
                }
                catch (SqlException)
                {
                    validConnection = 0;
                }
                finally { con.Close(); }
            }
            return validConnection;
        }
        public void DownloadUserAccount(CurrentUser currentUserModel)
        {
            using (var con = GetConnection())
            {
                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "select Name, LastName, Email, PhoneNumber from LogPass where Log = @Login";
                        cmd.Parameters.Add("Login", System.Data.SqlDbType.VarChar).Value = currentUserModel.Username;
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentUserModel.Name = reader.IsDBNull(0) ? "" : reader.GetString(0);
                                currentUserModel.LastName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                currentUserModel.Email = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                currentUserModel.PhoneNumber = reader.IsDBNull(3) ? "" : reader.GetString(3);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally { con.Close(); }
            }
        }
        public List<T> GetItemsForList<T, K>(K id, string commandText)
        {
            var items = new List<T>();
            using (SqlConnection con = GetConnection())
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(commandText, con))
                    {
                        cmd.Parameters.AddWithValue("id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                T item = Activator.CreateInstance<T>();

                                var properties = typeof(T).GetProperties();
                                foreach (var property in properties)
                                {
                                    bool hasReaderProperty = CheckReaderProperty(reader, property.Name);

                                    if (hasReaderProperty)
                                    {
                                        var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                        property.SetValue(item, value);
                                    }
                                }

                                items.Add(item);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally { con.Close(); }
            }
            return items;
        }
        public List<T> GetItemsForList<T>(string commandText, Dictionary<string, object> nameIdPair)
        {
            var items = new List<T>();
            using (SqlConnection con = GetConnection())
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(commandText, con))
                    {
                        foreach(var pair in nameIdPair) 
                        {
                            if (commandText.Contains($"@{pair.Key}"))
                            {
                                cmd.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                            }
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                T item = Activator.CreateInstance<T>();

                                var properties = typeof(T).GetProperties();
                                foreach (var property in properties)
                                {
                                    bool hasReaderProperty = CheckReaderProperty(reader, property.Name);

                                    if (hasReaderProperty)
                                    {
                                        var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                        property.SetValue(item, value);
                                    }
                                }

                                items.Add(item);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally { con.Close(); }
            }
            return items;
        }
        public List<T> GetItemsForList<T>(string commandText)
        {
            var items = new List<T>();

            using (SqlConnection con = GetConnection())
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(commandText, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                T item = Activator.CreateInstance<T>();

                                var properties = typeof(T).GetProperties();
                                foreach (var property in properties)
                                {
                                    bool hasReaderProperty = CheckReaderProperty(reader, property.Name);

                                    if (hasReaderProperty)
                                    {
                                        var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                        property.SetValue(item, value);
                                    }
                                }

                                items.Add(item);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally { con.Close(); }
            }
            return items;
        }
        public int SaveItem(object item, string selectQueryId, string selectQuery, string updateQuery, string uploadQuery, Dictionary<string, object> nameIdPairs)
        {
            int flag = 0;
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    var itemType = item.GetType();
                    var itemProperties = itemType.GetProperties();
                    if (item is not null)
                    {
                        object result;
                        // Check item with nameId pair                        
                        using (SqlCommand selectCommand = new SqlCommand(selectQueryId, con, transaction))
                        {
                            selectCommand.Parameters.Clear();
                            foreach (var pair in nameIdPairs)
                            {
                                if (!selectCommand.Parameters.Contains($"@{pair.Key}") && selectCommand.CommandText.Contains($"@{pair.Key}"))
                                {
                                    selectCommand.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                                }
                            }
                            result = selectCommand.ExecuteScalar();
                        }

                        // If Id is not found, check the same item and other properties. And replace result
                        if (result is null)
                        {
                            using (SqlCommand selectCommand = new SqlCommand(selectQuery, con, transaction))
                            {
                                selectCommand.Parameters.Clear();
                                foreach (var property in itemProperties)
                                {
                                    var value = property.GetValue(item);
                                    if (value is not null && selectCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        selectCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                    else if (value is null && selectCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        var propertyType = property.PropertyType;
                                        value = propertyType == typeof(string) ?
                                            "" : propertyType == typeof(int) ?
                                            0 : propertyType == typeof(decimal) ?
                                            0 : propertyType == typeof(DateTime) ?
                                            DateTime.Now : 0;
                                        selectCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                }
                                result = selectCommand.ExecuteScalar();
                            }
                        }

                        // If the same item is found than update properties
                        if (result is not null)
                        {
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con, transaction))
                            {
                                updateCommand.Parameters.Clear();
                                foreach (var property in itemProperties)
                                {
                                    var value = property.GetValue(item);
                                    if (value is not null && updateCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        updateCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                    else if (value is null && updateCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        var propertyType = property.PropertyType;
                                        value = propertyType == typeof(string) ?
                                            "" : propertyType == typeof(int) ?
                                            0 : propertyType == typeof(decimal) ?
                                            0 : propertyType == typeof(DateTime) ?
                                            DateTime.Now : 0;
                                        updateCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                }
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        // Otherwise insert new values to the table
                        else
                        {
                            using (SqlCommand uploadCommand = new SqlCommand(uploadQuery, con, transaction))
                            {
                                uploadCommand.Parameters.Clear();
                                foreach (var property in itemProperties)
                                {
                                    var value = property.GetValue(item);
                                    if (value is not null && uploadCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        uploadCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                    else if (value is null && uploadCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        var propertyType = property.PropertyType;
                                        value = propertyType == typeof(string) ?
                                            "" : propertyType == typeof(int) ?
                                            0 : propertyType == typeof(decimal) ?
                                            0 : propertyType == typeof(DateTime) ?
                                            DateTime.Now : 0;
                                        uploadCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                }
                                foreach (var pair in nameIdPairs)
                                {
                                    if (!uploadCommand.Parameters.Contains($"@{pair.Key}") && uploadCommand.CommandText.Contains($"@{pair.Key}"))
                                    {
                                        uploadCommand.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                                    }
                                }
                                uploadCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                    flag = 2;
                }
                catch (Exception ex)
                {
                    flag = 1;
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }
                finally { con.Close(); }
            }
            return flag;
        }
        public int SaveItem(object item, string selectQueryId, string selectQuery, string updateQuery, string uploadQuery) 
        {
            int flag = 0;
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    var itemType = item.GetType();
                    var itemProperties = itemType.GetProperties();
                    if (item is not null)
                    {
                        object result;
                        // Check item with Id                        
                        using (SqlCommand selectCommand = new SqlCommand(selectQueryId, con, transaction))
                        {
                            selectCommand.Parameters.Clear();
                            foreach (var property in itemProperties)
                            {
                                var propertyName = property.Name;

                                if (selectCommand.CommandText.Contains($"@{propertyName}"))
                                {
                                    var value = property.GetValue(item);
                                    selectCommand.Parameters.AddWithValue($"@{propertyName}", value);
                                    break;
                                }
                            }
                            result = selectCommand.ExecuteScalar();
                        }

                        // If Id is not found, check the same item and other properties. And replace result
                        if (result is null)
                        {                         
                            using (SqlCommand selectCommand = new SqlCommand(selectQuery, con, transaction))
                            {
                                selectCommand.Parameters.Clear();
                                foreach (var property in itemProperties)
                                {
                                    var value = property.GetValue(item);
                                    if (value is not null && selectCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        selectCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                    else if (value is null && selectCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        var propertyType = property.PropertyType;
                                        value = propertyType == typeof(string) ?
                                            "" : propertyType == typeof(int) ?
                                            0 : propertyType == typeof(decimal) ?
                                            0 : propertyType == typeof(DateTime) ?
                                            DateTime.Now : 0;
                                        selectCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                }
                                result = selectCommand.ExecuteScalar();
                            }
                        }

                        // If the same item is found than update properties
                        if (result is not null)
                        {
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con, transaction))
                            {
                                updateCommand.Parameters.Clear();
                                foreach (var property in itemProperties)
                                {
                                    var value = property.GetValue(item);
                                    if (value is not null && updateCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        updateCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                    else if (value is null && updateCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        var propertyType = property.PropertyType;
                                        value = propertyType == typeof(string) ?
                                            "" : propertyType == typeof(int) ?
                                            0 : propertyType == typeof(decimal) ?
                                            0 : propertyType == typeof(DateTime) ?
                                            DateTime.Now : 0;
                                        updateCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                }
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        // Otherwise insert new values to the table
                        else
                        {
                            using (SqlCommand uploadCommand = new SqlCommand(uploadQuery, con, transaction))
                            {
                                uploadCommand.Parameters.Clear();
                                foreach (var property in itemProperties)
                                {
                                    var value = property.GetValue(item);
                                    if (value is not null && uploadCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        uploadCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                    else if (value is null && uploadCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        var propertyType = property.PropertyType;
                                        value = propertyType == typeof(string) ?
                                            "" : propertyType == typeof(int) ?
                                            0 : propertyType == typeof(decimal) ?
                                            0 : propertyType == typeof(DateTime) ?
                                            DateTime.Now : 0;
                                        uploadCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                }
                                uploadCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                    flag = 2;
                }
                catch (Exception ex)
                {
                    flag = 1;
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }
                finally { con.Close(); }
            }
            return flag;
        }
        public int SaveItem(object item, string selectQueryId, string updateQuery, string uploadQuery, Dictionary<string, object> nameIdPairs)
        {
            int flag = 0;
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    var itemType = item.GetType();
                    var itemProperties = itemType.GetProperties();
                    if (item is not null)
                    {
                        object result;
                        // Check item with Id                        
                        using (SqlCommand selectCommand = new SqlCommand(selectQueryId, con, transaction))
                        {
                            selectCommand.Parameters.Clear();
                            foreach (var pair in nameIdPairs)
                            {
                                if (pair.Value is not null && selectCommand.CommandText.Contains($"@{pair.Key}"))
                                {
                                    selectCommand.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                                }
                            }
                            foreach (var property in itemProperties)
                            {
                                var propertyName = property.Name;
                                if (!selectCommand.Parameters.Contains($"@{propertyName}") && selectCommand.CommandText.Contains($"@{propertyName}"))
                                {
                                    var value = property.GetValue(item);
                                    selectCommand.Parameters.AddWithValue($"@{propertyName}", value);
                                }
                            }
                            result = selectCommand.ExecuteScalar();
                        }

                        // If the same item is found than update properties
                        if (result is not null)
                        {
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con, transaction))
                            {
                                updateCommand.Parameters.Clear();
                                foreach (var pair in nameIdPairs)
                                {
                                    if (pair.Value is not null && updateCommand.CommandText.Contains($"@{pair.Key}") )
                                    {
                                        updateCommand.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                                    }
                                }
                                foreach (var property in itemProperties)
                                {
                                    var value = property.GetValue(item);
                                    if (value is not null && updateCommand.CommandText.Contains($"@{property.Name}") && !updateCommand.Parameters.Contains($"@{property.Name}"))
                                    {
                                        updateCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                    else if (value is null && updateCommand.CommandText.Contains($"@{property.Name}") && !updateCommand.Parameters.Contains($"@{property.Name}"))
                                    {
                                        var propertyType = property.PropertyType;
                                        value = propertyType == typeof(string) ?
                                            "" : propertyType == typeof(int) ?
                                            0 : propertyType == typeof(decimal) ?
                                            0 : propertyType == typeof(DateTime) ?
                                            DateTime.Now : 0;
                                        updateCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                }
                                
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        // Otherwise insert new values to the table
                        else
                        {
                            using (SqlCommand uploadCommand = new SqlCommand(uploadQuery, con, transaction))
                            {
                                uploadCommand.Parameters.Clear();
                                foreach (var pair in nameIdPairs)
                                {
                                    if (pair.Value is not null && uploadCommand.CommandText.Contains($"@{pair.Key}"))
                                    {
                                        uploadCommand.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                                    }
                                }
                                foreach (var property in itemProperties)
                                {
                                    var value = property.GetValue(item);
                                    if (value is not null && !uploadCommand.Parameters.Contains($"@{property.Name}") && uploadCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        uploadCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                    else if (value is null && !uploadCommand.Parameters.Contains($"@{property.Name}") && uploadCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        var propertyType = property.PropertyType;
                                        value = propertyType == typeof(string) ?
                                            "" : propertyType == typeof(int) ?
                                            0 : propertyType == typeof(decimal) ?
                                            0 : propertyType == typeof(DateTime) ?
                                            DateTime.Now : 0;
                                        uploadCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                }
                                uploadCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                    flag = 2;
                }
                catch (Exception ex)
                {
                    flag = 1;
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }
                finally { con.Close(); }
            }
            return flag;
        }
        public int SaveItem(object item, string selectQueryId, string updateQuery, string uploadQuery)
        {
            int flag = 0;
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    var itemType = item.GetType();
                    var itemProperties = itemType.GetProperties();
                    if (item is not null)
                    {
                        object result;
                        // Check item with Id                        
                        using (SqlCommand selectCommand = new SqlCommand(selectQueryId, con, transaction))
                        {
                            selectCommand.Parameters.Clear();
                            foreach (var property in itemProperties)
                            {
                                var propertyName = property.Name;
                                var propertyNameId = propertyName.Substring(propertyName.Length - 2); // Gets last 2 string letter
                                
                                if(propertyNameId == "Id" && selectCommand.CommandText.Contains($"@{ propertyName}"))
                                {
                                    var value = property.GetValue(item);
                                    selectCommand.Parameters.AddWithValue($"@{propertyName}", value);
                                }
                            }
                            result = selectCommand.ExecuteScalar();
                        }

                        // If the same item is found than update properties
                        if (result is not null)
                        {
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con, transaction))
                            {
                                updateCommand.Parameters.Clear();
                                foreach (var property in itemProperties)
                                {
                                    var value = property.GetValue(item);
                                    if (value is not null && updateCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        updateCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                    else if (value is null && updateCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        var propertyType = property.PropertyType;
                                        value = propertyType == typeof(string) ?
                                            "" : propertyType == typeof(int) ?
                                            0 : propertyType == typeof(decimal) ?
                                            0 : propertyType == typeof(DateTime) ?
                                            DateTime.Now : 0;
                                        updateCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                }
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        // Otherwise insert new values to the table
                        else
                        {
                            using (SqlCommand uploadCommand = new SqlCommand(uploadQuery, con, transaction))
                            {
                                uploadCommand.Parameters.Clear();
                                foreach (var property in itemProperties)
                                {
                                    var value = property.GetValue(item);
                                    if (value is not null && uploadCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        uploadCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                    else if (value is null && uploadCommand.CommandText.Contains($"@{property.Name}"))
                                    {
                                        var propertyType = property.PropertyType;
                                        value = propertyType == typeof(string) ?
                                            "" : propertyType == typeof(int) ?
                                            0 : propertyType == typeof(decimal) ?
                                            0 : propertyType == typeof(DateTime) ?
                                            DateTime.Now : 0;
                                        uploadCommand.Parameters.AddWithValue($"@{property.Name}", value);
                                    }
                                }
                                uploadCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                    flag = 2;
                }
                catch (Exception ex)
                {
                    flag = 1;
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }
                finally { con.Close(); }
            }
            return flag;
        }
        public T GetItem<T>(string selectQuery) where T : class 
        {
            var item = Activator.CreateInstance<T>();
            var itemType = item.GetType();
            var itemProperties = itemType.GetProperties();
            using (var con = GetConnection())
            {
                try
                {
                    con.Open();
                    using (var cmd = new SqlCommand(selectQuery, con))
                    {
                        foreach (var property in itemProperties)
                        {
                            var propertyName = property.Name;
                            var propertyNameId = propertyName.Substring(propertyName.Length - 2); // Gets last 2 string letter
                            var propertyNameItem = propertyName.Substring(0, propertyName.Length - 2); // Gets the first string letters before Id
                            if (propertyNameId == "Id" && propertyNameItem == itemType.Name)
                            {
                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        var checkValue = property.GetValue(item);
                                        if (checkValue != null)
                                        {
                                            var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                            property.SetValue(item, value);
                                        }
                                    }
                                }
                                break;
                            }
                        }

                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally { con.Close(); }
            }
            return item;
        }
        public T GetItem<T>(object Id, string selectQuery) where T : class
        {
            var item = Activator.CreateInstance<T>();
            var itemType = item.GetType();
            var itemProperties = itemType.GetProperties();
            if (Id is not null)
            {
                using (var con = GetConnection())
                {
                    try
                    {
                        con.Open();
                        using (var cmd = new SqlCommand(selectQuery, con))
                        {
                            cmd.Parameters.Clear();
                            foreach (var property in itemProperties)
                            {
                                var propertyName = property.Name;
                                var propertyNameId = propertyName.Substring(propertyName.Length - 2); // Gets last 2 string letter

                                if (propertyNameId == "Id" && cmd.CommandText.Contains($"@{propertyName}"))
                                {
                                    cmd.Parameters.AddWithValue($"@{propertyName}", Id);
                                    break;
                                }
                                else if (cmd.CommandText.Contains("@Id")) 
                                {
                                    cmd.Parameters.AddWithValue("@Id", Id);
                                    break;
                                }
                            }
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    foreach (var property in itemProperties)
                                    {
                                        bool hasReaderProperty = CheckReaderProperty(reader, property.Name);
                                        if (hasReaderProperty)
                                        {
                                            var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                            property.SetValue(item, value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally { con.Close(); }
                }
                return item;
            }
            else
            {
                return item;
            }
        }
        public T GetItem<T>(string selectQuery, Dictionary<string, object> nameIdPairs) where T : class
        {
            var item = Activator.CreateInstance<T>();
            var itemType = item.GetType();
            var itemProperties = itemType.GetProperties();
            using (var con = GetConnection())
            {
                try
                {
                    con.Open();
                    using (var cmd = new SqlCommand(selectQuery, con))
                    {
                        foreach (var pair in nameIdPairs)
                        {
                            if (cmd.CommandText.Contains($"@{pair.Key}"))
                            {
                                cmd.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                            }
                        }
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                foreach (var property in itemProperties)
                                {
                                    bool hasReaderProperty = CheckReaderProperty(reader, property.Name);
                                    if (hasReaderProperty)
                                    {
                                        var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                        property.SetValue(item, value);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally { con.Close(); }
            }
            return item;
        }
        private bool CheckReaderProperty (SqlDataReader reader, string property) 
        {
            bool flag;
            try 
            {
                flag = reader[property] != DBNull.Value ? true : false;
            }
            catch(Exception) 
            {
                flag = false;
            }
            return flag;
        }
    }
}