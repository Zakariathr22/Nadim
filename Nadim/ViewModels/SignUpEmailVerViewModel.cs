using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Nadim.Services;
using Nadim.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.NetworkOperators;

namespace Nadim.ViewModels
{
    public partial class SignUpEmailVerViewModel: ObservableObject
    {
        private int emailOTP = 0;
        public int failedAttempts = 0;

        [ObservableProperty] private string email;
        [ObservableProperty] private string verificationCode="";

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
            emailOTP = EmailVerificationService.GenerateRandomOTP();
            EmailVerificationService.SendAccountCreationEmailOTP(Email, emailOTP);
        }

        [RelayCommand]
        void verifyOTP()
        {
            EmailCodeIsValid = true;
            if (VerificationCode.TrimStart() == "" 
                || int.Parse(VerificationCode.TrimEnd().TrimStart()) != emailOTP
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

                if (VerificationCode.TrimStart() != "" && (int.Parse(VerificationCode.TrimEnd().TrimStart()) != emailOTP
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