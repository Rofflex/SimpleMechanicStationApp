using SimpleMechanicStationApp.OrderWindow.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleMechanicStationApp.OrderWindow.View
{
    /// <summary>
    /// Logic to interact with OrderWindowView.xaml
    /// </summary>
    public partial class OrderWindowView : Window
    {
        public OrderWindowView()
        {
            InitializeComponent();
        }
        private void UpdateAmount(object sender, EventArgs e)
        {
            var dataGrid = (DataGrid)sender;
            var viewModel = (OrderWindowViewModel)dataGrid?.DataContext;

            var cellInfo = dataGrid?.CurrentCell;

            if (cellInfo != null && cellInfo.Value.Column != null)
            {
                if (viewModel.DefinateColumnsChanged.CanExecute(null))
                {
                    viewModel.DefinateColumnsChanged.Execute(null);
                }
            }
        }
        private void DataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UpdateAmount(sender, e);
            }
        }
        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            UpdateAmount(sender, e);
        }

        private void TextBox_MouseDoubleClick_ChooseCar(object sender, MouseEventArgs e) 
        {
            if (DataContext is OrderWindowViewModel viewModel)
            {
                if (viewModel.ChooseCar.CanExecute(null))
                {
                    viewModel.ChooseCar.Execute(null);
                }
            }
        }
        private void TextBox_MouseDoubleClick_ChooseCustomer(object sender, MouseEventArgs e)
        {
            if (DataContext is OrderWindowViewModel viewModel)
            {
                if (viewModel.ChooseCustomer.CanExecute(null))
                {
                    viewModel.ChooseCustomer.Execute(null);
                }
            }
        }
    }
}
