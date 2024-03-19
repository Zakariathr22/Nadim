using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Nadim.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.ViewModels
{
    public partial class SignUpLawyerInfoViewModel : ObservableObject
    {
        [ObservableProperty] private string lastName = "";
        [ObservableProperty] private string firstName = "";
        [ObservableProperty] private string birthDate = "";
        [ObservableProperty] private string gender = "";
        [ObservableProperty] private bool male = false;
        [ObservableProperty] private bool female = false;
        [ObservableProperty] private string accreditation = "";
        [ObservableProperty] private string startingDate = "";
        [ObservableProperty] private string email = "";
        [ObservableProperty] private string phone = "";
        [ObservableProperty] private string password;
        [ObservableProperty] private string confirmPassword;

        //------------------------------------------------------------------------------------------------------------------------------------------

        [ObservableProperty] private Brush lastNameTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush firstNameTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush birthDateDatePickerBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush accreditationComboBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush startingDateDatePickerBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush emailTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush phoneTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush passwordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
        [ObservableProperty] private Brush confirmPasswordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;

    //------------------------------------------------------------------------------------------------------------------------------------------

        [ObservableProperty] private Visibility lastNameRequiredErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility lastNameTooLongErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility lastNameTooShortErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility lastNameIsNotArabicErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility lastNameContainsNumberErrorVisibility = Visibility.Collapsed;

        [ObservableProperty] private Visibility firstNameRequiredErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility firstNameTooLongErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility firstNameTooShortErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility firstNameIsNotArabicErrorVisibility = Visibility.Collapsed;
        [ObservableProperty] private Visibility firstNameContainsNumberErrorVisibility = Visibility.Collapsed;

        [ObservableProperty] private Visibility birthDateRequiredErrorVisibilty = Visibility.Collapsed;
        [ObservableProperty] private Visibility birthDateVerifyErrorMessage = Visibility.Collapsed;

        [ObservableProperty] private Visibility genderRequiredErrorVisibilty = Visibility.Collapsed;

        [ObservableProperty] private Visibility accreditationRequiredErrorVisibilty = Visibility.Collapsed;

        [ObservableProperty] private Visibility startDateRequiredErrorVisiblity = Visibility.Collapsed;

        [ObservableProperty] private Visibility emailRequiredErrorVisibilty = Visibility.Collapsed;
        [ObservableProperty] private Visibility emailIsNotValidErrorVisibilty = Visibility.Collapsed;
        [ObservableProperty] private Visibility emailAllreadyExistsErrorVisibilty = Visibility.Collapsed;

        [ObservableProperty] private Visibility phoneRequiredErrorVisibilty = Visibility.Collapsed;
        [ObservableProperty] private Visibility phoneIsNotValidErrorVisibilty = Visibility.Collapsed;
        [ObservableProperty] private Visibility phoneTooLongErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility phoneTooShortErrorVisiblity = Visibility.Collapsed;

        [ObservableProperty] private Visibility passwordRequiredErrorVisibilty = Visibility.Collapsed;
        [ObservableProperty] private Visibility passwordTooShortErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility passwordMustContainAlpabetErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility passwordMustContainNumberErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility passwordMustContainSymbolErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility passwordTooLongErrorVisiblity = Visibility.Collapsed;

        [ObservableProperty] private Visibility confirmPasswoedRequiredErrorVisibilty = Visibility.Collapsed;
        [ObservableProperty] private Visibility confirmPasswoedMatchErrorVisibilty = Visibility.Collapsed;

    }
}
