using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Nadim.Models;
using Nadim.Services;
using Nadim.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.Account.InfoPageControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditNamePage : Page
    {
        ContentDialog dialog;
        AccountInfoViewModel accountInfoViewModel;
        public EditNamePage()
        {
            this.InitializeComponent();
        }

        public EditNamePage(ContentDialog dialog, AccountInfoViewModel accountInfoViewModel)
        {
            this.InitializeComponent();
            this.dialog = dialog;
            this.DataContext = accountInfoViewModel;
            this.accountInfoViewModel = accountInfoViewModel;
            this.accountInfoViewModel.NewFirstName = accountInfoViewModel.User.firstName;
            this.accountInfoViewModel.NewLastName = accountInfoViewModel.User.lastName;
            this.dialog.PrimaryButtonClick += (sender, args) =>
            {
                accountInfoViewModel.FullNameValidationCommand.Execute(null);

                if (!accountInfoViewModel.FullNameIsValid)
                {
                    // If validation fails, cancel the button click event,
                    // which will prevent the dialog from closing
                    args.Cancel = true;
                }
                else
                {
                    accountInfoViewModel.EditNameCommand.Execute(null);
                }
            };
            this.dialog.CloseButtonClick += (sender, args) =>
            {
                accountInfoViewModel.ResetFullNameValidationCommand.Execute(null);
            };
        }

        private void lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (firstNameTextBox.Text.Trim() != accountInfoViewModel.User.firstName || lastNameTextBox.Text.Trim() != accountInfoViewModel.User.lastName) 
                dialog.IsPrimaryButtonEnabled = true;
            else dialog.IsPrimaryButtonEnabled = false;
            string LastName = lastNameTextBox.Text;
            if (LastName.TrimStart() == ""
                || !DataValidationService.HasMaximumCharacters(LastName.TrimStart().TrimEnd(), 49)
                || !DataValidationService.HasMinimumCharacters(LastName.TrimStart().TrimEnd(), 2)
                || !DataValidationService.IsArabic(LastName.TrimStart().TrimEnd())
                || DataValidationService.ContainsNumber(LastName.TrimStart().TrimEnd()))
            {
                lastNameTextBox.Background = ThemeSelectorService.SetBrush("Critical");

                lastNameTextBox.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
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
            if (firstNameTextBox.Text.Trim() != accountInfoViewModel.User.firstName || lastNameTextBox.Text.Trim() != accountInfoViewModel.User.lastName)
                dialog.IsPrimaryButtonEnabled = true;
            else dialog.IsPrimaryButtonEnabled = false;
            string FirstName = firstNameTextBox.Text;
            if (FirstName.TrimStart() == ""
                || !DataValidationService.HasMaximumCharacters(FirstName.TrimStart().TrimEnd(), 49)
                || !DataValidationService.HasMinimumCharacters(FirstName.TrimStart().TrimEnd(), 2)
                || !DataValidationService.IsArabic(FirstName.TrimStart().TrimEnd())
                || DataValidationService.ContainsNumber(FirstName.TrimStart().TrimEnd()))
            {
                firstNameTextBox.Background = ThemeSelectorService.SetBrush("Critical");
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
    }
}
