using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        public bool EveryThingValid = true;

        [ObservableProperty] private string lastName = "";
        [ObservableProperty] private string firstName = "";
        [ObservableProperty] private DateTimeOffset birthDate = DateTimeOffset.Now.AddYears(-18);
        [ObservableProperty] private string gender = "";
        [ObservableProperty] private int accreditation = -1;
        [ObservableProperty] private DateTimeOffset startingDate = DateTimeOffset.Now;
        [ObservableProperty] private string email = "";
        [ObservableProperty] private string phone = "";
        [ObservableProperty] private string password = "";
        [ObservableProperty] private string confirmPassword = "";

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

        [ObservableProperty] private Visibility birthDateVerifyErrorMessage = Visibility.Collapsed;

        [ObservableProperty] private Visibility genderRequiredErrorVisibilty = Visibility.Collapsed;

        [ObservableProperty] private Visibility accreditationRequiredErrorVisibilty = Visibility.Collapsed;

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

        //-------------------------------------------------------------------------------------------------------------------------------------------------

        public SignUpLawyerInfoViewModel() 
        {
            
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------

        [RelayCommand]
        void RequiredFields()
        {
            
            if (LastName.TrimStart() == "")
            {
                EveryThingValid = false;
                LastName = "";
                LastNameTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                LastNameRequiredErrorVisiblity = Visibility.Visible;
            }
            else
            {
                LastName = LastName.TrimStart();
                LastName = LastName.TrimEnd();
                LastNameTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                LastNameRequiredErrorVisiblity = Visibility.Collapsed;
            }

            if (FirstName.TrimStart() == "")
            {
                FirstName = "";
                FirstNameTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                FirstNameRequiredErrorVisiblity = Visibility.Visible;
            }
            else
            {
                FirstName = FirstName.TrimStart();
                FirstName = FirstName.TrimEnd();
                FirstNameTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                FirstNameRequiredErrorVisiblity = Visibility.Collapsed;
            }

            if (Gender == "")
            {
                GenderRequiredErrorVisibilty = Visibility.Visible;
            }
            else
            {
                GenderRequiredErrorVisibilty = Visibility.Collapsed;
            }


            if(Accreditation != 0 && Accreditation != 1)
            {
                AccreditationComboBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                AccreditationRequiredErrorVisibilty = Visibility.Visible;
            }
            else
            {
                AccreditationComboBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                AccreditationRequiredErrorVisibilty = Visibility.Collapsed;
            }

            if (Email.TrimStart() == "")
            {
                EveryThingValid = false;
                Email = "";
                EmailTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                EmailRequiredErrorVisibilty = Visibility.Visible;
            }
            else
            {
                Email = Email.TrimStart();
                Email = Email.TrimEnd();
                EmailTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                EmailRequiredErrorVisibilty = Visibility.Collapsed;
            }

            if (Phone.TrimStart() == "")
            {
                EveryThingValid = false;
                Phone = "";
                PhoneTextBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                PhoneRequiredErrorVisibilty = Visibility.Visible;
            }
            else
            {
                Phone = Phone.TrimStart();
                Phone = Phone.TrimEnd();
                PhoneTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                PhoneRequiredErrorVisibilty = Visibility.Collapsed;
            }

            if (Password == "")
            {
                EveryThingValid = false;
                PasswordBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                PasswordRequiredErrorVisibilty = Visibility.Visible;
            }
            else
            {
                PasswordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                PasswordRequiredErrorVisibilty = Visibility.Collapsed;
            }

            if (ConfirmPassword == "")
            {
                EveryThingValid = false;
                ConfirmPasswordBoxBackground = App.Current.Resources["SystemFillColorCriticalBackgroundBrush"] as Brush;
                ConfirmPasswoedRequiredErrorVisibilty = Visibility.Visible;
            }
            else
            {
                ConfirmPasswordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                ConfirmPasswoedRequiredErrorVisibilty = Visibility.Collapsed;
            }
        }
    }
}
