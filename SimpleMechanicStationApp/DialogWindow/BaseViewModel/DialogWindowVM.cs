using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using SimpleMechanicStationApp.GeneralMethods.WindowViewModel;
using SimpleMechanicStationApp.GeneralVMM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace SimpleMechanicStationApp.DialogWindow.BaseViewModel
{
    public class DialogWindowVM<M,VM,V,I> : BaseDialogWindowVM<M> 
        where M : BaseModel<I>, new()
        where VM: WindowVM<M,I>, new() 
        where V : Window, new()
    {
        // Fields
        private string _stringParam;
        private int _intParam;
        private Dictionary<string, object> _nameIdPair;
        // Properties

        // Constructor
        public DialogWindowVM(string getItems) 
        {
            Items = new ObservableCollection<M>(_dbCommands.GetItemsForList<M>(getItems));
            ChangeItem = new ViewModelCommand<object>(ExecuteChangeItem);
            AddItem = new ViewModelCommand<object>(ExecuteAddItem);
        }
        public DialogWindowVM(string getItems, string parameter)
        {
            _stringParam= parameter;
            Items = new ObservableCollection<M>(_dbCommands.GetItemsForList<M,string>(parameter, getItems));
            ChangeItem = new ViewModelCommand<object>(ExecuteChangeItemStr);
            AddItem = new ViewModelCommand<object>(ExecuteAddItem);
        }
        public DialogWindowVM(string getItems, int parameter)
        {
            _intParam= parameter;
            Items = new ObservableCollection<M>(_dbCommands.GetItemsForList<M, int>(parameter, getItems));
            ChangeItem = new ViewModelCommand<object>(ExecuteChangeItemInt);
            AddItem = new ViewModelCommand<object>(ExecuteAddItem);
        }
        public DialogWindowVM(string getItems, Dictionary<string, object> nameIdPair)
        {
            _nameIdPair = nameIdPair;
            Items = new ObservableCollection<M>(_dbCommands.GetItemsForList<M>(getItems, nameIdPair));
            ChangeItem = new ViewModelCommand<object>(ExecuteChangeItemDict);
            AddItem = new ViewModelCommand<object>(ExecuteAddItem);
        }

        // Methods
        private void ExecuteChangeItem(object param)
        {
            if (SelectedItem is not null)
            {
                ItemAction(SelectedItem.GetId());
            }
            else
            {
                MessageBox.Show("You have to choose item from the table");
            }
        }
        private void ExecuteChangeItemInt(object param)
        {
            if (SelectedItem is not null)
            {
                ItemAction(SelectedItem.GetId(), _intParam);
            }
            else
            {
                MessageBox.Show("You have to choose item from the table");
            }
        }
        private void ExecuteChangeItemStr(object param)
        {
            if (SelectedItem is not null)
            {
                ItemAction(SelectedItem.GetId(), _stringParam);
            }
            else
            {
                MessageBox.Show("You have to choose item from the table");
            }
        }
        private void ExecuteChangeItemDict(object param)
        {
            if (SelectedItem is not null)
            {
                ItemAction(_nameIdPair);
            }
            else
            {
                MessageBox.Show("You have to choose item from the table");
            }
        }
        private void ExecuteAddItem(object param)
        {
            ItemAction();
        }
        private void ItemAction()
        {
            V view = new V();
            VM viewModel = new VM();
            view.DataContext = viewModel;
            view.ShowDialog();

            var selectedItem = viewModel.Item;
            if (selectedItem is M && viewModel.IsEditing == false)
            {
                I selectedItemId = selectedItem.GetId();
                string selectedItemName = selectedItem.GetName();
                bool anyMatch = Items.Any(p => p.CompareId(selectedItemId));

                // Add new Item if not match and Item has name
                if ((!anyMatch) && selectedItemName is not null && selectedItemName != "")
                {
                    Items.Add(selectedItem);
                }
                else if (anyMatch) // If match then change current item to selected item in the item window from its viewmodel.
                {
                    var currentItem = Items.FirstOrDefault(p => p.CompareId(selectedItemId));
                    PropertyInfo[] properties = currentItem.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        var selectedItemProp = selectedItem.GetType().GetProperty(property.Name);
                        var selectedItemValue = selectedItemProp.GetValue(selectedItem);
                        if (property.CanWrite)
                        {
                            property.SetValue(currentItem, selectedItemValue);
                        }
                    }
                }
            }
        }
        private void ItemAction(I id)
        {
            V view = new V();
            VM viewModel = Activator.CreateInstance(typeof(VM),new object[] { id }) as VM;
            view.DataContext = viewModel;
            view.ShowDialog();

            var selectedItem = viewModel.Item;
            if (selectedItem is M && viewModel.IsEditing == false)
            {
                I selectedItemId = selectedItem.GetId();
                string selectedItemName = selectedItem.GetName();
                bool anyMatch = Items.Any(p => p.CompareId(selectedItemId));
                
                // Add new Item if not match and Item has name
                if ((!anyMatch) && selectedItemName is not null && selectedItemName != "")
                {
                    Items.Add(selectedItem);
                }
                else if (anyMatch) // If match then change current item to selected item in the item window from its viewmodel.
                {
                    var currentItem = Items.FirstOrDefault(p => p.CompareId(selectedItemId));
                    PropertyInfo[] properties = currentItem.GetType().GetProperties();
                    foreach(var property in properties) 
                    {
                        var selectedItemProp = selectedItem.GetType().GetProperty(property.Name);
                        var selectedItemValue = selectedItemProp.GetValue(selectedItem);
                        if (property.CanWrite)
                        {
                            property.SetValue(currentItem, selectedItemValue);
                        }
                    }
                }
            }
        }
        private void ItemAction(I id, int param)
        {
            V view = new V();
            VM viewModel = Activator.CreateInstance(typeof(VM), new object[] { id, param }) as VM;
            view.DataContext = viewModel;
            view.ShowDialog();

            var selectedItem = viewModel.Item;
            if (selectedItem is M && viewModel.IsEditing == false)
            {
                I selectedItemId = selectedItem.GetId();
                string selectedItemName = selectedItem.GetName();
                bool anyMatch = Items.Any(p => p.CompareId(selectedItemId));

                // Add new Item if not match and Item has name
                if ((!anyMatch) && selectedItemName is not null && selectedItemName != "")
                {
                    Items.Add(selectedItem);
                }
                else if (anyMatch) // If match then change current item to selected item in the item window from its viewmodel.
                {
                    var currentItem = Items.FirstOrDefault(p => p.CompareId(selectedItemId));
                    PropertyInfo[] properties = currentItem.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        var selectedItemProp = selectedItem.GetType().GetProperty(property.Name);
                        var selectedItemValue = selectedItemProp.GetValue(selectedItem);
                        if (property.CanWrite)
                        {
                            property.SetValue(currentItem, selectedItemValue);
                        }
                    }
                }
            }
        }
        private void ItemAction(I id, string param)
        {
            V view = new V();
            VM viewModel = Activator.CreateInstance(typeof(VM), new object[] { id, param }) as VM;
            view.DataContext = viewModel;
            view.ShowDialog();

            var selectedItem = viewModel.Item;
            if (selectedItem is M && viewModel.IsEditing == false)
            {
                I selectedItemId = selectedItem.GetId();
                string selectedItemName = selectedItem.GetName();
                bool anyMatch = Items.Any(p => p.CompareId(selectedItemId));

                // Add new Item if not match and Item has name
                if ((!anyMatch) && selectedItemName is not null && selectedItemName != "")
                {
                    Items.Add(selectedItem);
                }
                else if (anyMatch) // If match then change current item to selected item in the item window from its viewmodel.
                {
                    var currentItem = Items.FirstOrDefault(p => p.CompareId(selectedItemId));
                    PropertyInfo[] properties = currentItem.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        var selectedItemProp = selectedItem.GetType().GetProperty(property.Name);
                        var selectedItemValue = selectedItemProp.GetValue(selectedItem);
                        if (property.CanWrite)
                        {
                            property.SetValue(currentItem, selectedItemValue);
                        }
                    }
                }
            }
        }
        private void ItemAction(Dictionary<string, object> nameIdPair)
        {
            V view = new V();
            var currentItemType = SelectedItem.GetType();
            foreach (var pair in nameIdPair) 
            {
                var property = currentItemType.GetProperty(pair.Key);
                if (property is not null) 
                {
                    nameIdPair[pair.Key] = property.GetValue(SelectedItem);
                }
            }
            VM viewModel = Activator.CreateInstance(typeof(VM), new object[] { SelectedItem, nameIdPair }) as VM;
            view.DataContext = viewModel;
            view.ShowDialog();
            
            var selectedItem = viewModel.PrevItem;
            if (selectedItem is not null) 
            {
                PropertyInfo[] properties = selectedItem.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var selectedItemProp = selectedItem.GetType().GetProperty(property.Name);
                    var selectedItemValue = selectedItemProp.GetValue(selectedItem);
                    if (property.CanWrite)
                    {
                        property.SetValue(SelectedItem, selectedItemValue);
                    }
                }
            }
            /*if (selectedItem is M && viewModel.IsEditing == false)
            {
                I selectedItemId = selectedItem.GetId();
                string selectedItemName = selectedItem.GetName();
                bool anyMatch = Items.Any(p => p == selectedItem);

                // Add new Item if not match and Item has name
                if ((!anyMatch) && selectedItemName is not null && selectedItemName != "")
                {
                    Items.Add(selectedItem);
                }
                else if (anyMatch) // If match then change current item to selected item in the item window from its viewmodel.
                {
                    var currentItem = Items.FirstOrDefault(p => p.CompareId(selectedItemId));
                    PropertyInfo[] properties = currentItem.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        var selectedItemProp = selectedItem.GetType().GetProperty(property.Name);
                        var selectedItemValue = selectedItemProp.GetValue(selectedItem);
                        if (property.CanWrite)
                        {
                            property.SetValue(currentItem, selectedItemValue);
                        }
                    }
                }
            }*/
        }

    }
}
