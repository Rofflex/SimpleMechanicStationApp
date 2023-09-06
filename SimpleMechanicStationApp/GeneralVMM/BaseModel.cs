using SimpleMechanicStationApp.GeneralMethods.ViewModelBaseCommand;
using System;
using System.Collections.Generic;

namespace SimpleMechanicStationApp.GeneralVMM
{
    public abstract class BaseModel<T> : ViewModelBase,ICloneable
    {
        // Fields
        private T _id;
        private string _name;

        // Properties
        protected T Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        protected string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        // Methods
        public bool CompareId(T id)
        {
            if (id == null)
            {
                return false;
            }
            else
            {
                return Comparer<T>.Default.Compare(_id, id) == 0 ? true : false;
            }
        }
        public T GetId()
        {
            return Id;
        }
        public string GetName()
        {
            return Name;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
