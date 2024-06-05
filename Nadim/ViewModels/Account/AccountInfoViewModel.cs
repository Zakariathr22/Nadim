using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using Nadim.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
using Nadim.Services;
using MySqlConnector;
using Microsoft.UI.Xaml;
using CommunityToolkit.Mvvm.Input;
using Windows.Networking;

namespace Nadim.ViewModels.Account
{
    public partial class AccountInfoViewModel : ObservableObject
    {
        [ObservableProperty] Lawyer user;
        [ObservableProperty] BitmapImage image;
        private Token token;
        public bool isTokenValid = true;

        public AccountInfoViewModel()
        {
            var credential = App.valut.Retrieve("NadimApplication", "token");
            token = new Token(credential);
            User = GetUserDetails(token);
            if (User != null) if (User.profilePic != null) Image = FileImageService.ByteArrayToBitmapImage(User.profilePic);
        }

        private Lawyer GetUserDetails(Token token)
        {
            if (!App.dataAccess.ConnectionStatIsOpened())
                try
                {
                    App.dataAccess.OpenConnection();
                }
                catch { }

            Lawyer user = null;

            try
            {
                string query = "CALL GetUserDetails(@p_token_value, @p_ip_address, @p_user_agent, @p_machine_name)";
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@p_token_value", token.tokenValue),
                    new MySqlParameter("@p_ip_address", token.ipAddress),
                    new MySqlParameter("@p_user_agent", token.userAgent),
                    new MySqlParameter("@p_machine_name", token.machineName)
                };
                using var reader = App.dataAccess.ExecuteQuery(query, parameters);
                if (reader.Read())
                {
                    isTokenValid = reader.GetBoolean("is_token_valid");
                    if (isTokenValid)
                    {
                        user = new Lawyer();
                        if (!reader.IsDBNull(reader.GetOrdinal("firstname")))
                            user.firstName = reader.GetString("firstname").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("lastname")))
                            user.lastName = reader.GetString("lastname").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("birthdate"))) 
                            user.birthDate = (DateTimeOffset)reader.GetDateTime("birthdate");
                        if (!reader.IsDBNull(reader.GetOrdinal("gender")))
                            user.gender = reader.GetString("gender").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("profilePic")))
                        {
                            long len = reader.GetBytes(reader.GetOrdinal("profilePic"), 0, null, 0, 0);
                            byte[] array = new byte[len];
                            reader.GetBytes(reader.GetOrdinal("profilePic"), 0, array, 0, (int)len);
                            user.profilePic = array;
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("email"))) 
                            user.email = reader.GetString("email").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("emailVerified")))
                            user.emailVerified = reader.GetBoolean("emailVerified");
                        if (!reader.IsDBNull(reader.GetOrdinal("phone"))) 
                            user.phone = reader.GetString("phone").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("phoneVerified")))
                            user.phoneVerified = reader.GetBoolean("phoneVerified");
                        if (!reader.IsDBNull(reader.GetOrdinal("starting_date")))
                            user.createdAt = (DateTimeOffset)reader.GetDateTime("createdAt");
                        if (!reader.IsDBNull(reader.GetOrdinal("createdAt")))
                            user.lastUpdate = (DateTimeOffset)reader.GetDateTime("lastUpdate");
                        if (!reader.IsDBNull(reader.GetOrdinal("lastUpdate")))
                            user.accreditation = reader.GetString("accreditation").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("starting_date")))
                            user.startingDate = (DateTimeOffset)reader.GetDateTime("starting_date");
                        if (!reader.IsDBNull(reader.GetOrdinal("salt")))
                            user.salt = reader.GetString("salt").ToString();

                        user.creator = new User();
                        if (!reader.IsDBNull(reader.GetOrdinal("creator_firstname")))
                            user.creator.firstName = reader.GetString("creator_firstname").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("creator_lastname")))
                            user.creator.lastName = reader.GetString("creator_lastname").ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("creator_gender")))
                            user.creator.gender = reader.GetString("creator_gender").ToString();
                    }
                }
                else
                {
                    isTokenValid = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return user;
        }

        public bool FullNameIsValid;
        [ObservableProperty] private string newLastName;
        [ObservableProperty] private string newFirstName;

        [ObservableProperty] private Brush lastNameTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        [ObservableProperty] private Visibility lastNameRequiredErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility lastNameTooLongErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility lastNameTooShortErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility lastNameIsNotArabicErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility lastNameContainsNumberErrorVisibility = Visibility.Collapsed;

        [ObservableProperty] private Brush firstNameTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        [ObservableProperty] private Visibility firstNameRequiredErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility firstNameTooLongErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility firstNameTooShortErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility firstNameIsNotArabicErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility firstNameContainsNumberErrorVisibility = Visibility.Collapsed;

        [RelayCommand]
        void FullNameValidation()
        {
            FullNameIsValid = true;
            if (NewLastName.TrimStart() == ""
                || !DataValidationService.HasMaximumCharacters(NewLastName.TrimStart().TrimEnd(), 49)
                || !DataValidationService.HasMinimumCharacters(NewLastName.TrimStart().TrimEnd(), 2)
                || !DataValidationService.IsArabic(NewLastName.TrimStart().TrimEnd())
                || DataValidationService.ContainsNumber(NewLastName.TrimStart().TrimEnd()))
            {
                FullNameIsValid = false;
                LastNameTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (NewLastName.TrimStart() == "")
                {
                    FullNameIsValid = false;
                    NewLastName = "";
                    LastNameRequiredErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    LastNameRequiredErrorVisiblity = Visibility.Collapsed;
                }
                if (!DataValidationService.HasMaximumCharacters(NewLastName.TrimStart().TrimEnd(), 49))
                {
                    LastNameTooLongErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    LastNameTooLongErrorVisiblity = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(NewLastName.TrimStart().TrimEnd(), 2)
                    && NewLastName.TrimStart() != "")
                {
                    LastNameTooShortErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    LastNameTooShortErrorVisiblity = Visibility.Collapsed;
                }

                if (!DataValidationService.IsArabic(NewLastName.TrimStart().TrimEnd())
                    && NewLastName.TrimStart() != "")
                {
                    LastNameIsNotArabicErrorVisibility = Visibility.Visible;
                }
                else
                {
                    LastNameIsNotArabicErrorVisibility = Visibility.Collapsed;
                }

                if (DataValidationService.ContainsNumber(NewLastName.TrimStart().TrimEnd())
                    && NewLastName.TrimStart() != "")
                {
                    LastNameContainsNumberErrorVisibility = Visibility.Visible;
                }
                else
                {
                    LastNameContainsNumberErrorVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                LastNameTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                NewLastName = NewLastName.TrimStart().TrimEnd();
                LastNameRequiredErrorVisiblity = Visibility.Collapsed;
                LastNameTooLongErrorVisiblity = Visibility.Collapsed;
                LastNameTooShortErrorVisiblity = Visibility.Collapsed;
                LastNameIsNotArabicErrorVisibility = Visibility.Collapsed;
                LastNameContainsNumberErrorVisibility = Visibility.Collapsed;
            }

            if (NewFirstName.TrimStart() == ""
                || !DataValidationService.HasMaximumCharacters(NewFirstName.TrimStart().TrimEnd(), 49)
                || !DataValidationService.HasMinimumCharacters(NewFirstName.TrimStart().TrimEnd(), 2)
                || !DataValidationService.IsArabic(NewFirstName.TrimStart().TrimEnd())
                || DataValidationService.ContainsNumber(NewFirstName.TrimStart().TrimEnd()))
            {
                FullNameIsValid = false;
                FirstNameTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (NewFirstName.TrimStart() == "")
                {
                    NewFirstName = "";
                    FirstNameRequiredErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    FirstNameRequiredErrorVisiblity = Visibility.Collapsed;
                }
                if (!DataValidationService.HasMaximumCharacters(NewFirstName.TrimStart().TrimEnd(), 49))
                {
                    FirstNameTooLongErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    FirstNameTooLongErrorVisiblity = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(NewFirstName.TrimStart().TrimEnd(), 2)
                    && NewFirstName.TrimStart() != "")
                {
                    FirstNameTooShortErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    FirstNameTooShortErrorVisiblity = Visibility.Collapsed;
                }

                if (!DataValidationService.IsArabic(NewFirstName.TrimStart().TrimEnd())
                    && NewFirstName.TrimStart() != "")
                {
                    FirstNameIsNotArabicErrorVisibility = Visibility.Visible;
                }
                else
                {
                    FirstNameIsNotArabicErrorVisibility = Visibility.Collapsed;
                }

                if (DataValidationService.ContainsNumber(NewFirstName.TrimStart().TrimEnd())
                    && NewFirstName.TrimStart() != "")
                {
                    FirstNameContainsNumberErrorVisibility = Visibility.Visible;
                }
                else
                {
                    FirstNameContainsNumberErrorVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                FirstNameTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                NewFirstName = NewFirstName.TrimStart().TrimEnd();
                FirstNameRequiredErrorVisiblity = Visibility.Collapsed;
                FirstNameTooLongErrorVisiblity = Visibility.Collapsed;
                FirstNameTooShortErrorVisiblity = Visibility.Collapsed;
                FirstNameIsNotArabicErrorVisibility = Visibility.Collapsed;
                FirstNameContainsNumberErrorVisibility = Visibility.Collapsed;

            }

        }
        [RelayCommand]
        private void ResetFullNameValidation()
        {
            LastNameTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

            LastNameRequiredErrorVisiblity = Visibility.Collapsed;
            LastNameTooLongErrorVisiblity = Visibility.Collapsed;
            LastNameTooShortErrorVisiblity = Visibility.Collapsed;
            LastNameIsNotArabicErrorVisibility = Visibility.Collapsed;
            LastNameContainsNumberErrorVisibility = Visibility.Collapsed;

            FirstNameTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

            FirstNameRequiredErrorVisiblity = Visibility.Collapsed;
            FirstNameTooLongErrorVisiblity = Visibility.Collapsed;
            FirstNameTooShortErrorVisiblity = Visibility.Collapsed;
            FirstNameIsNotArabicErrorVisibility = Visibility.Collapsed;
            FirstNameContainsNumberErrorVisibility = Visibility.Collapsed;
        }

        [RelayCommand]
        private void EditName()
        {
            var credential = App.valut.Retrieve("NadimApplication", "token");
            Token token = new Token(credential);
            string query = "CALL UpdateUserFullName(@p_token_value, @p_ip_address, @p_user_agent, @p_machine_name, @p_firstname, @p_lastname)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@p_token_value", token.tokenValue),
                new MySqlParameter("@p_ip_address", token.ipAddress),
                new MySqlParameter("@p_user_agent", token.userAgent),
                new MySqlParameter("@p_machine_name", token.machineName),
                new MySqlParameter("@p_firstname", NewFirstName),
                new MySqlParameter("@p_lastname", NewLastName)
            };
            string message = App.dataAccess.ExecuteScalar(query, parameters).ToString();
            if (message == "update success")
            {
                User.FirstName = NewFirstName;
                User.LastName = NewLastName;
                //User.fullName = user.fullName;
            }
        }

    }
}
