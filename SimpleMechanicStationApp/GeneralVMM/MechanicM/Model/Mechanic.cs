namespace SimpleMechanicStationApp.GeneralVMM.MechanicM.Model
{
    public class Mechanic : BaseModel<int>
    {
        public int MechanicId
        {
            get => Id;
            set
            {
                Id = value;
                OnPropertyChanged(nameof(MechanicId));
            }
        }
        public string MechanicName
        {
            get => Name;
            set
            {
                Name = value;
                OnPropertyChanged(nameof(MechanicName));
            }
        }

    }
}
