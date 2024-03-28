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
    public partial class LoginViewModel: ObservableObject
    {
        [ObservableProperty] string emailOrPhone = "";
        [ObservableProperty] string password = "";

        [ObservableProperty] private Brush passwordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush emailOrPhoneTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        [ObservableProperty] Visibility emailOrPhoneRequiredErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility passwordRequiredErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility emailOrPhoneOrPasswordIncorrectVisibility = Visibility.Collapsed;
    }
}
