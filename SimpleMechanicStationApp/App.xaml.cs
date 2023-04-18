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
        [STAThread]
        public static void Main()
        {
            int Access = 1;
            App app = new App();
            app.InitializeComponent();
            while (Access != 0)
            {
                switch (Access)
                {
                    case 1:
                        var logInWindow = new LogInWindow();
                        Access = app.Run(logInWindow);
                        break;
                }
            }
        }
    }
}
