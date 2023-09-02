using System;
using System.Windows;
using System.Windows.Input;

namespace SimpleMechanicStationApp.DialogWindow.View
{
    /// <summary>
    /// Logic to interact with DialogWindowView.xaml
    /// </summary>
    public partial class DialogWindowView : Window
    {
        public DialogWindowView()
        {
            InitializeComponent();
        }
        public void DialogWindow_MouseDoubleClick(object sender, MouseEventArgs e) 
        {
            DialogWindow_Closed(sender, e);
        }
        public void DialogWindow_Closed(object sender, EventArgs e) 
        {
            if (sender.GetType().Name == "DataGrid")
            {
                DialogResult = true;
            }
            else 
            {
                DialogResult = false;
            }
        }
    }
}
