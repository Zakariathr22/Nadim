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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.AccountRecovery
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FindAccountPage : Page
    {
        public FindAccountPage()
        {
            this.InitializeComponent();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            App.recoveryWindow.selectorBar.SelectedItem = App.recoveryWindow.SelectorBarItemVerification;
            App.recoveryWindow.SelectorBarItemFindAccount.IsEnabled = false;
            App.recoveryWindow.SelectorBarItemVerification.IsEnabled = true;
        }
    }
}
