using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MySqlConnector;
using Nadim.Models;
using Nadim.Services;
using Nadim.Views.AccountRecovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.ViewModels.AccountRecovery
{
    public partial class AccountRecoveryViewModel : ObservableObject
    {
        public string Result = "";

        [ObservableProperty] Token resetPasswordToken;

        [RelayCommand]
        private void ResetPassword()
        {
            ResetPasswordToken.user.salt = CryptographyService.GenerateSalt();
            ResetPasswordToken.user.passwordHash = CryptographyService.HashPassword(
                AccountRecoveryWindow.accountRecoveryNewPasswordViewModel.Password +
                ResetPasswordToken.user.salt);
            if (!App.dataAccess.ConnectionStatIsOpened()) App.dataAccess.OpenConnection();
            string query = "CALL `UpdateUserPasswordAndDeactivateToken`(@p_token_value, @p_ip_address, @p_user_agent, @p_machine_name, @p_password_hash, @p_salt)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                    new MySqlParameter("@p_token_value", ResetPasswordToken.tokenValue),
                    new MySqlParameter("@p_ip_address", MachineInfoService.GetExternalIpAddress().ToString()),
                    new MySqlParameter("@p_user_agent", ResetPasswordToken.userAgent),
                    new MySqlParameter("@p_machine_name", MachineInfoService.GetComputerName()),
                    new MySqlParameter("@p_password_hash", ResetPasswordToken.user.passwordHash),
                    new MySqlParameter("@p_salt", ResetPasswordToken.user.salt)
            };
            object o = App.dataAccess.ExecuteScalar(query, parameters);
            if (o != null)
            {
                Result = o as string;
            }
            else
            {
                ResetPasswordToken.user.email = AccountRecoveryWindow.accountRecoveryFindAccountViewModel.Email;
                EmailVerificationService.SendPasswordResetNotification(ResetPasswordToken.user.email);
            }
        }

        [RelayCommand]
        private void Clear()
        {
            AccountRecoveryWindow.accountRecoveryFindAccountViewModel.Clear();
            AccountRecoveryWindow.accountRecoveryVerificationViewModel.Clear();
            AccountRecoveryWindow.accountRecoveryNewPasswordViewModel.Clear();
            ResetPasswordToken.Clear();
        }
    }
}
