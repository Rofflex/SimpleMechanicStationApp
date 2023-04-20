using SimpleMechanicStationApp.MainWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleMechanicStationApp.GeneralMethods.WindowMethods.OpenCloseCommands
{
    partial class OpenCloseCommands : IOpenCloseCommands
    {
        public void CloseWindow(Window window)
        {
            window?.Close();
        }

        public void CloseWindowOpenPrevious(Window window, Window Refwindow)
        {
            window?.Close();
            Refwindow.Show();
        }

        public void OpenWindow(Window window)
        {
             window?.Show();
        }
    }
}
