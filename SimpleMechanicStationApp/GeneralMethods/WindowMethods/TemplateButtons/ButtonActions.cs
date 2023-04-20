using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace SimpleMechanicStationApp.GeneralMethods.CreateForm.Buttons
{
    public class ButtonActions
    {
        Window _Window;
        public ButtonActions(Window p_Window)
        {
            _Window = p_Window;
        }

        public void Exit_Click(object sender, EventArgs e)
        {
            _Window.Close();
        }
        public void Collapse_Click(object sender, EventArgs e)
        {
            _Window.WindowState = WindowState.Minimized;
        }
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _Window.DragMove();
        }

    }
}
