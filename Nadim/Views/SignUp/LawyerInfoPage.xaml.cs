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
using Nadim.Views;
using Windows.UI.ViewManagement;
using Nadim.ViewModels;
using Nadim.Services;
using Windows.Networking;
using System.Text.RegularExpressions;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim
{
    
    public sealed partial class LawyerInfoPage : Page
    {
        public LawyerInfoPage()
        {
            this.InitializeComponent();
            mainPanel.DataContext = SignUpWindow.signUpLawyerInfoViewModel;
            birthDateDatePicker.MaxYear = DateTimeOffset.Now.AddYears(-15);
            startingDateDatePicker.MaxYear = DateTimeOffset.Now;
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateData();
        }

        private void maleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SignUpWindow.signUpLawyerInfoViewModel.Gender = "ذكر";
            genderRequiredError.Visibility = Visibility.Collapsed;
        }

        private void femaleRadiobutton_Checked(object sender, RoutedEventArgs e)
        {
            SignUpWindow.signUpLawyerInfoViewModel.Gender = "أنثى";
            genderRequiredError.Visibility = Visibility.Collapsed;
        }

        private void birthDateDatePicker_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {

        }

        private void lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string LastName = lastNameTextBox.Text;
            if (LastName.TrimStart() == ""
                || !DataValidationService.HasMaximumCharacters(LastName.TrimStart().TrimEnd(), 49)
                || !DataValidationService.HasMinimumCharacters(LastName.TrimStart().TrimEnd(), 2)
                || !DataValidationService.IsArabic(LastName.TrimStart().TrimEnd())
                || DataValidationService.ContainsNumber(LastName.TrimStart().TrimEnd()))
            {
                lastNameTextBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (LastName.TrimStart() == "")
                {
                    LastName = "";
                    lastNameRequiredError.Visibility = Visibility.Visible;
                }
                else
                {
                    lastNameRequiredError.Visibility = Visibility.Collapsed;
                }
                if (!DataValidationService.HasMaximumCharacters(LastName.TrimStart().TrimEnd(), 49))
                {
                    lastNameTooLongError.Visibility = Visibility.Visible;
                }
                else
                {
                    lastNameTooLongError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(LastName.TrimStart().TrimEnd(), 2)
                    && LastName.TrimStart() != "")
                {
                    lastNameTooShortError.Visibility = Visibility.Visible;
                }
                else
                {
                    lastNameTooShortError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.IsArabic(LastName.TrimStart().TrimEnd())
                    && LastName.TrimStart() != "")
                {
                    lastNameIsNotArabicError.Visibility = Visibility.Visible;
                }
                else
                {
                    lastNameIsNotArabicError.Visibility = Visibility.Collapsed;
                }

                if (DataValidationService.ContainsNumber(LastName.TrimStart().TrimEnd())
                    && LastName.TrimStart() != "")
                {
                    lastNameContainsNumberError.Visibility = Visibility.Visible;
                }
                else
                {
                    lastNameContainsNumberError.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                lastNameTextBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                lastNameRequiredError.Visibility = Visibility.Collapsed;
                lastNameTooLongError.Visibility = Visibility.Collapsed;
                lastNameTooShortError.Visibility = Visibility.Collapsed;
                lastNameIsNotArabicError.Visibility = Visibility.Collapsed;
                lastNameContainsNumberError.Visibility = Visibility.Collapsed;
            }
        }

        private void firstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string FirstName = firstNameTextBox.Text;
            if (FirstName.TrimStart() == ""
                || !DataValidationService.HasMaximumCharacters(FirstName.TrimStart().TrimEnd(), 49)
                || !DataValidationService.HasMinimumCharacters(FirstName.TrimStart().TrimEnd(), 2)
                || !DataValidationService.IsArabic(FirstName.TrimStart().TrimEnd())
                || DataValidationService.ContainsNumber(FirstName.TrimStart().TrimEnd()))
            {
                firstNameTextBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (FirstName.TrimStart() == "")
                {
                    FirstName = "";
                    firstNameRequiredError.Visibility = Visibility.Visible;
                }
                else
                {
                    firstNameRequiredError.Visibility = Visibility.Collapsed;
                }
                if (!DataValidationService.HasMaximumCharacters(FirstName.TrimStart().TrimEnd(), 49))
                {
                    firstNameTooLongError.Visibility = Visibility.Visible;
                }
                else
                {
                    firstNameTooLongError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(FirstName.TrimStart().TrimEnd(), 2)
                    && FirstName.TrimStart() != "")
                {
                    firstNameTooShortError.Visibility = Visibility.Visible;
                }
                else
                {
                    firstNameTooShortError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.IsArabic(FirstName.TrimStart().TrimEnd())
                    && FirstName.TrimStart() != "")
                {
                    firstNameIsNotArabicError.Visibility = Visibility.Visible;
                }
                else
                {
                    firstNameIsNotArabicError.Visibility = Visibility.Collapsed;
                }

                if (DataValidationService.ContainsNumber(FirstName.TrimStart().TrimEnd())
                    && FirstName.TrimStart() != "")
                {
                    firstNameContainsNumberError.Visibility = Visibility.Visible;
                }
                else
                {
                    firstNameContainsNumberError.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                firstNameTextBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                firstNameRequiredError.Visibility = Visibility.Collapsed;
                firstNameTooLongError.Visibility = Visibility.Collapsed;
                firstNameTooShortError.Visibility = Visibility.Collapsed;
                firstNameIsNotArabicError.Visibility = Visibility.Collapsed;
                firstNameContainsNumberError.Visibility = Visibility.Collapsed;
            }
        }

        private void accreditationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            accreditationRequiredError.Visibility = Visibility.Collapsed;
            accreditationComboBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        }

        private void startingDateDatePicker_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            DateTimeOffset StartingDate = startingDateDatePicker.Date;
            if (StartingDate > DateTimeOffset.Now)
            {
                startingDateDatePicker.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                startingDateError.Visibility = Visibility.Visible;
            }
            else
            {
                startingDateDatePicker.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                startingDateError.Visibility = Visibility.Collapsed;
            }
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Email = emailTextBox.Text;
            if (Email.TrimStart() == ""
                || !DataValidationService.IsValidEmail(Email.TrimStart().TrimStart()))
            {
                emailTextBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (Email.TrimStart() == "")
                {
                    Email = "";
                    emailRequiredError.Visibility = Visibility.Visible;
                }
                else
                {
                    Email = Email.TrimStart().TrimEnd();
                    emailRequiredError.Visibility = Visibility.Collapsed;
                }
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
                emailRequiredError.Visibility = Visibility.Collapsed;
                emailIsNotValidError.Visibility = Visibility.Collapsed;
                //emailAllreadyExistsError.Visibility = Visibility.Collapsed;
            }
        }

        private void phoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Phone = phoneTextBox.Text;
            int currentPosition = phoneTextBox.SelectionStart - 1;
            string text = ((TextBox)sender).Text;

            Regex regex = new Regex("^[0-9]*$");

            if (!regex.IsMatch(text))
            {
                var foundChar = Regex.Match(phoneTextBox.Text, @"[^0-9]");
                if (foundChar.Success)
                {
                    phoneTextBox.Text = phoneTextBox.Text.Remove(foundChar.Index, 1);
                }

                phoneTextBox.Select(currentPosition, 0);
            }

            if (Phone.TrimStart() == ""
                || !DataValidationService.IsNumber(Phone)
                || !DataValidationService.HasMaximumCharacters(Phone.Replace(" ", ""), 10)
                || !DataValidationService.HasMinimumCharacters(Phone.Replace(" ", ""), 9))
            {
                phoneTextBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;

                if (Phone.TrimStart() == "")
                {
                    Phone = "";
                    phoneRequiredError.Visibility = Visibility.Visible;
                }
                else
                {
                    phoneRequiredError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.IsNumber(Phone) && Phone.TrimStart() != "")
                {
                    phoneIsNotValidError.Visibility = Visibility.Visible;
                }
                else
                {
                    phoneIsNotValidError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMaximumCharacters(Phone.Replace(" ", ""), 10))
                {
                    phoneTooLongError.Visibility = Visibility.Visible;
                }
                else
                {
                    phoneTooLongError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(Phone.Replace(" ", ""), 9) && Phone.TrimStart() != "")
                {
                    phoneTooShortError.Visibility = Visibility.Visible;
                }
                else
                {
                    phoneTooShortError.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                phoneTextBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                phoneRequiredError.Visibility = Visibility.Collapsed;
                phoneIsNotValidError.Visibility = Visibility.Collapsed;
                phoneTooLongError.Visibility = Visibility.Collapsed;
                phoneTooShortError.Visibility = Visibility.Collapsed;

            }
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

        private void ValidateData()
        {
            try
            {
                SignUpWindow.signUpLawyerInfoViewModel.LawyerInfoValidationCommand.Execute(null);
            }
            catch
            {
                SignUpWindow.signUpLawyerInfoViewModel.EveryThingValid = false;
                ShowDialog();
            }
            //if (SignUpWindow.signUpLawyerInfoViewModel.EveryThingValid)
            //{
                App.signUpWindow.selectorBar.SelectedItem = App.signUpWindow.SelectorBarItemOfficeInfo;
                App.signUpWindow.SelectorBarItemPersonalInfo.IsEnabled = false;
                App.signUpWindow.SelectorBarItemOfficeInfo.IsEnabled = true;
            //}
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
                    ValidateData();
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
