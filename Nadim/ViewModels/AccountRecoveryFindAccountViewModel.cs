using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using MySqlConnector;
using Nadim.Models;
using Nadim.Services;
using Nadim.Views.AccountRecovery;
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
                AccountRecoveryWindow.accountRecoveryViewModel.ResetPasswordToken = new Token(Email);
                GenerateResetPasswordToken(AccountRecoveryWindow.accountRecoveryViewModel.ResetPasswordToken);
            }
        }

        private void GenerateResetPasswordToken(Token token)
        {
            string query = "CALL `GeneratePasswordResetToken`(@p_email_or_phone, @p_ip_address, @p_user_agent, @p_machine_name)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                    new MySqlParameter("@p_email_or_phone", token.user.loger),
                    new MySqlParameter("@p_ip_address", token.ipAddress),
                    new MySqlParameter("@p_user_agent", token.userAgent),
                    new MySqlParameter("@p_machine_name", token.machineName)
            };
            string result = App.dataAccess.ExecuteScalar(query, parameters) as string;
            token.tokenValue = result;
        }
    }

}
