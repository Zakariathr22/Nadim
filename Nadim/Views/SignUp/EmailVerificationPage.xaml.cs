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
using System.Text.RegularExpressions;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.SignUp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EmailVerificationPage : Page
    {
        public DispatcherTimer timer;
        public DispatcherTimer timer2;
        public TimeSpan timeRemaining;
        public TimeSpan timeRemaining2;
        int resendingTimes = 0;
        public EmailVerificationPage()
        {
            this.InitializeComponent();
            SignUpWindow.signUpEmailVerViewModel = new ViewModels.SignUp.SignUpEmailVerViewModel();
            mainPanel.DataContext = SignUpWindow.signUpEmailVerViewModel;

            // Create a new DispatcherTimer instance
            timer = new DispatcherTimer();
            timer2 = new DispatcherTimer();

            // Set the interval for the timer
            timer.Interval = TimeSpan.FromSeconds(1);
            timer2.Interval = TimeSpan.FromSeconds(1);

            // Set the initial time (1 minute in this case)
            timeRemaining = TimeSpan.FromMinutes(1);

            timer.Tick += Timer_Tick;
            timer2.Tick += Timer2_Tick;

            timer.Start();
        }

        private void Timer2_Tick(object sender, object e)
        {
            // Subtract one second from the remaining time
            timeRemaining2 = timeRemaining2.Add(-timer.Interval);

            // Update a TextBlock with the remaining time
            manyFailedAttempsError.Text = SignUpWindow.signUpEmailVerViewModel.failedAttempts + " محاولات فاشلة إعادة محاولة خلال " + timeRemaining2.ToString(@"mm\:ss");

            // If the time has run out, stop the timer
            if (timeRemaining2 <= TimeSpan.Zero)
            {
                timer2.Stop();
                manyFailedAttempsError.Visibility = Visibility.Collapsed;
                verificationCodeIsNotValid.Visibility = Visibility.Visible;
                verificationCodeIsRequiredError.Visibility = Visibility.Collapsed;
                sendAgainHyperlinkButton.Visibility = Visibility.Visible;
                verificationCodeTextBox.IsEnabled = true;
                nextButton.IsEnabled = true;
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            // Subtract one second from the remaining time
            timeRemaining = timeRemaining.Add(-timer.Interval);

            // Update a TextBlock with the remaining time
            sendAgainHyperlinkButton.Content = "لم تتلقى البريد الإلكتروني؟ إعادة إرسال خلال " + timeRemaining.ToString(@"mm\:ss");

            // If the time has run out, stop the timer
            if (timeRemaining <= TimeSpan.Zero)
            {
                timer.Stop();
                sendAgainHyperlinkButton.Content = "لم تتلقى البريد الإلكتروني؟ إعادة إرسال";
                sendAgainHyperlinkButton.IsEnabled = true;
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow.signUpEmailVerViewModel.verifyOTPCommand.Execute(true);
            if (SignUpWindow.signUpEmailVerViewModel.failedAttempts % 7 == 0)
            {
                manyFailedAttempsError.Visibility = Visibility.Visible;
                verificationCodeIsNotValid.Visibility = Visibility.Collapsed;
                verificationCodeIsRequiredError.Visibility = Visibility.Collapsed;
                sendAgainHyperlinkButton.Visibility = Visibility.Collapsed;
                verificationCodeTextBox.IsEnabled = false;
                timeRemaining2 = TimeSpan.FromMinutes((SignUpWindow.signUpEmailVerViewModel.failedAttempts / 7)*3);
                nextButton.IsEnabled = false;
                timer2.Start();
            }
            if (SignUpWindow.signUpEmailVerViewModel.EmailCodeIsValid)
            {
                //App.signUpWindow.selectorBar.SelectedItem = App.signUpWindow.SelectorBarItemphoneVer;
                //App.signUpWindow.SelectorBarItemphoneVer.IsEnabled = true;
                //App.signUpWindow.SelectorBarItemEmailVer.IsEnabled = false;
            }
        }

        private void mainPanel_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SignUpWindow.signUpEmailVerViewModel.GenerateSendOTPCommand.Execute(this);
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
            dialog.CloseButtonText = "تخطي";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new Views.SystemMessages.ConnectionFailedPage();
            dialog.FlowDirection = FlowDirection.RightToLeft;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    SignUpWindow.signUpEmailVerViewModel.GenerateSendOTPCommand.Execute(this);
                }
                catch
                {
                    ShowDialog();
                }
            }
            else
            {
                App.signUpWindow.selectorBar.SelectedItem = App.signUpWindow.SelectorBarItemphoneVer;
                App.signUpWindow.SelectorBarItemphoneVer.IsEnabled = true;
                App.signUpWindow.SelectorBarItemEmailVer.IsEnabled = false;
            }
        }

        private void sendAgainHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            resendingTimes++;
            sendAgainHyperlinkButton.IsEnabled = false;
            SignUpWindow.signUpEmailVerViewModel.GenerateSendOTPCommand.Execute(this);
            // Set the interval for the timer
            timer.Interval = TimeSpan.FromSeconds(1);

            // Set the initial time (2 minutes in this case)
            timeRemaining = TimeSpan.FromMinutes(resendingTimes + 1);

            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void verificationCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int currentPosition = verificationCodeTextBox.SelectionStart - 1;
            string text = ((TextBox)sender).Text;

            Regex regex = new Regex("^[0-9]*$");

            if (!regex.IsMatch(text))
            {
                var foundChar = Regex.Match(verificationCodeTextBox.Text, @"[^0-9]");
                if (foundChar.Success)
                {
                    verificationCodeTextBox.Text = verificationCodeTextBox.Text.Remove(foundChar.Index, 1);
                }

                verificationCodeTextBox.Select(currentPosition, 0);
            }
        }
    }
}
