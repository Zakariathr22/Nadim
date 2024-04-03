using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Nadim.Services;
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
            AccountRecoveryWindow.accountRecoveryFindAccountViewModel = new ViewModels.AccountRecoveryFindAccountViewModel();
            mainPanel.DataContext = AccountRecoveryWindow.accountRecoveryFindAccountViewModel;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AccountRecoveryWindow.accountRecoveryFindAccountViewModel.ValidateCommand.Execute(this);
                if (AccountRecoveryWindow.accountRecoveryFindAccountViewModel.EverythingIsValid)
                {
                    App.recoveryWindow.selectorBar.SelectedItem = App.recoveryWindow.SelectorBarItemVerification;
                    App.recoveryWindow.SelectorBarItemFindAccount.IsEnabled = false;
                    App.recoveryWindow.SelectorBarItemVerification.IsEnabled = true;
                }
            }
            catch
            {
                ShowDialog();
            }
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Email = emailTextBox.Text;
            if (Email.TrimStart() != "" && !DataValidationService.IsValidEmail(Email.TrimStart().TrimStart()))
            {
                emailTextBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;

                if (!DataValidationService.IsValidEmail(Email.TrimStart().TrimStart()) && Email.TrimStart() != "")
                {
                    emailIsNotValidError.Visibility = Visibility.Visible;
                }
                else
                {
                    emailIsNotValidError.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                emailTextBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                emailIsNotValidError.Visibility = Visibility.Collapsed;
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
                    AccountRecoveryWindow.accountRecoveryFindAccountViewModel.ValidateCommand.Execute(this);
                    if (AccountRecoveryWindow.accountRecoveryFindAccountViewModel.EverythingIsValid)
                    {
                        App.recoveryWindow.selectorBar.SelectedItem = App.recoveryWindow.SelectorBarItemVerification;
                        App.recoveryWindow.SelectorBarItemFindAccount.IsEnabled = false;
                        App.recoveryWindow.SelectorBarItemVerification.IsEnabled = true;
                    }
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
