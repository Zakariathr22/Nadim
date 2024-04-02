using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Nadim.Services;
using Nadim.Views.AccountRecovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Nadim.ViewModels
{
    public partial class AccountRecoveryVerificationViewModel:ObservableObject
    {
        private PasswordVault vault = new PasswordVault();
        private PasswordCredential emailOTP = new PasswordCredential();
        public int failedAttempts = 0;
        public bool EmailCodeIsValid = false;

        [ObservableProperty] string verificationCode = "";
        [ObservableProperty] string email = "";

        [ObservableProperty] Brush verificationCodeTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        [ObservableProperty] Visibility verificationCodeIsNotValidVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility verificationCodeIsRequiredErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] Visibility manyFailedAttempsErrorVisibility = Visibility.Collapsed;

        public AccountRecoveryVerificationViewModel()
        {
            Email = AccountRecoveryWindow.accountRecoveryFindAccountViewModel.Email;
        }

        [RelayCommand]
        void GenerateSendAccountRecoveryCode()
        {
            vault.Add(new PasswordCredential("NadimApplication", "otp", EmailVerificationService.GenerateRandomOTP().ToString()));
            emailOTP = vault.Retrieve("NadimApplication", "otp");
            EmailVerificationService.SendAccountRecoveryCode(Email, emailOTP);
        }

        [RelayCommand]
        void verifyAccountRecoveryCode()
        {
            EmailCodeIsValid = true;
            if (VerificationCode.TrimStart() == ""
                || VerificationCode.TrimEnd().TrimStart() != emailOTP.Password
                || int.Parse(VerificationCode.TrimEnd().TrimStart()) == 0)
            {
                EmailCodeIsValid = false;
                VerificationCodeTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;

                if (VerificationCode.TrimStart() == "")
                {
                    VerificationCodeIsRequiredErrorVisibility = Visibility.Visible;
                }
                else
                {
                    VerificationCodeIsRequiredErrorVisibility = Visibility.Collapsed;
                }

                if (VerificationCode.TrimStart() != "" && (VerificationCode.TrimEnd().TrimStart() != emailOTP.Password
                    || int.Parse(VerificationCode.TrimEnd().TrimStart()) == 0))
                {
                    failedAttempts++;
                    VerificationCodeIsNotValidVisibility = Visibility.Visible;
                }
                else
                {
                    VerificationCodeIsNotValidVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                VerificationCodeTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                VerificationCodeIsNotValidVisibility = Visibility.Collapsed;
                VerificationCodeIsRequiredErrorVisibility = Visibility.Collapsed;
            }
        }
    }
}
