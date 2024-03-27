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
        public DispatcherTimer timer;
        public TimeSpan timeRemaining;
        public EmailVerificationPage()
        {
            this.InitializeComponent();
            SignUpWindow.signUpEmailVerViewModel = new ViewModels.SignUpEmailVerViewModel();
            mainPanel.DataContext = SignUpWindow.signUpEmailVerViewModel;

            // Create a new DispatcherTimer instance
            timer = new DispatcherTimer();

            // Set the interval for the timer
            timer.Interval = TimeSpan.FromSeconds(1);

            // Set the initial time (2 minutes in this case)
            timeRemaining = TimeSpan.FromMinutes(2);

            timer.Tick += Timer_Tick;

            timer.Start();
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
            if (SignUpWindow.signUpEmailVerViewModel.EmailCodeIsValid)
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
            sendAgainHyperlinkButton.IsEnabled = false;
            SignUpWindow.signUpEmailVerViewModel.GenerateSendOTPCommand.Execute(this);
            // Set the interval for the timer
            timer.Interval = TimeSpan.FromSeconds(1);

            // Set the initial time (2 minutes in this case)
            timeRemaining = TimeSpan.FromMinutes(2);

            timer.Tick += Timer_Tick;

            timer.Start();
        }
    }
}
