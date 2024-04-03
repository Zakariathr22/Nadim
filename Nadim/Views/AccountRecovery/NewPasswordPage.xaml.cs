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
    public sealed partial class NewPasswordPage : Page
    {
        public NewPasswordPage()
        {
            this.InitializeComponent();
            AccountRecoveryWindow.accountRecoveryNewPasswordViewModel = new ViewModels.AccountRecoveryNewPasswordViewModel();
            mainPanel.DataContext = AccountRecoveryWindow.accountRecoveryNewPasswordViewModel;
        }

        private void resetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            AccountRecoveryWindow.accountRecoveryNewPasswordViewModel.ValdateCommand.Execute(this);
            
            if (AccountRecoveryWindow.accountRecoveryNewPasswordViewModel.EveryThingValid)
                AccountRecoveryWindow.accountRecoveryViewModel.ResetPasswordCommand.Execute(null);
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string Password = passwordBox.Password;
            if (Password == ""
            || !DataValidationService.HasMinimumCharacters(Password, 8)
            || !DataValidationService.HasMaximumCharacters(Password, 128)
            || !Password.Any(char.IsLetter)
            || !DataValidationService.ContainsSymbol(Password)
            || !DataValidationService.ContainsNumber(Password))
            {
                passwordBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (Password == "")
                {
                    passwordRequiredError.Visibility = Visibility.Visible;
                }
                else
                {
                    passwordRequiredError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(Password, 8) && Password != "")
                {
                    passwordTooShortError.Visibility = Visibility.Visible;
                }
                else
                {
                    passwordTooShortError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMaximumCharacters(Password, 128))
                {
                    passwordTooLongError.Visibility = Visibility.Visible;
                }
                else
                {
                    passwordTooLongError.Visibility = Visibility.Collapsed;
                }

                if (!Password.Any(char.IsLetter) && Password != "")
                {
                    passwordMustContainAlpabetError.Visibility = Visibility.Visible;
                }
                else
                {
                    passwordMustContainAlpabetError.Visibility = Visibility.Collapsed;
                }
                if (!DataValidationService.ContainsSymbol(Password) && Password != "")
                {
                    passwordMustContainSymbolError.Visibility = Visibility.Visible;
                }
                else
                {
                    passwordMustContainSymbolError.Visibility = Visibility.Collapsed;
                }
                if (!DataValidationService.ContainsNumber(Password) && Password != "")
                {
                    passwordMustContainNumberError.Visibility = Visibility.Visible;
                }
                else
                {
                    passwordMustContainNumberError.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                passwordBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                passwordRequiredError.Visibility = Visibility.Collapsed;
                passwordTooShortError.Visibility = Visibility.Collapsed;
                passwordTooLongError.Visibility = Visibility.Collapsed;
                passwordMustContainAlpabetError.Visibility = Visibility.Collapsed;
                passwordMustContainSymbolError.Visibility = Visibility.Collapsed;
                passwordMustContainNumberError.Visibility = Visibility.Collapsed;
            }
            if (confirmPasswordBox.Password != passwordBox.Password && confirmPasswordBox.Password != "")
            {
                confirmPasswordBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                confirmPasswoedMatchError.Visibility = Visibility.Visible;
            }
            else
            {
                confirmPasswordBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                confirmPasswoedMatchError.Visibility = Visibility.Collapsed;
            }
        }

        private void confirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (confirmPasswordBox.Password == "" || confirmPasswordBox.Password != passwordBox.Password)
            {
                confirmPasswordBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (confirmPasswordBox.Password == "")
                {
                    confirmPasswoedRequiredError.Visibility = Visibility.Visible;
                }
                else
                {
                    confirmPasswoedRequiredError.Visibility = Visibility.Collapsed;
                }
                if (confirmPasswordBox.Password != passwordBox.Password && confirmPasswordBox.Password != "")
                {
                    confirmPasswoedMatchError.Visibility = Visibility.Visible;
                }
                else
                {
                    confirmPasswoedMatchError.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                confirmPasswordBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                confirmPasswoedRequiredError.Visibility = Visibility.Collapsed;
                confirmPasswoedMatchError.Visibility = Visibility.Collapsed;
            }
        }
    }
}
