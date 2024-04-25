using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Nadim.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.ViewModels.AccountRecovery
{
    public partial class AccountRecoveryNewPasswordViewModel:ObservableObject
    {
        public bool EveryThingValid = false;

        [ObservableProperty] string password = "";
        [ObservableProperty] string confirmPassword = "";

        [ObservableProperty] Brush passwordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] Brush confirmPasswordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        [ObservableProperty] Visibility passwordRequiredErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordTooShortErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordMustContainAlpabetErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordMustContainNumberErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordMustContainSymbolErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordTooLongErrorVisibility = Visibility.Collapsed;

        [ObservableProperty] Visibility confirmPasswoedRequiredErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility confirmPasswoedMatchErrorVisibility = Visibility.Collapsed;

        [RelayCommand]
        private void Valdate()
        {
            EveryThingValid = true;
            if (Password == ""
                || !DataValidationService.HasMinimumCharacters(Password, 8)
                || !DataValidationService.HasMaximumCharacters(Password, 128)
                || !Password.Any(char.IsLetter)
                || !DataValidationService.ContainsSymbol(Password)
                || !DataValidationService.ContainsNumber(Password))
            {
                EveryThingValid = false;
                PasswordBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (Password == "")
                {
                    PasswordRequiredErrorVisibility = Visibility.Visible;
                }
                else
                {
                    PasswordRequiredErrorVisibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(Password, 8) && Password != "")
                {
                    PasswordTooShortErrorVisibility = Visibility.Visible;
                }
                else
                {
                    PasswordTooShortErrorVisibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMaximumCharacters(Password, 128))
                {
                    PasswordTooLongErrorVisibility = Visibility.Visible;
                }
                else
                {
                    PasswordTooLongErrorVisibility = Visibility.Collapsed;
                }

                if (!Password.Any(char.IsLetter) && Password != "")
                {
                    PasswordMustContainAlpabetErrorVisibility = Visibility.Visible;
                }
                else
                {
                    PasswordMustContainAlpabetErrorVisibility = Visibility.Collapsed;
                }
                if (!DataValidationService.ContainsSymbol(Password) && Password != "")
                {
                    PasswordMustContainSymbolErrorVisibility = Visibility.Visible;
                }
                else
                {
                    PasswordMustContainSymbolErrorVisibility = Visibility.Collapsed;
                }
                if (!DataValidationService.ContainsNumber(Password) && Password != "")
                {
                    PasswordMustContainNumberErrorVisibility = Visibility.Visible;
                }
                else
                {
                    PasswordMustContainNumberErrorVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                PasswordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                PasswordRequiredErrorVisibility = Visibility.Collapsed;
                PasswordTooShortErrorVisibility = Visibility.Collapsed;
                PasswordTooLongErrorVisibility = Visibility.Collapsed;
                PasswordMustContainNumberErrorVisibility = Visibility.Collapsed;
                PasswordMustContainSymbolErrorVisibility = Visibility.Collapsed;
                PasswordMustContainAlpabetErrorVisibility = Visibility.Collapsed;
            }

            if (ConfirmPassword == "" || ConfirmPassword != Password)
            {
                EveryThingValid = false;
                ConfirmPasswordBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (ConfirmPassword == "")
                {
                    ConfirmPasswoedRequiredErrorVisibility = Visibility.Visible;
                }
                else
                {
                    ConfirmPasswoedRequiredErrorVisibility = Visibility.Collapsed;
                }
                if (ConfirmPassword != Password && ConfirmPassword != "")
                {
                    ConfirmPasswoedMatchErrorVisibility = Visibility.Visible;
                }
                else
                {
                    ConfirmPasswoedMatchErrorVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                ConfirmPasswordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                ConfirmPasswoedRequiredErrorVisibility = Visibility.Collapsed;
                ConfirmPasswoedMatchErrorVisibility = Visibility.Collapsed;
            }
        }

        public void Clear()
        {
            Password = "";
            ConfirmPassword = "";
        }
    }
}
