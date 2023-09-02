using SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using System.Reflection;
using System;
using System.Windows;
using System.Collections.Generic;
using SimpleMechanicStationApp.GeneralVMM;

namespace SimpleMechanicStationApp.GeneralMethods.WindowViewModel
{
    public class WindowVM<T,I>:BaseWindowVM where T:BaseModel<I>, new()
    {
        // Fields
        private readonly string _selectQueryId, _selectQuery, _updateQuery, _uploadQuery;
        public readonly DBCommands _dbCommands = DBCommands.Instance;
        protected Dictionary<string, object> _nameIdPairs;
        private T _item;
        private T _prevItem;

        // Properties
        public T Item 
        { 
            get => _item; 
            set 
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }
        public T PrevItem
        {
            get => _prevItem;
            set
            {
                _prevItem = value;
                OnPropertyChanged(nameof(PrevItem));
            }
        }

        // Constructor
        /// <summary>
        /// Invoke a class constructor with getting Item. Gets from DBCommands.GetItem<T>()
        /// Every Item has clone created before any chages on it. Can get it from Property PrevItem.
        /// Save - assigned with a command of 4 Queries to DataBase.
        /// Edit - assigned
        /// </summary>
        /// <param name="itemId">Item ID to get from database.</param>
        /// <param name="selectQueryId">Query to check the same Item ID.</param>
        /// <param name="selectQuery">Query to check the same Item ID, name and other parameters.</param>
        /// <param name="updateQuery">Query to update values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="uploadQuery">Query to upload values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="getQuery">Query to get full information about Item from database</param>
        public WindowVM(object itemId, string selectQueryId, string selectQuery, string updateQuery, string uploadQuery, string getQuery) 
        {
            _item = _dbCommands.GetItem<T>(itemId, getQuery);
            _prevItem= (T)_item.Clone();
            _selectQueryId = selectQueryId;
            _selectQuery = selectQuery;
            _updateQuery = updateQuery;
            _uploadQuery = uploadQuery;
            Save = new ViewModelCommand<object>(ExecuteSave_4Query);
            Edit = new ViewModelCommand<object>(ExecuteEdit);
            IsReadOnly = true;
            IsEditing = false;
        }
        /// <summary>
        /// Invoke a class constructor with getting Item. 
        /// Every Item has clone created before any chages on it. Can get it from Property PrevItem.
        /// Save - assigned with a command of 3 Queries to DataBase.
        /// Edit - assigned
        /// </summary>
        /// <param name="itemId">Item ID to get from database.</param>
        /// <param name="selectQueryId">Query to check the same Item ID.</param>
        /// <param name="updateQuery">Query to update values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="uploadQuery">Query to upload values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="getQuery">Query to get full information about Item from database</param>
        public WindowVM(object itemId, string selectQueryId, string updateQuery, string uploadQuery, string getQuery)
        {
            _item = _dbCommands.GetItem<T>(itemId, getQuery);
            _prevItem = (T)_item.Clone();
            _selectQueryId = selectQueryId;
            _updateQuery = updateQuery;
            _uploadQuery = uploadQuery;
            Save = new ViewModelCommand<object>(ExecuteSave_3Query);
            Edit = new ViewModelCommand<object>(ExecuteEdit);
            IsReadOnly = true;
            IsEditing = false;
        }
        /// <summary>
        /// Invoke a class constructor with getting Item. 
        /// Every Item has clone created before any chages on it. Can get it from Property PrevItem.
        /// Save - assigned with a command of 3 Queries to DataBase with dictionary.
        /// Edit - assigned
        /// </summary>
        /// <param name="selectQueryId">Query to check the same Item ID.</param>
        /// <param name="updateQuery">Query to update values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="uploadQuery">Query to upload values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="getQuery">Query to get full information about Item from database</param>
        /// <param name="nameIdPairs">Dictionary <string, object> where string - name of property, object - value</param>
        public WindowVM(string selectQueryId, string updateQuery, string uploadQuery, string getQuery, Dictionary<string, object> nameIdPairs)
        {
            _nameIdPairs = nameIdPairs;
            _item = _dbCommands.GetItem<T>(getQuery, nameIdPairs);
            _prevItem = (T)_item.Clone();
            _selectQueryId = selectQueryId;
            _updateQuery = updateQuery;
            _uploadQuery = uploadQuery;
            Save = new ViewModelCommand<object>(ExecuteSave_3Query_Dictionary);
            Edit = new ViewModelCommand<object>(ExecuteEdit);
            IsReadOnly = true;
            IsEditing = false;
        }
        /// <summary>
        /// Invoke a class constructor with getting Item. 
        /// Every Item has clone created before any chages on it. Can get it from Property PrevItem.
        /// Edit - assigned
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="getQuery"></param>
        public WindowVM(object itemId, string getQuery)
        {
            _item = _dbCommands.GetItem<T>(itemId, getQuery);
            _prevItem = (T)_item.Clone();
            Edit = new ViewModelCommand<object>(ExecuteEdit);
            IsReadOnly = true;
            IsEditing = false;
        }
        /// <summary>
        /// Invoke a class constructor with creating empty Item.
        /// Save - not assigned
        /// Edit - assigned
        /// </summary>
        public WindowVM()
        {
            _item = new T();
            _prevItem = (T)_item.Clone();
            Edit = new ViewModelCommand<object>(ExecuteEdit);
            IsReadOnly = true;
            IsEditing = false;
        }
        /// <summary>
        /// Invoke a class constructor with creating Item with last ID which got from DataBase. 
        /// Save - assigned with a command of 4 Queries to DataBase
        /// Edit - assigned
        /// </summary>
        /// <param name="selectQueryId">Query to check the same Item ID.</param>
        /// <param name="selectQuery">Query to check the same Item ID, name and other parameters.</param>
        /// <param name="updateQuery">Query to update values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="uploadQuery">Query to upload values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="getQueryId">Query to get last Item ID plus 1 value</param>
        public WindowVM(string selectQueryId, string selectQuery, string updateQuery, string uploadQuery, string getQueryId)
        {
            _item = _dbCommands.GetItem<T>(getQueryId);
            _prevItem = (T)_item.Clone();
            _selectQueryId = selectQueryId;
            _selectQuery = selectQuery;
            _updateQuery = updateQuery;
            _uploadQuery = uploadQuery;
            Save = new ViewModelCommand<object>(ExecuteSave_4Query);
            Edit = new ViewModelCommand<object>(ExecuteEdit);
            IsReadOnly = false;
            IsEditing = true;
        }
        /// <summary>
        /// Invoke a class constructor with creating Item with last ID which got from DataBase. 
        /// Save - assigned with a command of 3 Queries to DataBase.
        /// Edit - assigned
        /// </summary>
        /// <param name="selectQueryId">Query to check the same Item ID.</param>
        /// <param name="updateQuery">Query to update values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="uploadQuery">Query to upload values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="getQueryId">Query to get last Item ID plus 1 value</param>
        public WindowVM(string selectQueryId, string updateQuery, string uploadQuery, string getQueryId)
        {
            _item = _dbCommands.GetItem<T>(getQueryId);
            _prevItem = (T)_item.Clone();
            _selectQueryId = selectQueryId;
            _updateQuery = updateQuery;
            _uploadQuery = uploadQuery;
            Save = new ViewModelCommand<object>(ExecuteSave_3Query);
            Edit = new ViewModelCommand<object>(ExecuteEdit);
            IsReadOnly = false;
            IsEditing = true;
        }
        /// <summary>
        /// Invoke a class constructor with creating empty Item. 
        /// Every Item has clone created before any chages on it. Can get it from Property PrevItem.
        /// Save - assigned with a command of 3 Queries to DataBase.
        /// Edit - assigned
        /// </summary>
        /// <param name="selectQueryId">Query to check the same Item ID.</param>
        /// <param name="updateQuery">Query to update values into the table. It can be update sql command or insert and delete.</param>
        /// <param name="uploadQuery">Query to upload values into the table. It can be update sql command or insert and delete.</param>
        public WindowVM(string selectQueryId, string updateQuery, string uploadQuery)
        {
            _item = new T();
            _prevItem = (T)_item.Clone();
            _selectQueryId = selectQueryId;
            _updateQuery = updateQuery;
            _uploadQuery = uploadQuery;
            Save = new ViewModelCommand<object>(ExecuteSave_3Query);
            Edit = new ViewModelCommand<object>(ExecuteEdit);
            IsReadOnly = false;
            IsEditing = true;
        }
        /// <summary>
        /// Invoke a class constructor with creating Item with last ID which got from DataBase. 
        /// Save - not assigned
        /// Edit - assigned
        /// </summary>
        /// <param name="getQueryId">Query to get last Item ID plus 1 value</param>
        public WindowVM(string getQueryId)
        {
            _item = _dbCommands.GetItem<T>(getQueryId);
            _prevItem = (T)_item.Clone();
            Edit = new ViewModelCommand<object>(ExecuteEdit);
            IsReadOnly = false;
            IsEditing = true;
        }
        /// <summary>
        /// Invoke class constructor with getting Item.
        /// Every Item has clone created before any chages on it. Can get it from Property PrevItem.
        /// Save - not assigned
        /// Edit - assigned
        /// </summary>
        /// <param name="item">Getting Item and clone it to PrevItem</param>
        public WindowVM(T item)
        {
            _item = item;
            _prevItem = (T)_item.Clone();
            Edit = new ViewModelCommand<object>(ExecuteEdit);
            IsReadOnly = true;
            IsEditing = false;
        }
        
