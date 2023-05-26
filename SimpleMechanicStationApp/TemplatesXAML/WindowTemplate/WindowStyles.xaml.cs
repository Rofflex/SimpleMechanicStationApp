using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace SimpleMechanicStationApp.TemplatesXAML.WindowTemplate
{
    public partial class WindowStyles : ResourceDictionary
    {
        public WindowStyles()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button closeButton && closeButton.TemplatedParent is Window window)
            {
                window.Close();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button minimizeButton && minimizeButton.TemplatedParent is Window window)
            {
                window.WindowState = WindowState.Minimized;
            }
        }
        private void DragMoveArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is FrameworkElement element)
            {
                Window window = Window.GetWindow(element);
                window.DragMove();
            }
        }
    }
}
