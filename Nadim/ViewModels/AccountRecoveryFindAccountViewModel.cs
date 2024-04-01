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
    public partial class AccountRecoveryFindAccountViewModel: ObservableObject
    {
        [ObservableProperty] string email = "";

        [ObservableProperty] Brush emailTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        [ObservableProperty] Visibility emailOrPhoneRequiredErrorVisbility = Visibility.Collapsed;
        [ObservableProperty] Visibility inputIsNotConnectedToAnAccountVisibility = Visibility.Collapsed;
    }
}
