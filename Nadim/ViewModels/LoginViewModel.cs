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
            PasswordIncorrectVisibility = Visibility.Collapsed;
            if (EmailOrPhone.TrimStart() == "") 
            {
                EverythingValid = false;
                EmailOrPhoneTextBoxBackground = ThemeSelectorService.SetBrush("Critical");
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
                PasswordBoxBackground = ThemeSelectorService.SetBrush("Critical");
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
                        EmailOrPhoneTextBoxBackground = ThemeSelectorService.SetBrush("Critical");
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

                        object obj = App.dataAccess.ExecuteScalar(query, parameters);
                        salt = (string)obj;
                    }
                }
                else
                {
                    if (!DataValidationService.DoesEmailExist(EmailOrPhone.TrimEnd().TrimStart()))
                    {
                        EmailOrPhoneIsCorrect = false;
                        EmailOrPhoneTextBoxBackground = ThemeSelectorService.SetBrush("Critical");
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

                        object obj = App.dataAccess.ExecuteScalar(query, parameters);
                        salt = (string)obj;
                    }
                }
            }
        }

        [RelayCommand]
        private void Login()
        {
            VerifyEmailOrPhone();
            if (EverythingValid && EmailOrPhoneIsCorrect)
            {
                LoginIsCorrect = true;

                if (!App.dataAccess.ConnectionStatIsOpened()) App.dataAccess.OpenConnection();

                Token loginToken = new Token(EmailOrPhone, Password, salt);

                string query = "CALL `UserLoginAndGenerateToken`(@p_email_or_phone, @p_password, @p_ip_address, @p_user_agent, @p_machine_name)";
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@p_email_or_phone", loginToken.user.loger),
                    new MySqlParameter("@p_password", loginToken.user.passwordHash),
                    new MySqlParameter("@p_ip_address", loginToken.ipAddress),
                    new MySqlParameter("@p_user_agent", loginToken.userAgent),
                    new MySqlParameter("@p_machine_name", loginToken.machineName)
                };
                object obj = App.dataAccess.ExecuteScalar(query, parameters);
                string result = (string)obj;

                loginToken.Clear();
                loginToken = null;

                if(result == "Invalid credentials")
                {
                    LoginIsCorrect=false;
                    PasswordBoxBackground = ThemeSelectorService.SetBrush("Critical");
                    PasswordIncorrectVisibility = Visibility.Visible;
                }
                else
                {
                    PasswordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                    PasswordIncorrectVisibility = Visibility.Collapsed;
                    App.valut.Add(new Windows.Security.Credentials.PasswordCredential("NadimApplication", "token", result));
                    EmailOrPhone = "";
                    Password = "";
                    result = null;
                    //var valut = new Windows.Security.Credentials.PasswordVault();
                    //var credential = valut.Retrieve("MyApp", "token");
                    //var token = credential.Password;
                    //token = token;
                }
            }
        }
    }
}
