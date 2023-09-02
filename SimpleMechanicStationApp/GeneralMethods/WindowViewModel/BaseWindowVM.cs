using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using System.Windows.Input;

namespace SimpleMechanicStationApp.GeneralMethods.WindowViewModel
{
    public abstract class BaseWindowVM:ViewModelBase
    {
        // Fields
        private bool _isEditing;
        private bool _isReadOnly;

        // Properties
        public bool IsEditing 
        {
            get => _isEditing;
            set 
            { 
                _isEditing = value; 
                OnPropertyChanged(nameof(IsEditing));
            }
        }
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        public ICommand Save { get; set; }
        public ICommand Edit { get; set; }
    }
}
