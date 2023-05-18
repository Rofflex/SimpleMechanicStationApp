using SimpleMechanicStationApp.GeneralMethods.DBMethods.Models;
using SimpleMechanicStationApp.MainWindow.ViewModel;
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

namespace SimpleMechanicStationApp.MainWindow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            GeneralMethods.CreateForm.Buttons.ButtonActions buttonActions = new(this);
            Exit.Click += buttonActions.Exit_Click;
            Collapse.Click += buttonActions.Collapse_Click;
            MouseDown += buttonActions.Window_MouseDown;
        }
    }
}
