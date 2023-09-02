using SimpleMechanicStationApp.CarWindow.ViewModel;
using SimpleMechanicStationApp.OrderWindow.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleMechanicStationApp.CarWindow.View
{
    /// <summary>
    /// Logic to interact with CarPartView.xaml
    /// </summary>
    public partial class CarWindowView : Window
    {
        public CarWindowView()
        {
            InitializeComponent();
        }
        /*public void CarWindow_Closed(object sender, EventArgs e)
        {
            var viewModel = (CarWindowViewModel)DataContext;
            if (viewModel.IsNew)
            {
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }*/
    }
}
