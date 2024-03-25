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
using Nadim.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.SignUp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EmailVerificationPage : Page
    {
        private int emailOTP;
        public EmailVerificationPage()
        {
            this.InitializeComponent();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(verificationCodeTextBox.Text.TrimEnd().TrimStart()) == emailOTP)
            {
                App.signUpWindow.selectorBar.SelectedItem = App.signUpWindow.SelectorBarItemphoneVer;
                App.signUpWindow.SelectorBarItemphoneVer.IsEnabled = true;
                App.signUpWindow.SelectorBarItemEmailVer.IsEnabled = false;
            }
        }

        private void mainPanel_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                emailOTP = EmailVerificationService.GenerateRandomOTP();
                EmailVerificationService.SendAccountCreationEmailOTP("zakotahri@outlook.com", emailOTP);
            }
            catch
            {
                ShowDialog();
            }
        }

        private async void ShowDialog()
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new Views.SystemMessages.ConnectionFailedTitleControl();
            dialog.PrimaryButtonText = "حاول مرة أخرى";
            dialog.CloseButtonText = "إغلاق البرنامج";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new Views.SystemMessages.ConnectionFailedPage();
            dialog.FlowDirection = FlowDirection.RightToLeft;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    emailOTP = EmailVerificationService.GenerateRandomOTP();
                    EmailVerificationService.SendAccountCreationEmailOTP("zakotahri@outlook.com", emailOTP);
                }
                catch
                {
                    ShowDialog();
                }
            }
            else
            {
                App.signUpWindow.Close();
            }
        }
    }
}
