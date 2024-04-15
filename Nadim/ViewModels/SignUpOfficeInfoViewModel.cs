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
using Windows.Networking;

namespace Nadim.ViewModels
{
    public partial class SignUpOfficeInfoViewModel : ObservableObject
    {
        public bool EveryThingValid = false;

        [ObservableProperty] private string naming;
        [ObservableProperty] private string accreditation = "";
        [ObservableProperty] private string headquarters = "";
        [ObservableProperty] private int wilaya = -1;
        [ObservableProperty] private int municipality = -1;
        [ObservableProperty] private bool isCompany = false;
        [ObservableProperty] private string phone1 = "";
        [ObservableProperty] private string phone2 = "";
        [ObservableProperty] private string email = "";
        [ObservableProperty] private string fax = "";        

        //------------------------------------------------------------------------------------------------------------------------------------------

        [ObservableProperty] private Brush namingTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush headquartersTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush wilayaComboBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        //[ObservableProperty] private Brush municipalityComboBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush phone1TextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush phone2TextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush emailTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush faxTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

        //------------------------------------------------------------------------------------------------------------------------------------------

        [ObservableProperty] private Visibility namingRequiredErrorVisibility = Visibility.Collapsed;

        [ObservableProperty] private Visibility headquartersRequiredErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility headquartersToLongErrorVisibility = Visibility.Collapsed;

        [ObservableProperty] private Visibility wilayaRequiredErrorVisibility = Visibility.Collapsed;

        [ObservableProperty] private Visibility phone1RequiredErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility phone1IsNotValidErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility phone1TooLongErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility phone1TooShortErrorVisibility = Visibility.Collapsed;

        [ObservableProperty] private Visibility phone2IsNotValidErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility phone2TooLongErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility phone2TooShortErrorVisibility = Visibility.Collapsed;

        [ObservableProperty] private Visibility faxIsNotValidErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility faxTooLongErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility faxTooShortErrorVisibility = Visibility.Collapsed;

        [ObservableProperty] private Visibility emailIsNotValidErrorVisibility = Visibility.Collapsed;

        //-------------------------------------------------------------------------------------------------------------------------------------------------

