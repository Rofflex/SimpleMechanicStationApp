using SimpleMechanicStationApp.GeneralMethods.DBMethods.Models;
using SimpleMechanicStationApp.GeneralVMM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleMechanicStationApp.OrderWindow.View
{
    /// <summary>
    /// Логика взаимодействия для OrderWindowView.xaml
    /// </summary>
    public partial class OrderWindowView : Window
    {
        public OrderWindowView(DbCurrentUserModel dbCurrentUser, Order order)
        {
            InitializeComponent();
            TestTextBlock.Text = order?.Summary.ToString();
        }
    }
}
