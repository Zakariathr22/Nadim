using Microsoft.Extensions.Configuration;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Nadim.Services;
using Nadim.Views;
using Nadim.Views.AccountRecovery;
using Nadim.Views.SystemMessages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.Security.Credentials;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static DataAccessService dataAccess;
        public static int openWindowCount;
        public static SignUpWindow signUpWindow;
        public static PasswordVault valut = new PasswordVault();
        public static AccountRecoveryWindow recoveryWindow;
        public static MainWindow mainWindow { get; set; }
        public App()
        {
            this.InitializeComponent();
            openWindowCount = 0;
            // Get the desired culture (e.g., Arabic)
            CultureInfo newCulture = CultureInfo.GetCultureInfo("ar-DZ");

            // Set the CurrentCulture of the current thread
            Thread.CurrentThread.CurrentCulture = newCulture;

            // Set the CurrentUICulture of the current thread (optional)
            // This affects UI elements like date pickers and number formats
            Thread.CurrentThread.CurrentUICulture = newCulture;

            ApplicationLanguages.PrimaryLanguageOverride = "ar-DZ";

            if (bool.Parse(ConfigurationService.GetAppSetting("IsFirstLaunch")))
            {
                valut.Add(new PasswordCredential("NadimApplication", "token", "empty"));
                ConfigurationService.SetAppSetting("IsFirstLaunch", false);
                valut.Add(new PasswordCredential("NadimApplication", "Database", CryptographyService.onifojij(ConfigurationService.GetConnectionString("Database"))));
            }

            try
            {
                dataAccess = DataAccessService.CreateInstance(valut.Retrieve("NadimApplication", "Database").Password);
                dataAccess.OpenConnection();
            }
            catch
            {

            }
            
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            s_window = new StartWindow();
            s_window.Activate();
        }

        public static Window s_window;

        public static TEnum GetEnum<TEnum>(string text) where TEnum : struct
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum)
            {
                throw new InvalidOperationException("Generic parameter 'TEnum' must be an enum.");
            }
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }

    }
}
