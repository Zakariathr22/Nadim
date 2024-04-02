using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Nadim.ViewModels;
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
    public sealed partial class VerificationPage : Page
    {
        public VerificationPage()
        {
            this.InitializeComponent();

            AccountRecoveryWindow.accountRecoveryVerificationViewModel = new AccountRecoveryVerificationViewModel();
            mainPanel.DataContext = AccountRecoveryWindow.accountRecoveryVerificationViewModel;
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            App.recoveryWindow.selectorBar.SelectedItem = App.recoveryWindow.SelectorBarItemNewPassword;
            App.recoveryWindow.SelectorBarItemVerification.IsEnabled = false;
            App.recoveryWindow.SelectorBarItemNewPassword.IsEnabled = true;
        }
    }
}
