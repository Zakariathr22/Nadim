using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.ViewModels
{
    public partial class AccountRecoveryNewPasswordViewModel:ObservableObject
    {
        [ObservableProperty] string password = "";
        [ObservableProperty] string confirmPassword = "";

        [ObservableProperty] Brush passwordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] Brush confirmPasswordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        [ObservableProperty] Visibility passwordRequiredErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordTooShortErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordMustContainAlpabetErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordMustContainNumberErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordMustContainSymbolErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordTooLongErrorVisibility = Visibility.Collapsed;

        [ObservableProperty] Visibility confirmPasswoedRequiredErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility confirmPasswoedMatchErrorVisibility = Visibility.Collapsed;
    }
}
