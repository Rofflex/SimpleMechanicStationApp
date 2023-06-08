using SimpleMechanicStationApp.GeneralMethods.DBMethods.Commands;

namespace SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model
{
    public class CurrentUser
    {
        private static readonly CurrentUser instance = new CurrentUser();

        private CurrentUser()
        {

        }

        public static CurrentUser Instance 
        {
            get { return instance; }
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
