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
using Windows.UI;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.SystemMessages
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomMessageBox : Window
    {
        private AppWindow appWindow;
        private OverlappedPresenter overlappedPresenter;
        public static LoginWindow loginWindow;
        private AppWindowTitleBar titleBar;
        public CustomMessageBox()
        {
            this.InitializeComponent();

            appWindow = GetAppWindowForCurrentWindow();
            appWindow.Resize(new Windows.Graphics.SizeInt32(400, 220));

            overlappedPresenter = GetAppWindowOverlappedPresenter(appWindow);

            titleBar = GetAppWindowTitleBar(appWindow);
            titleBar.ButtonBackgroundColor = Color.FromArgb(1, 0, 0, 0);

            titleBar.ButtonForegroundColor = Color.FromArgb(0, 128, 128, 128);

            overlappedPresenter.IsResizable = false;
            overlappedPresenter.IsMinimizable = false;
            overlappedPresenter.IsMaximizable = false;

            titleBar.ExtendsContentIntoTitleBar = true;

            CenterWindow();

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
    }
}