        public SignUpOfficeInfoViewModel()
        { 
            if (SignUpWindow.signUpLawyerInfoViewModel.Gender == "ذكر")
            {
                naming = $"مكتب الأستاذ {SignUpWindow.signUpLawyerInfoViewModel.LastName} {SignUpWindow.signUpLawyerInfoViewModel.FirstName}";
            }
            else if (SignUpWindow.signUpLawyerInfoViewModel.Gender == "أنثى")
            {
                naming = $"مكتب الأستاذة {SignUpWindow.signUpLawyerInfoViewModel.LastName} {SignUpWindow.signUpLawyerInfoViewModel.FirstName}";
            }
            else
            {
                naming = $"مكتب الأستاذ(ة) {SignUpWindow.signUpLawyerInfoViewModel.LastName} {SignUpWindow.signUpLawyerInfoViewModel.FirstName}";
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------

        [RelayCommand]
        void OfficeInfoValidation()
        {
            EveryThingValid = true;
            if (Naming.TrimStart() == "")
            {
                EveryThingValid = false;
                NamingTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (Naming.TrimStart() == "")
                {
                    EveryThingValid = false;
                    Naming = "";
                    NamingRequiredErrorVisibility = Visibility.Visible;
                }
                else
                {
                    NamingRequiredErrorVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                NamingTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                Naming = Naming.TrimStart().TrimEnd();
                NamingRequiredErrorVisibility = Visibility.Collapsed;
            }

            if (Headquarters.TrimStart() == "" || !DataValidationService.HasMaximumCharacters(Headquarters.TrimStart().TrimEnd(),199))
            {
                EveryThingValid = false;
                HeadquartersTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                if (Headquarters.TrimStart() == "")
                {
                    EveryThingValid = false;
                    Headquarters = "";
                    HeadquartersRequiredErrorVisibility = Visibility.Visible;
                }
                else
                {
                    HeadquartersRequiredErrorVisibility = Visibility.Collapsed;
                }
                if (!DataValidationService.HasMaximumCharacters(Headquarters.TrimStart().TrimEnd(), 199))
                {
                    HeadquartersToLongErrorVisibility = Visibility.Visible;
                }
                else
                {
                    HeadquartersToLongErrorVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                HeadquartersTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                Headquarters = Headquarters.TrimStart().TrimEnd();
                HeadquartersRequiredErrorVisibility = Visibility.Collapsed;
                HeadquartersRequiredErrorVisibility = Visibility.Collapsed;
            }

            if (Wilaya <= -1  || Wilaya >= 58)
            {
                EveryThingValid = false;
                WilayaComboBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                WilayaRequiredErrorVisibility = Visibility.Visible;
            }
            else
            {
                WilayaComboBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                WilayaRequiredErrorVisibility = Visibility.Collapsed;
            }

            if (Phone1.TrimStart() == ""
                || !DataValidationService.IsNumber(Phone1)
                || !DataValidationService.HasMaximumCharacters(Phone1.Replace(" ", ""), 10)
                || !DataValidationService.HasMinimumCharacters(Phone1.Replace(" ", ""), 9))
            {
                EveryThingValid = false;
                Phone1TextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;

                if (Phone1.TrimStart() == "")
                {
                    Phone1 = "";
                    Phone1RequiredErrorVisibility = Visibility.Visible;
                }
                else
                {
                    Phone1RequiredErrorVisibility = Visibility.Collapsed;
                }

                if (!DataValidationService.IsNumber(Phone1) && Phone1.TrimStart() != "")
                {
                    Phone1IsNotValidErrorVisibility = Visibility.Visible;
                }
                else
                {
                    Phone1IsNotValidErrorVisibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMaximumCharacters(Phone1.Replace(" ", ""), 10))
                {
                    Phone1TooLongErrorVisibility = Visibility.Visible;
                }
                else
                {
                    Phone1TooLongErrorVisibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(Phone1.Replace(" ", ""), 9) && Phone1.TrimStart() != "")
                {
                    Phone1TooShortErrorVisibility = Visibility.Visible;
                }
                else
                {
                    Phone1TooShortErrorVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                Phone1 = Phone1.Replace(" ", "");
                Phone1TextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                Phone1RequiredErrorVisibility = Visibility.Collapsed;
                Phone1IsNotValidErrorVisibility = Visibility.Collapsed;
                Phone1TooLongErrorVisibility = Visibility.Collapsed;
                Phone1TooShortErrorVisibility = Visibility.Collapsed;
            }

            if (Phone2.TrimStart() != ""
                && (!DataValidationService.IsNumber(Phone2)
                || !DataValidationService.HasMaximumCharacters(Phone2.Replace(" ", ""), 10)
                || !DataValidationService.HasMinimumCharacters(Phone2.Replace(" ", ""), 9)))
            {
                EveryThingValid = false;
                Phone2TextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;

                if (!DataValidationService.IsNumber(Phone2) && Phone2.TrimStart() != "")
                {
                    Phone2IsNotValidErrorVisibility = Visibility.Visible;
                }
                else
                {
                    Phone2IsNotValidErrorVisibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMaximumCharacters(Phone2.Replace(" ", ""), 10))
                {
                    Phone2TooLongErrorVisibility = Visibility.Visible;
                }
                else
                {
                    Phone2TooLongErrorVisibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(Phone2.Replace(" ", ""), 9) && Phone2.TrimStart() != "")
                {
                    Phone2TooShortErrorVisibility = Visibility.Visible;
                }
                else
                {
                    Phone2TooShortErrorVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                Phone2 = Phone2.Replace(" ", "");
                Phone2TextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                Phone2IsNotValidErrorVisibility = Visibility.Collapsed;
                Phone2TooLongErrorVisibility = Visibility.Collapsed;
                Phone2TooShortErrorVisibility = Visibility.Collapsed;
            }

            if (Email.TrimStart() != "" && !DataValidationService.IsValidEmail(Email.TrimStart().TrimStart()))
            {
                EveryThingValid = false;
                EmailTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;

                if (!DataValidationService.IsValidEmail(Email.TrimStart().TrimStart()) && Email.TrimStart() != "")
                {
                    EmailIsNotValidErrorVisibility = Visibility.Visible;
                }
                else
                {
                    EmailIsNotValidErrorVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                Email = Email.TrimStart().TrimEnd();
                EmailTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                EmailIsNotValidErrorVisibility = Visibility.Collapsed;
            }

            if (Fax.TrimStart() != ""
                && (!DataValidationService.IsNumber(Fax)
                || !DataValidationService.HasMaximumCharacters(Fax.Replace(" ", ""), 10)
                || !DataValidationService.HasMinimumCharacters(Fax.Replace(" ", ""), 9)))
            {
                EveryThingValid = false;
                FaxTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;

                if (!DataValidationService.IsNumber(Fax) && Fax.TrimStart() != "")
                {
                    FaxIsNotValidErrorVisibility = Visibility.Visible;
                }
                else
                {
                    FaxIsNotValidErrorVisibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMaximumCharacters(Fax.Replace(" ", ""), 10))
                {
                    FaxTooLongErrorVisibility = Visibility.Visible;
                }
                else
                {
                    FaxTooLongErrorVisibility = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(Fax.Replace(" ", ""), 9) && Fax.TrimStart() != "")
                {
                    FaxTooShortErrorVisibility = Visibility.Visible;
                }
                else
                {
                    FaxTooShortErrorVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                Fax = Fax.Replace(" ", "");
                FaxTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                FaxIsNotValidErrorVisibility = Visibility.Collapsed;
                FaxTooLongErrorVisibility = Visibility.Collapsed;
                FaxTooShortErrorVisibility = Visibility.Collapsed;
            }
        }

    }
}
