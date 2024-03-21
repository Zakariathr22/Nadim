using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;
using Nadim.Views;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartWindow : Window
    {
        private AppWindow appWindow;
        private OverlappedPresenter overlappedPresenter;
        public static LoginWindow loginWindow;
        public StartWindow()
        {
            this.InitializeComponent();
            
            appWindow = GetAppWindowForCurrentWindow();
            appWindow.Resize(new Windows.Graphics.SizeInt32(50, 50));

            overlappedPresenter = GetAppWindowOverlappedPresenter(appWindow);

            appWindow.Title = "Nadim";
            loginWindow = new LoginWindow();
            loginWindow.Activate();

            appWindow.IsShownInSwitchers = false;           
            this.Activated += MainWindow_Activated;
        }

        private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            overlappedPresenter.Minimize();
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }

        public OverlappedPresenter GetAppWindowOverlappedPresenter(AppWindow appWindow)
        {
            return (OverlappedPresenter)appWindow.Presenter;
        }

    }
}
