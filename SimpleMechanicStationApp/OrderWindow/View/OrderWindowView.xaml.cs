using SimpleMechanicStationApp.GeneralVMM.CurrentUserM.Model;
using SimpleMechanicStationApp.GeneralVMM.OrderVMM.Model;
using SimpleMechanicStationApp.OrderWindow.ViewModel;
using System.Windows;


namespace SimpleMechanicStationApp.OrderWindow.View
{
    /// <summary>
    /// Логика взаимодействия для OrderWindowView.xaml
    /// </summary>
    public partial class OrderWindowView : Window
    {
        public OrderWindowView(CurrentUser dbCurrentUser, Order order)
        {
            InitializeComponent();
            DataContext = new OrderWindowViewModel(order);
            TestTextBlock.Text = order?.Summary.ToString();
        }
    }
}
