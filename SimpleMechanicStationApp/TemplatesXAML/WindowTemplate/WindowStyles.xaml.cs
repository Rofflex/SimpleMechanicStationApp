using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
