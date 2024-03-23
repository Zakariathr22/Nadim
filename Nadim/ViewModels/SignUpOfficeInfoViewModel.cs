using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Nadim.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

    }
}
