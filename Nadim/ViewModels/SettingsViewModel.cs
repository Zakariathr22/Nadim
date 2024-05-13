using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty] private string appTheme;
        [ObservableProperty] private string appBackDrop;
        [ObservableProperty] private string filesFontFamily;
        [ObservableProperty] private string landingPage;


    }
}
