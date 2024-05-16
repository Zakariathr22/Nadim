using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Composition.SystemBackdrops;
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
    public partial class MainViewModel:ObservableObject
    {
        [ObservableProperty] private int appTheme;
        [ObservableProperty] private int appBackDrop;
        [ObservableProperty] private int landingPage;

        public MainViewModel()
        {
            appTheme = int.Parse(ConfigurationService.GetAppSetting("AppTheme"));
            appBackDrop = int.Parse(ConfigurationService.GetAppSetting("AppBackDrop"));
            landingPage = int.Parse(ConfigurationService.GetAppSetting("LandingPage"));
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
    }
}
