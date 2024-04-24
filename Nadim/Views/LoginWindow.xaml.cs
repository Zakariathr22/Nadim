using Microsoft.UI;
using Microsoft.UI.System;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Nadim.ViewModels;
using Nadim.Views.AccountRecovery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.UI;
using Windows.UI.Core;
using WinRT.Interop;


namespace Nadim.Views
{
    public sealed partial class LoginWindow : Window
    {
        private AppWindow appWindow;
        private OverlappedPresenter overlappedPresenter;
        private AppWindowTitleBar titleBar;
        private bool isActivatedOnce = false;
        private LoginViewModel loginViewModel;
        public LoginWindow()
        {
            this.InitializeComponent();

            appWindow = GetAppWindowForCurrentWindow();
            overlappedPresenter = GetAppWindowOverlappedPresenter(appWindow);
            titleBar = GetAppWindowTitleBar(appWindow);
            titleBar.ButtonBackgroundColor = Color.FromArgb(1, 0, 0, 0);
            
            titleBar.ButtonForegroundColor = Color.FromArgb(0, 128, 128, 128);
            

            appWindow.Resize(new Windows.Graphics.SizeInt32(840, 500));

            overlappedPresenter.IsResizable = false;
            overlappedPresenter.IsMinimizable = false;
            overlappedPresenter.IsMaximizable = false;

            titleBar.ExtendsContentIntoTitleBar = true;

            CenterWindow();

            Activated += LoginWindow_Activated;
            Closed += LoginWindow_Closed;

            MainGrid.Loaded += MainGrid_Loaded;

            loginViewModel = new LoginViewModel();
            MainGrid.DataContext = loginViewModel;
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.dataAccess.ConnectionStatIsOpened() || App.dataAccess.ConnectionStatIsOpened==null)
            {
                ShowDialog();
                emailOrPhoneTextBox.IsEnabled = false;
                passwordBox.IsEnabled = false;
                forgetPasswordlinkButton.IsEnabled = false;
                signUpButton.IsEnabled = false;
            }
        }

        private void LoginWindow_Activated(object sender, Microsoft.UI.Xaml.WindowActivatedEventArgs args)
        {
            if (isActivatedOnce == false)
            {
                isActivatedOnce = true;
                App.openWindowCount++;
            }
        }

        private void LoginWindow_Closed(object sender, WindowEventArgs args)
        {
            if (isActivatedOnce == true)
            {
                isActivatedOnce = false;
                App.openWindowCount--;
            }
            if (App.openWindowCount <= 0) {
                App.s_window.Close();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.signUpWindow = new SignUpWindow();
            App.signUpWindow.Activate();
            this.Close();
        }

        private async void ShowDialog()
        {
            
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new SystemMessages.ConnectionFailedTitleControl();
            dialog.PrimaryButtonText = "حاول مرة أخرى";
            dialog.CloseButtonText = "إغلاق البرنامج";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new SystemMessages.ConnectionFailedPage();
            dialog.FlowDirection = FlowDirection.RightToLeft;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    App.dataAccess.OpenConnection();
                    emailOrPhoneTextBox.IsEnabled = true;
                    passwordBox.IsEnabled = true;
                    forgetPasswordlinkButton.IsEnabled = true;
                    signUpButton.IsEnabled = true;
                }
                catch
                {
                    ShowDialog();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                loginViewModel.LoginCommand.Execute(this);
            }
            catch
            {
                ShowLoginDialog();
            }
            if (loginViewModel.LoginIsCorrect)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Activate();
                this.Close();
            }
        }

        private async void ShowLoginDialog()
        {

            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new SystemMessages.ConnectionFailedTitleControl();
            dialog.PrimaryButtonText = "حاول مرة أخرى";
            dialog.CloseButtonText = "إغلاق البرنامج";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new SystemMessages.ConnectionFailedPage();
            dialog.FlowDirection = FlowDirection.RightToLeft;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    try
                    {
                        loginViewModel.LoginCommand.Execute(this);
                    }
                    catch
                    {
                        ShowLoginDialog();
                    }
                    if (loginViewModel.LoginIsCorrect)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Activate();
                        this.Close();
                    }
                }
                catch
                {
                    ShowLoginDialog();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void forgetPasswordlinkButton_Click(object sender, RoutedEventArgs e)
        {
            App.recoveryWindow = new AccountRecoveryWindow();
            App.recoveryWindow.Activate();
            this.Close();
        }
    }
}
