using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Nadim.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty] private int appTheme;
        [ObservableProperty] private int appBackDrop;
        [ObservableProperty] private int filesFontFamily;
        [ObservableProperty] private int landingPage;

        public SettingsViewModel()
        {
            appTheme = int.Parse(ConfigurationService.GetAppSetting("AppTheme"));
            appBackDrop = int.Parse(ConfigurationService.GetAppSetting("AppBackDrop"));
            filesFontFamily = int.Parse(ConfigurationService.GetAppSetting("FilesFontFamily"));
            landingPage = int.Parse(ConfigurationService.GetAppSetting("LandingPage"));
        }

        [RelayCommand]
        void ThemeChanged()
        {
            if (AppTheme == 0)
            {
                ThemeSelectorService.SetTheme(ElementTheme.Default, App.mainWindow);
            }
            else if (AppTheme == 1)
            {
                ThemeSelectorService.SetTheme(ElementTheme.Light, App.mainWindow);
            }
            else
            {
                ThemeSelectorService.SetTheme(ElementTheme.Dark, App.mainWindow);
            }
            ConfigurationService.SetAppSetting("AppTheme", AppTheme);
        }

        [RelayCommand]
        void BackDropChanged()
        {
            ConfigurationService.SetAppSetting("AppBackDrop", AppBackDrop);
        }

        [RelayCommand]
        void FilesFontFamilyChanged() 
        {
            ConfigurationService.SetAppSetting("FilesFontFamily", FilesFontFamily);
        }

        [RelayCommand]
        void LandingPageChanged()
        {
            ConfigurationService.SetAppSetting("LandingPage", LandingPage);
        }
    }
}
