using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using WinRT.Interop;
using Windows.UI;
using Microsoft.UI.Xaml.Media.Animation;
using Nadim.Views.SignUp;
using System.Runtime.CompilerServices;
using Nadim.ViewModels;
using Nadim.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpWindow : Window
    {
        private AppWindow appWindow;
        private OverlappedPresenter overlappedPresenter;
        private AppWindowTitleBar titleBar;
        private int previousSelectedIndex;
        private bool isActivatedOnce = false;
        public static SignUpLawyerInfoViewModel signUpLawyerInfoViewModel;
        public static SignUpOfficeInfoViewModel signUpOfficeInfoViewModel;
        public SignUpWindow()
        {
            this.InitializeComponent();

            appWindow = GetAppWindowForCurrentWindow();
            overlappedPresenter = GetAppWindowOverlappedPresenter(appWindow);
            titleBar = GetAppWindowTitleBar(appWindow);
            titleBar.ButtonBackgroundColor = Color.FromArgb(1, 0, 0, 0);

            titleBar.ButtonForegroundColor = Color.FromArgb(0, 128, 128, 128);


            appWindow.Resize(new Windows.Graphics.SizeInt32(500, 600));

            overlappedPresenter.IsResizable = false;
            overlappedPresenter.IsMinimizable = false;
            overlappedPresenter.IsMaximizable = false;

            titleBar.ExtendsContentIntoTitleBar = true;

            CenterWindow();

            Activated += SignUpWindow_Activated;
            Closed += SignUpWindow_Closed;

            signUpLawyerInfoViewModel = new SignUpLawyerInfoViewModel();
            
    }

        private void SignUpWindow_Closed(object sender, WindowEventArgs args)
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
            DataValidationService.existedEmails.Clear();
            DataValidationService.existedPhones.Clear();
        }

        private void SignUpWindow_Activated(object sender, WindowActivatedEventArgs args)
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

        private void selectorBar_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
        {
                SelectorBarItem selectedItem = sender.SelectedItem;
                int currentSelectedIndex = sender.Items.IndexOf(selectedItem);
                System.Type pageType;

                switch (currentSelectedIndex)
                {
                    case 0:
                        pageType = typeof(LawyerInfoPage);
                        break;
                    case 1:
                        pageType = typeof(OfficeInfoPage);
                        break;
                    case 2:
                        pageType = typeof(EmailVerificationPage);
                        break;
                    case 3:
                        pageType = typeof(PhoneVerificationPage);
                        break;
                default:
                        pageType = typeof(Page);
                        break;
                }

                var slideNavigationTransitionEffect = currentSelectedIndex - previousSelectedIndex > 0 ? SlideNavigationTransitionEffect.FromRight : SlideNavigationTransitionEffect.FromLeft;

                ContentFrame.Navigate(pageType, new SlideNavigationTransitionInfo() { Effect = slideNavigationTransitionEffect });
                
                previousSelectedIndex = currentSelectedIndex;
            
        }

        private void haveAccountAlready_Click(object sender, RoutedEventArgs e)
        {

            LoginWindow login = new LoginWindow();
            login.Activate();
            this.Close();
        }
    }
}
