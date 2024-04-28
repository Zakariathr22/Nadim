using Microsoft.UI;
using Microsoft.UI.Windowing;
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
using Windows.Graphics;
using Windows.Security.Credentials;
using Windows.System;
using Windows.UI;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private AppWindow appWindow;
        private OverlappedPresenter overlappedPresenter;
        private AppWindowTitleBar titleBar;
        private bool isActivatedOnce = false;
        public MainWindow()
        {
            this.InitializeComponent();

            var vault = new Windows.Security.Credentials.PasswordVault();
            var credential = vault.Retrieve("NadimApplication", "token");
            var token = credential.Password;

            appWindow = GetAppWindowForCurrentWindow();
            overlappedPresenter = GetAppWindowOverlappedPresenter(appWindow);
            titleBar = GetAppWindowTitleBar(appWindow);
            titleBar.ButtonBackgroundColor = Color.FromArgb(1, 0, 0, 0);

            titleBar.ButtonForegroundColor = Color.FromArgb(0, 128, 128, 128);

            appWindow.Resize(new Windows.Graphics.SizeInt32(1130, 480));

            //overlappedPresenter.Maximize();
            //navigationView.Height = "auto";

            titleBar.ExtendsContentIntoTitleBar = true;

            CenterWindow();

            Activated += MainWindow_Activated;
            Closed += MainWindow_Closed;

            navigationView.SelectedItem = navigationView.MenuItems.OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>().First();
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

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }

        private OverlappedPresenter GetAppWindowOverlappedPresenter(AppWindow appWindow)
        {
            return (OverlappedPresenter)appWindow.Presenter;
        }

        private AppWindowTitleBar GetAppWindowTitleBar(AppWindow appWindow)
        {
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                var titleBar = appWindow.TitleBar;

                return titleBar;
            }
            else
            {
                return null;
            }
        }

        private void CenterWindow()
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            Microsoft.UI.WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            Microsoft.UI.Windowing.AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            if (appWindow is not null)
            {
                Microsoft.UI.Windowing.DisplayArea displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(windowId, Microsoft.UI.Windowing.DisplayAreaFallback.Nearest);
                if (displayArea is not null)
                {
                    var CenteredPosition = appWindow.Position;
                    CenteredPosition.X = ((displayArea.WorkArea.Width - appWindow.Size.Width) / 2);
                    CenteredPosition.Y = ((displayArea.WorkArea.Height - appWindow.Size.Height) / 2);
                    appWindow.Move(CenteredPosition);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PasswordVault vault = new PasswordVault();
            try
            {
                vault.Add(new Windows.Security.Credentials.PasswordCredential("NadimApplication", "token", null));
            }
            catch
            {
                vault.Add(new Windows.Security.Credentials.PasswordCredential("NadimApplication", "token", "empty"));
            }
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Activate();
            this.Close();
        }

        private void navigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;
            if (selectedItem != null)
            {
                string selectedItemTag = ((string)selectedItem.Tag);
                //sender.Header = "Sample Page " + selectedItemTag.Substring(selectedItemTag.Length - 1);
                string pageName = $"Nadim.Views.{selectedItemTag}.{selectedItemTag}Page";
                Type pageType = Type.GetType(pageName);
                contentFrame.Navigate(pageType);
                Console.Write("Hi");
            }
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            //Microsoft.UI.Windowing.DisplayArea displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(windowId, Microsoft.UI.Windowing.DisplayAreaFallback.Nearest);
            //if (appWindow.Size.Width <= displayArea.WorkArea.Width / 2)
            //{
            //    navigationView.PaneDisplayMode = NavigationViewPaneDisplayMode.Auto;
            //}
        }
    }
}
