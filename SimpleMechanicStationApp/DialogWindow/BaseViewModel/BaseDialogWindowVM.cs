using SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands;
using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SimpleMechanicStationApp.DialogWindow.BaseViewModel
{
    public abstract class BaseDialogWindowVM<T> : ViewModelBase
    {
        // Fields
        public readonly IDBCommands _dbCommands = DBCommands.Instance;
        private ObservableCollection<T> _items;
        private T _selectedItem;

        // Properties
        public ObservableCollection<T> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ICommand ChangeItem { get; set; }
        public ICommand AddItem { get; set; }
        public ICommand DeleteItem { get; set; }

    }
}
