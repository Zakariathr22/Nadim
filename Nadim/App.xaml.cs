﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Nadim.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static int openWindowCount;
        public App()
        {
            this.InitializeComponent();
            openWindowCount = 0;
            // Get the desired culture (e.g., French)
            CultureInfo newCulture = CultureInfo.GetCultureInfo("ar-DZ");

            // Set the CurrentCulture of the current thread
            Thread.CurrentThread.CurrentCulture = newCulture;

            // Set the CurrentUICulture of the current thread (optional)
            // This affects UI elements like date pickers and number formats
            Thread.CurrentThread.CurrentUICulture = newCulture;

            ApplicationLanguages.PrimaryLanguageOverride = "ar-DZ";
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
    }
}
