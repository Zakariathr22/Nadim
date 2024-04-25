using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Nadim.Services;
using Nadim.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.NetworkOperators;
using Windows.Security.Credentials;

namespace Nadim.ViewModels.SignUp
{
    public partial class SignUpEmailVerViewModel: ObservableObject
    {
        public static PasswordVault vault = new PasswordVault();
        private PasswordCredential emailOTP = new PasswordCredential();
        public int failedAttempts = 0;

        [ObservableProperty] private string email;
        [ObservableProperty] private string verificationCode = "";

        [ObservableProperty] private bool emailCodeIsValid = false;

        [ObservableProperty] private Brush verificationCodeTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        [ObservableProperty] private Visibility verificationCodeIsNotValidVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility verificationCodeIsRequiredErrorVisibility = Visibility.Collapsed;

        public SignUpEmailVerViewModel()
        {
            Email = SignUpWindow.signUpLawyerInfoViewModel.Email;
        }

        [RelayCommand]
        void GenerateSendOTP()
        {
            vault.Add(new PasswordCredential("NadimApplication", "otp", EmailVerificationService.GenerateRandomOTP().ToString()));
            emailOTP = vault.Retrieve("NadimApplication", "otp");
            EmailVerificationService.SendAccountCreationEmailOTP(Email, emailOTP);
        }

        [RelayCommand]
        void verifyOTP()
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
