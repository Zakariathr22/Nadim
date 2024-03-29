using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlConnector;
using Nadim.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private bool isActivatedOnce = false;
        public MainWindow()
        {
            this.InitializeComponent();

            var vault = new Windows.Security.Credentials.PasswordVault();
            var credential = vault.Retrieve("MyApp", "token");
            var token = credential.Password;

            string query = "CALL GetUserInfoByToken(@token)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
               new MySqlParameter("@token", token)
            };

            MySqlDataReader reader = App.dataAccess.ExecuteQuery(query, parameters);
            Models.User user = new Models.User();

            while (reader.Read())
            {
                user.firstName = reader["firstname"] as string;
                user.lastName = reader["lastname"] as string;
            }

            textblock.Text = $"مرحبا {user.firstName} {user.lastName}";
            
            token = null;
            parameters = null;

            Activated += MainWindow_Activated;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, WindowEventArgs args)
        {
            if (isActivatedOnce == true)
            {
                isActivatedOnce = false;
                App.openWindowCount--;
            }
            if (App.openWindowCount <= 0)
            {
                App.s_window.Close();
            }
        }

        private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            if (isActivatedOnce == false)
            {
                isActivatedOnce = true;
                App.openWindowCount++;
            }
        }
    }
}
