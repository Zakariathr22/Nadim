using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using MySqlConnector;
using Nadim.Models;
using Nadim.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.ViewModels
{
    public partial class LoginViewModel: ObservableObject
    {
        private bool EverythingValid = false;
        public bool EmailOrPhoneIsCorrect = false;
        public bool LoginIsCorrect = false;
        private string salt;

        [ObservableProperty] string emailOrPhone = "";
        [ObservableProperty] string password = "";

        [ObservableProperty] private Brush emailOrPhoneTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush passwordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        [ObservableProperty] Visibility emailOrPhoneRequiredErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility inputIsNotConnectedToAnAccountVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordRequiredErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordIncorrectVisibility = Visibility.Collapsed;

        private void Validate()
        {
            EverythingValid = true;
            if (EmailOrPhone.TrimStart() == "") 
            {
                EverythingValid = false;
                EmailOrPhoneTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                EmailOrPhoneRequiredErrorVisibility = Visibility.Visible;
            }
            else
            {
                EmailOrPhoneTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                EmailOrPhoneRequiredErrorVisibility = Visibility.Collapsed;
            }

            if (Password == "")
            {
                EverythingValid=false;
                PasswordBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                PasswordRequiredErrorVisibility = Visibility.Visible;
            }
            else
            {
                PasswordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                PasswordRequiredErrorVisibility = Visibility.Collapsed;
            }
        }

        private void VerifyEmailOrPhone()
        {
            Validate();
            EmailOrPhoneIsCorrect = true;
            if (EverythingValid)
            {
                if (DataValidationService.IsNumber(EmailOrPhone.TrimEnd().TrimStart()))
                {
                    if (!DataValidationService.DoesPhoneExist(EmailOrPhone.TrimEnd().TrimStart()))
                    {
                        EmailOrPhoneIsCorrect = false;
                        EmailOrPhoneTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                        InputIsNotConnectedToAnAccountVisibility = Visibility.Visible;
                    }
                    else
                    {
                        EmailOrPhoneTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                        InputIsNotConnectedToAnAccountVisibility = Visibility.Collapsed;

                        string phone = EmailOrPhone.TrimEnd().TrimStart();

                        string query = "CALL GetUserSaltByPhone(@p_phone)";
                        MySqlParameter[] parameters = new MySqlParameter[]
                        {
                            new MySqlParameter("@p_phone", phone)
                        };

                        salt = App.dataAccess.ExecuteScalar(query, parameters) as string;
                    }
                }
                else
                {
                    if (!DataValidationService.DoesEmailExist(EmailOrPhone.TrimEnd().TrimStart()))
                    {
                        EmailOrPhoneIsCorrect = false;
                        EmailOrPhoneTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                        InputIsNotConnectedToAnAccountVisibility = Visibility.Visible;
                    }
                    else
                    {
                        EmailOrPhoneTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                        InputIsNotConnectedToAnAccountVisibility = Visibility.Collapsed;
                        string email = EmailOrPhone.TrimEnd().TrimStart();

                        string query = "CALL GetUserSaltByEmail(@p_email)";
                        MySqlParameter[] parameters = new MySqlParameter[]
                        {
                            new MySqlParameter("@p_email", email)
                        };

                        salt = App.dataAccess.ExecuteScalar(query, parameters) as string;
                    }
                }
            }
        }

        [RelayCommand]
        private void Login()
        {
            VerifyEmailOrPhone();
            if (EmailOrPhoneIsCorrect)
            {
                LoginIsCorrect = true;
                string query = "CALL `UserLoginAndGenerateToken`(@p_email_or_phone, @p_password, @p_ip_address, @p_user_agent, @p_machine_name)";
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@p_email_or_phone", EmailOrPhone.TrimStart().TrimEnd()),
                    new MySqlParameter("@p_password", CryptographyService.HashPassword(Password+salt)),
                    new MySqlParameter("@p_ip_address", MachineInfoService.GetLocalIPAddress()),
                    new MySqlParameter("@p_user_agent", "Windows"),
                    new MySqlParameter("@p_machine_name", MachineInfoService.GetComputerName())
                };
                string result = App.dataAccess.ExecuteScalar(query, parameters) as string;

                if(result == "Invalid credentials")
                {
                    LoginIsCorrect=false;
                    PasswordBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                    PasswordIncorrectVisibility = Visibility.Visible;
                }
                else
                {
                    PasswordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                    PasswordIncorrectVisibility = Visibility.Collapsed;
                    App.vault.Add(new Windows.Security.Credentials.PasswordCredential("MyApp", "token", result));
                    result = null;
                    //var vault = new Windows.Security.Credentials.PasswordVault();
                    //var credential = vault.Retrieve("MyApp", "token");
                    //var token = credential.Password;
                    //token = token;
                }
            }
        }
    }
}
