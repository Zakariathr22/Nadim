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
    public partial class AccountRecoveryVerificationViewModel:ObservableObject
    {
        [ObservableProperty] string verificationCode = "";

        [ObservableProperty] Brush verificationCodeTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        [ObservableProperty] Visibility verificationCodeIsNotValidVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility verificationCodeIsRequiredErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility manyFailedAttempsErrorVisibility = Visibility.Collapsed;
    }
}
