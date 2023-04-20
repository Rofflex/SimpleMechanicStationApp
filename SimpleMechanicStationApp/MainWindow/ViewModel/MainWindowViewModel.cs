using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace SimpleMechanicStationApp.MainWindow.ViewModel
{
    public class MainWindowViewModel
    {
        private static MainWindow createBaseForm()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Background = new SolidColorBrush(Colors.Beige);
            return mainWindow;
        }

        private static MainWindow addObjects(MainWindow mainWindow)
        {
            var newScrollViewer = new ScrollViewer();
            newScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            newScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            newScrollViewer.Margin = new Thickness(0, 40, 40, 40);
            newScrollViewer.Width = 350;
            newScrollViewer.Height = 500;

            /*var myResourceDictionary = new ResourceDictionary();
            myResourceDictionary.Source =
                new Uri("/SimpleMechanicStationApp;component/app.xaml",
                        UriKind.RelativeOrAbsolute);

            TextBlock newTextBlock = new TextBlock();
            newTextBlock.Style = (Style)myResourceDictionary["TextBoxDictionary"];*/

             /*System.Uri resourceLocater = new System.Uri("/SimpleMechanicStationApp;component/app.xaml", System.UriKind.Relative);*/



            /*TextBlock newTextBlock = new TextBlock();
            newTextBlock.Style = (Style)Application.Current.Resources.MergedDictionaries[2];*/
            //System.Windows.Application.LoadComponent(newTextBlock, resourceLocater);

            mainWindow.Content = newScrollViewer;
            return mainWindow;
        }

        public static MainWindow createForm()
        {
            var mainWindow = createBaseForm();
            mainWindow = addObjects(mainWindow);
            return mainWindow;
        }
    }
}
