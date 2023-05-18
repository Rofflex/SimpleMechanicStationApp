using SimpleMechanicStationApp.GeneralMethods.DBMethods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleMechanicStationApp.GeneralMethods.WindowMethods.OpenCloseCommands
{
    public interface IOpenCloseCommands
    {
        /// <summary>
        /// Opening necessary window
        /// </summary>
        public void OpenWindow(Window window);
        /// <summary>
        /// Closing window
        /// </summary>
        /// <param name="window"></param>
        public void CloseWindow(Window window);
        /// <summary>
        /// Closing window with reference to open previous window
        /// </summary>
        /// <param name="window"></param>
        /// <param name="Refwindow"></param>
        public void CloseWindowOpenPrevious(Window window, Window Refwindow);
    }
}
