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

namespace Nadim.ViewModels
{
    public partial class AccountRecoveryFindAccountViewModel: ObservableObject
    {
        public bool EverythingIsValid = false;

        [ObservableProperty] string email = "";

        [ObservableProperty] Brush emailTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        [ObservableProperty] Visibility emailRequiredErrorVisbility = Visibility.Collapsed;
        [ObservableProperty] Visibility inputIsNotConnectedToAnAccountVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility emailIsNotValidErrorVisibility = Visibility.Collapsed;

        [RelayCommand]
        private void Validate()
        {
            EverythingIsValid = true;
            bool doesEmailExist = DataValidationService.DoesEmailExist(Email.TrimStart().TrimEnd());

            if (Email.TrimStart() == "" || !doesEmailExist || !DataValidationService.IsValidEmail(Email.TrimStart().TrimEnd()))
            {
                EverythingIsValid = false;
                EmailTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (Email.TrimStart() == "")
                {
                    EmailRequiredErrorVisbility = Visibility.Visible;
                }
                else
                {
                    EmailRequiredErrorVisbility = Visibility.Collapsed;
                }
                if (!DataValidationService.IsValidEmail(Email.TrimStart().TrimEnd()) && Email.TrimStart() != "")
                {
                    EmailIsNotValidErrorVisibility = Visibility.Visible;
                }
                else
                {
                    EmailIsNotValidErrorVisibility = Visibility.Collapsed;
                    if (!doesEmailExist && Email.TrimStart() != "")
                    {
                        InputIsNotConnectedToAnAccountVisibility = Visibility.Visible;
                    }
                    else
                    {
                        InputIsNotConnectedToAnAccountVisibility = Visibility.Collapsed;
                    }
                }

            }
            else
            {
                EmailTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                EmailRequiredErrorVisbility = Visibility.Collapsed;
                InputIsNotConnectedToAnAccountVisibility = Visibility.Collapsed;
                EmailIsNotValidErrorVisibility = Visibility.Collapsed;
            }
        }
    }

}
