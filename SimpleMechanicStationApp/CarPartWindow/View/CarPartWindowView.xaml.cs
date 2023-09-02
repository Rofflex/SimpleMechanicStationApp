using SimpleMechanicStationApp.CarPartWindow.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleMechanicStationApp.CarPartWindow.View
{
    /// <summary>
    /// Логика взаимодействия для CarPartWindowView.xaml
    /// </summary>
    public partial class CarPartWindowView : Window
    {
        public CarPartWindowView()
        {
            InitializeComponent();
        }
        private void TextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (DataContext is CarPartWindowViewModel viewModel)
            {
                if (viewModel.ChooseManufacture.CanExecute(null))
                {
                    viewModel.ChooseManufacture.Execute(null);
                }
            }
        }
    }
}
