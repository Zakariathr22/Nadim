﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        private int emailOTP;

        [ObservableProperty] private string email;
        [ObservableProperty] private string verificationCode;

        [ObservableProperty] private bool emailCodeIsValid = false;

        [ObservableProperty] private Brush verificationCodeTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        [ObservableProperty] private Visibility verificationCodeIsNotValidVisibility = Visibility.Collapsed;

        public SignUpEmailVerViewModel()
        {
            Email = SignUpWindow.signUpLawyerInfoViewModel.Email;
        }

        [RelayCommand]
        void GenerateSendOTP()
        {
            emailOTP = EmailVerificationService.GenerateRandomOTP();
            EmailVerificationService.SendAccountCreationEmailOTP("zakotahri@outlook.com", emailOTP);
        }

        [RelayCommand]
        void verifyOTP()
        {
            EmailCodeIsValid = true;
            if (int.Parse(VerificationCode.TrimEnd().TrimStart()) == emailOTP)
            {
                EmailCodeIsValid = true;
                VerificationCodeTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                VerificationCodeIsNotValidVisibility = Visibility.Visible;
            }
            else
            {
                VerificationCodeTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                VerificationCodeIsNotValidVisibility = Visibility.Collapsed;
            }
        }
    }
}
