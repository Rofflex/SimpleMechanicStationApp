using SimpleMechanicStationApp.MainWindow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleMechanicStationApp
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       /* protected override void OnStartup(StartupEventArgs e)
        {
            int WindowNumber = 1;
            switch (WindowNumber)
            {
                case 1:
                    var logInWindow = new LogInWindowView();
                    logInWindow.Show();
                    logInWindow.IsVisibleChanged += (s, ev) =>
                    {
                        if (logInWindow.IsVisible == false && logInWindow.IsLoaded)
                        {
                            var mainWindowView = new MainWindow.MainWindow();
                            mainWindowView.Show();
                            WindowNumber = 2;
                            logInWindow.Close();
                        }
                        else if (logInWindow.IsVisible == false && !logInWindow.IsActive)
                        {
                            WindowNumber = 0;
                        }
                    };
                    break;*/
                /*case 2:
                    var mainWindowView = new MainWindow.MainWindow();
                    mainWindowView.Show();
                    break;
            }*/
            /*var mainWindowView = new MainWindow.MainWindow();
            mainWindowView.Show();*/
            /*while (Access != 0)
            {
                switch (Access)
                {
                    case 1:
                        var logInWindow = new LogInWindow();
                        Access = app.Run(logInWindow);
                        break;
                }
            }
        }*/
    }
}
