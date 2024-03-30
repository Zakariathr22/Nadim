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
using Nadim.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.SignUp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhoneVerificationPage : Page
    {
        public PhoneVerificationPage()
        {
            this.InitializeComponent();
            SignUpWindow.signUpPhoneVerViewModel = new SignUpPhoneVerViewModel();

            mainPanel.DataContext = SignUpWindow.signUpPhoneVerViewModel;
        }

        private void skipAndSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow.signUpViewModel = new SignUpViewModel();
            try
            {
                SignUpWindow.signUpViewModel.LawyerSignUpCommand.Execute(this);
                skipAndSignUp.IsEnabled = false;
                ShowSuccessDialog();
            }
            catch 
            {
                ShowErrorDialog();
            }

        }

        private async void ShowErrorDialog()
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
                    SignUpWindow.signUpViewModel.LawyerSignUpCommand.Execute(this);
                }
                catch
                {
                    ShowErrorDialog();
                }
            }
            else
            {
                App.signUpWindow.Close();
            }
        }

        private async void ShowSuccessDialog()
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new Views.SystemMessages.SignUpSuccessTitleControl();
            dialog.PrimaryButtonText = "تسجيل الدخول";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new Views.SystemMessages.SignUpSuccessPage();
            dialog.FlowDirection = FlowDirection.RightToLeft;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                LoginWindow login = new LoginWindow();
                login.Activate();
                App.signUpWindow.Close();
            }
            else
            {
                App.signUpWindow.Close();
            }
        }
    }
}
