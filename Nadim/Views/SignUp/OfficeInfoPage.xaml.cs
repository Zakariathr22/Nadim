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
    public sealed partial class OfficeInfoPage : Page
    {
        public OfficeInfoPage()
        {
            this.InitializeComponent();
            SignUpWindow.signUpOfficeInfoViewModel = new ViewModels.SignUp.SignUpOfficeInfoViewModel();
            mainPanel.DataContext = SignUpWindow.signUpOfficeInfoViewModel;
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            App.signUpWindow.selectorBar.SelectedItem = App.signUpWindow.SelectorBarItemPersonalInfo;
            App.signUpWindow.SelectorBarItemPersonalInfo.IsEnabled = true;
            App.signUpWindow.SelectorBarItemOfficeInfo.IsEnabled = false;
        }

        private void createAccount_Click(object sender, RoutedEventArgs e)
        {
            Validate();
        }

        private void Validate()
        {
            SignUpWindow.signUpOfficeInfoViewModel.OfficeInfoValidationCommand.Execute(this);
            if (SignUpWindow.signUpOfficeInfoViewModel.EveryThingValid)
            {
                App.signUpWindow.selectorBar.SelectedItem = App.signUpWindow.SelectorBarItemEmailVer;
                App.signUpWindow.SelectorBarItemEmailVer.IsEnabled = true;
                App.signUpWindow.SelectorBarItemOfficeInfo.IsEnabled = false;
            }
        }

        private void namingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Naming = namingTextBox.Text;
            if (Naming.TrimStart() == "")
            {
                namingTextBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (Naming.TrimStart() == "")
                {
                    namingRequiredError.Visibility = Visibility.Visible;
                }
                else
                {
                    namingRequiredError.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                namingTextBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                namingRequiredError.Visibility = Visibility.Collapsed;
            }
        }

        private void headquartersTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Headquarters = headquartersTextBox.Text;
            if (Headquarters.TrimStart() == "" || !DataValidationService.HasMaximumCharacters(Headquarters.TrimStart().TrimEnd(), 100))
            {
                headquartersTextBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (Headquarters.TrimStart() == "")
                {
                    headquartersRequiredError.Visibility = Visibility.Visible;
                }
                else
                {
                    headquartersRequiredError.Visibility = Visibility.Collapsed;
                }
                if (!DataValidationService.HasMaximumCharacters(Headquarters.TrimStart().TrimEnd(), 100))
                {
                    headquartersToLongError.Visibility = Visibility.Visible;
                }
                else
                {
                    headquartersRequiredError.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                headquartersTextBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                headquartersRequiredError.Visibility = Visibility.Collapsed;
                headquartersRequiredError.Visibility = Visibility.Collapsed;
            }
        }

        private void wilayaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Wilaya = wilayaComboBox.SelectedIndex;
            if (Wilaya <= -1 || Wilaya >= 58)
            {
                wilayaComboBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                wilayaRequiredError.Visibility = Visibility.Visible;
            }
            else
            {
                wilayaComboBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                wilayaRequiredError.Visibility = Visibility.Collapsed;
            }
        }

        private void phone1TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int currentPosition = phone1TextBox.SelectionStart - 1;
            string text = ((TextBox)sender).Text;

            Regex regex = new Regex("^[0-9]*$");

            if (!regex.IsMatch(text))
            {
                var foundChar = Regex.Match(phone1TextBox.Text, @"[^0-9]");
                if (foundChar.Success)
                {
                    phone1TextBox.Text = phone1TextBox.Text.Remove(foundChar.Index, 1);
                }

                phone1TextBox.Select(currentPosition, 0);
            }
            string Phone1 = phone1TextBox.Text;
            if (Phone1.TrimStart() == ""
                || !DataValidationService.IsNumber(Phone1)
                || !DataValidationService.HasMaximumCharacters(Phone1.Replace(" ", ""), 10)
                || !DataValidationService.HasMinimumCharacters(Phone1.Replace(" ", ""), 9))
            {
                phone1TextBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;

                if (Phone1.TrimStart() == "")
                {
                    Phone1 = "";
                    phone1RequiredError.Visibility = Visibility.Visible;
                }
                else
                {
                    phone1RequiredError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.IsNumber(Phone1) && Phone1.TrimStart() != "")
                {
                    phone1IsNotValidError.Visibility = Visibility.Visible;
                }
                else
                {
                    phone1IsNotValidError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMaximumCharacters(Phone1.Replace(" ", ""), 10))
                {
                    phone1TooLongError.Visibility = Visibility.Visible;
                }
                else
                {
                    phone1TooLongError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(Phone1.Replace(" ", ""), 9) && Phone1.TrimStart() != "")
                {
                    phone1TooShortError.Visibility = Visibility.Visible;
                }
                else
                {
                    phone1TooShortError.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                phone1TextBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                phone1RequiredError.Visibility = Visibility.Collapsed;
                phone1IsNotValidError.Visibility = Visibility.Collapsed;
                phone1TooLongError.Visibility = Visibility.Collapsed;
                phone1TooShortError.Visibility = Visibility.Collapsed;
            }
        }

        private void phone2TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int currentPosition = phone2TextBox.SelectionStart - 1;
            string text = ((TextBox)sender).Text;

            Regex regex = new Regex("^[0-9]*$");

            if (!regex.IsMatch(text))
            {
                var foundChar = Regex.Match(phone2TextBox.Text, @"[^0-9]");
                if (foundChar.Success)
                {
                    phone2TextBox.Text = phone2TextBox.Text.Remove(foundChar.Index, 1);
                }

                phone2TextBox.Select(currentPosition, 0);
            }
            string Phone2 = phone2TextBox.Text;
            if (Phone2.TrimStart() != ""
                && (!DataValidationService.IsNumber(Phone2)
                || !DataValidationService.HasMaximumCharacters(Phone2.Replace(" ", ""), 10)
                || !DataValidationService.HasMinimumCharacters(Phone2.Replace(" ", ""), 9)))
            {
                phone2TextBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;

                if (!DataValidationService.IsNumber(Phone2) && Phone2.TrimStart() != "")
                {
                    phone2IsNotValidError.Visibility = Visibility.Visible;
                }
                else
                {
                    phone2IsNotValidError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMaximumCharacters(Phone2.Replace(" ", ""), 10))
                {
                    phone2TooLongError.Visibility = Visibility.Visible;
                }
                else
                {
                    phone2TooLongError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(Phone2.Replace(" ", ""), 9) && Phone2.TrimStart() != "")
                {
                    phone2TooShortError.Visibility = Visibility.Visible;
                }
                else
                {
                    phone2TooShortError.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                phone2TextBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                phone2IsNotValidError.Visibility = Visibility.Collapsed;
                phone2TooLongError.Visibility = Visibility.Collapsed;
                phone2TooShortError.Visibility = Visibility.Collapsed;
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

        private void faxTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int currentPosition = faxTextBox.SelectionStart - 1;
            string text = ((TextBox)sender).Text;

            Regex regex = new Regex("^[0-9]*$");

            if (!regex.IsMatch(text))
            {
                var foundChar = Regex.Match(faxTextBox.Text, @"[^0-9]");
                if (foundChar.Success)
                {
                    faxTextBox.Text = faxTextBox.Text.Remove(foundChar.Index, 1);
                }

                faxTextBox.Select(currentPosition, 0);
            }
            string Fax = faxTextBox.Text;
            if (Fax.TrimStart() != ""
                && (!DataValidationService.IsNumber(Fax)
                || !DataValidationService.HasMaximumCharacters(Fax.Replace(" ", ""), 10)
                || !DataValidationService.HasMinimumCharacters(Fax.Replace(" ", ""), 9)))
            {
                faxTextBox.Background = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;

                if (!DataValidationService.IsNumber(Fax) && Fax.TrimStart() != "")
                {
                    faxIsNotValidError.Visibility = Visibility.Visible;
                }
                else
                {
                    faxIsNotValidError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMaximumCharacters(Fax.Replace(" ", ""), 10))
                {
                    faxTooLongError.Visibility = Visibility.Visible;
                }
                else
                {
                    faxTooLongError.Visibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(Fax.Replace(" ", ""), 9) && Fax.TrimStart() != "")
                {
                    faxTooShortError.Visibility = Visibility.Visible;
                }
                else
                {
                    faxTooShortError.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                faxTextBox.Background = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                faxIsNotValidError.Visibility = Visibility.Collapsed;
                faxTooLongError.Visibility = Visibility.Collapsed;
                faxTooShortError.Visibility = Visibility.Collapsed;
            }
        }
    }
}
