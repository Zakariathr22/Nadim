using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Nadim.Models;
using Nadim.Services;
using System;
using System.Collections.Generic;
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

        private Office office;
        private User user;

        [ObservableProperty] string navigationViewPanTitle;

        [ObservableProperty] int homeInfoBadgeValue = 0;
        [ObservableProperty] Visibility homeInfoBadgeVisibility;

        [ObservableProperty] int clientsInfoBadgeValue = 0;
        [ObservableProperty] Visibility clientsInfoBadgeVisibility;

        [ObservableProperty] int foldersInfoBadgeValue = 0;
        [ObservableProperty] Visibility foldersInfoBadgeVisibility;

        [ObservableProperty] int scheduleInfoBadgeValue = 0;
        [ObservableProperty] Visibility scheduleInfoBadgeVisibility;

        [ObservableProperty] int feesInfoBadgeValue = 0;
        [ObservableProperty] Visibility feesInfoBadgeVisibility;

        [ObservableProperty] int lawsInfoBadgeValue = 0;
        [ObservableProperty] Visibility lawsInfoBadgeVisibility;

        [ObservableProperty] int tasksInfoBadgeValue = 0;
        [ObservableProperty] Visibility tasksInfoBadgeVisibility;

        [ObservableProperty] int notesInfoBadgeValue = 0;
        [ObservableProperty] Visibility notesInfoBadgeVisibility;

        [ObservableProperty] int comunityInfoBadgeValue = 0;
        [ObservableProperty] Visibility comunityInfoBadgeVisibility;

        [ObservableProperty] int accountInfoBadgeValue = 0;
        [ObservableProperty] Visibility accountInfoBadgeVisibility;
        [ObservableProperty] string accountNavigationViewItemTextBlockText;
        public MainViewModel()
        {
            appTheme = int.Parse(ConfigurationService.GetAppSetting("AppTheme"));
            appBackDrop = int.Parse(ConfigurationService.GetAppSetting("AppBackDrop"));
            landingPage = int.Parse(ConfigurationService.GetAppSetting("LandingPage"));

            office = new Office();
            office.naming = "مكتب الأستاذ طاهري زكرياء";

            user = new User();
            user.firstName = "زكرياء";
            user.lastName = "طاهري";

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
    }
}
