﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using MySqlConnector;
using Nadim.Models;
using Nadim.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.ViewModels
{
    public partial class MainViewModel:ObservableObject
    {
        [ObservableProperty] private int appTheme;
        [ObservableProperty] private int appBackDrop;
        [ObservableProperty] private int landingPage;

        private Token token;
        private User user;

        [ObservableProperty] string navigationViewPanTitle;

        [ObservableProperty] int homeInfoBadgeValue = 10;
        [ObservableProperty] Visibility homeInfoBadgeVisibility;

        [ObservableProperty] int clientsInfoBadgeValue = 10;
        [ObservableProperty] Visibility clientsInfoBadgeVisibility;

        [ObservableProperty] int foldersInfoBadgeValue = 10;
        [ObservableProperty] Visibility foldersInfoBadgeVisibility;

        [ObservableProperty] int scheduleInfoBadgeValue = 10;
        [ObservableProperty] Visibility scheduleInfoBadgeVisibility;

        [ObservableProperty] int feesInfoBadgeValue = 10;
        [ObservableProperty] Visibility feesInfoBadgeVisibility;

        [ObservableProperty] int lawsInfoBadgeValue = 10;
        [ObservableProperty] Visibility lawsInfoBadgeVisibility;

        [ObservableProperty] int tasksInfoBadgeValue = 10;
        [ObservableProperty] Visibility tasksInfoBadgeVisibility;

        [ObservableProperty] int notesInfoBadgeValue = 10;
        [ObservableProperty] Visibility notesInfoBadgeVisibility;

        [ObservableProperty] int comunityInfoBadgeValue = 10;
        [ObservableProperty] Visibility comunityInfoBadgeVisibility;

        [ObservableProperty] int accountInfoBadgeValue = 10;
        [ObservableProperty] Visibility accountInfoBadgeVisibility;
        [ObservableProperty] string accountNavigationViewItemTextBlockText;
        public MainViewModel()
        {
            var credential = App.valut.Retrieve("NadimApplication", "token");
            token = new Token(credential);

            appTheme = int.Parse(ConfigurationService.GetAppSetting("AppTheme"));
            appBackDrop = int.Parse(ConfigurationService.GetAppSetting("AppBackDrop"));
            landingPage = int.Parse(ConfigurationService.GetAppSetting("LandingPage"));

            user = GetUserByToken(token);

            user.office = new Office();
            user.office.naming = "مكتب الأستاذ طاهري أحمد";
            NavigationViewPanTitle = user.office.naming;

            HomeInfoBadgeVisibility = SetInfoBarVisbility(HomeInfoBadgeValue);
            ClientsInfoBadgeVisibility = SetInfoBarVisbility(ClientsInfoBadgeValue);
            FoldersInfoBadgeVisibility = SetInfoBarVisbility(FoldersInfoBadgeValue);
            ScheduleInfoBadgeVisibility = SetInfoBarVisbility(ScheduleInfoBadgeValue);
            FeesInfoBadgeVisibility = SetInfoBarVisbility(FeesInfoBadgeValue);
            LawsInfoBadgeVisibility = SetInfoBarVisbility(LawsInfoBadgeValue);
            TasksInfoBadgeVisibility = SetInfoBarVisbility(TasksInfoBadgeValue);
            NotesInfoBadgeVisibility = SetInfoBarVisbility(NotesInfoBadgeValue);
            ComunityInfoBadgeVisibility = SetInfoBarVisbility(ComunityInfoBadgeValue);
            AccountInfoBadgeVisibility = SetInfoBarVisbility(AccountInfoBadgeValue);

            AccountNavigationViewItemTextBlockText = $"الحساب ({user.lastName} {user.firstName})";
        }

        public void SetAppTheme(Window window) 
        {
            if (AppTheme == 0)
            {
                ThemeSelectorService.SetTheme(ElementTheme.Default, window);
            }
            else if (AppTheme == 1)
            {
                ThemeSelectorService.SetTheme(ElementTheme.Light, window);
            }
            else
            {
                ThemeSelectorService.SetTheme(ElementTheme.Dark, window);
            }
        }

        public void SetAppBackDrop(Window window)
        {
            if (AppBackDrop == 0)
            {
                window.SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.Base };
            }
            else if (AppBackDrop == 1)
            {
                window.SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.BaseAlt };
            }
            else
            {
                window.SystemBackdrop = new DesktopAcrylicBackdrop();
            }
        }

        private Visibility SetInfoBarVisbility(int value)
        {
            if (value > 0)
            return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public User GetUserByToken(Token token)
        {
            User user = null;

            try
            {
                string query = "CALL GetUserByToken(@p_token_value, @p_ip_address, @p_user_agent, @p_machine_name)";

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
                    user = new User
                    {
                        firstName = reader["firstname"].ToString(),
                        lastName = reader["lastname"].ToString(),
                        birthDate = reader["birthdate"] as DateTimeOffset?,
                        gender = reader["gender"].ToString(),
                        profilePic = reader["profilePic"] as byte[],
                        email = reader["email"].ToString(),
                        emailVerified = reader["emailVerified"] as bool?,
                        phone = reader["phone"].ToString(),
                        phoneVerified = reader["phoneVerified"] as bool?,
                        isUserCreatedPassword = Convert.ToBoolean(reader["isUserCreatedPassword"]),
                        createdAt = (DateTime)reader["createdAt"],
                        lastUpdate = (DateTime)reader["lastUpdate"],
                        isDeleted = Convert.ToBoolean(reader["isDeleted"]),
                        office = null // You need to handle this according to your Office class
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return user;
        }
    }
}