        // Methods
        private void ExecuteSave_4Query(object obj)
        {
            int flag = 0;
            Type type = typeof(T);
            string typeName = type.Name;
            PropertyInfo propertyInfo = type.GetProperty($"{typeName}Name");
            object value = propertyInfo.GetValue(Item);
            var resultMB = MessageBox.Show("Do you want to save changes ?", "Save item", MessageBoxButton.YesNo);
            if (resultMB == MessageBoxResult.Yes)
            {
                if (value is not null && (string)value != "")
                {
                    flag = _dbCommands.SaveItem(_item, _selectQueryId, _selectQuery, _updateQuery, _uploadQuery);
                }
                else
                {
                    MessageBox.Show("No value in name.\nAdd value to name");
                    flag = 1;
                }
            }
            if (flag == 2)
            {
                _prevItem = (T)_item.Clone();
                IsEditing = false;
                IsReadOnly = true;
            }
        }
        private void ExecuteSave_3Query(object obj)
        {
            int flag = 0;
            Type type = typeof(T);
            string typeName = type.Name;
            PropertyInfo propertyInfo = type.GetProperty($"{typeName}Name");
            object value = propertyInfo.GetValue(Item);
            var resultMB = MessageBox.Show("Do you want to save changes ?", "Save item", MessageBoxButton.YesNo);
            if (resultMB == MessageBoxResult.Yes)
            {
                if (value is not null && (string)value != "")
                {
                    flag = _dbCommands.SaveItem(_item, _selectQueryId, _updateQuery, _uploadQuery);
                }
                else
                {
                    MessageBox.Show("No value in name.\nAdd value to name");
                    flag = 1;
                }
            }
            if (flag == 2)
            {
                _prevItem = (T)_item.Clone();
                IsEditing = false;
                IsReadOnly = true;
            }
        }
        private void ExecuteSave_3Query_Dictionary(object obj)
        {
            int flag = 0;
            Type type = typeof(T);
            string typeName = type.Name;
            PropertyInfo propertyInfo = type.GetProperty($"{typeName}Name");
            object value = propertyInfo.GetValue(Item);
            var resultMB = MessageBox.Show("Do you want to save changes ?", "Save item", MessageBoxButton.YesNo);
            if (resultMB == MessageBoxResult.Yes)
            {
                if (value is not null && (string)value != "")
                {
                    flag = _dbCommands.SaveItem(_item, _selectQueryId, _updateQuery, _uploadQuery, _nameIdPairs);
                }
                else
                {
                    MessageBox.Show("No value in name.\nAdd value to name");
                    flag = 1;
                }
            }
            if (flag == 2)
            {
                _prevItem = (T)_item.Clone();
                IsEditing = false;
                IsReadOnly = true;
            }
        }
        private void ExecuteEdit(object obj)
        {
            if (IsReadOnly)
            {
                IsReadOnly = false;
                IsEditing = true;
            }
        }
    }
}
