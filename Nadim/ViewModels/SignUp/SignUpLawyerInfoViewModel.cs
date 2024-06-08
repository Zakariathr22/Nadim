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

namespace Nadim.ViewModels.SignUp
{
    public partial class SignUpLawyerInfoViewModel : ObservableObject
    {
        public bool EveryThingValid = false;

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

        [ObservableProperty] private Visibility genderRequiredErrorVisibilty = Visibility.Collapsed;

        [ObservableProperty] private Visibility accreditationRequiredErrorVisibilty = Visibility.Collapsed;

        [ObservableProperty] private Visibility startingDateErrorVisibilty = Visibility.Collapsed;

        [ObservableProperty] private Visibility emailRequiredErrorVisibilty = Visibility.Collapsed;
        [ObservableProperty] private Visibility emailIsNotValidErrorVisibilty = Visibility.Collapsed;
        [ObservableProperty] private Visibility emailAllreadyExistsErrorVisibilty = Visibility.Collapsed;

        [ObservableProperty] private Visibility phoneRequiredErrorVisibilty = Visibility.Collapsed;
        [ObservableProperty] private Visibility phoneIsNotValidErrorVisibilty = Visibility.Collapsed;
        [ObservableProperty] private Visibility phoneTooLongErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility phoneTooShortErrorVisiblity = Visibility.Collapsed;
        [ObservableProperty] private Visibility phoneAllreadyExistsErrorVisibilty = Visibility.Collapsed;


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
        void LawyerInfoValidation()
        {
            EveryThingValid = true;
            bool emailExistBefor = DataValidationService.DoesEmailExist(Email.TrimStart().TrimStart());
            bool phoneExistBefor = DataValidationService.DoesPhoneExist(Phone.Replace(" ", ""));


            if (LastName.TrimStart() == ""
                || !DataValidationService.HasMaximumCharacters(LastName.TrimStart().TrimEnd(), 49)
                || !DataValidationService.HasMinimumCharacters(LastName.TrimStart().TrimEnd(), 2)
                || !DataValidationService.IsArabic(LastName.TrimStart().TrimEnd())
                || DataValidationService.ContainsNumber(LastName.TrimStart().TrimEnd()))
            {
                EveryThingValid = false;
                LastNameTextBoxBackground = ThemeSelectorService.SetBrush("Critical");
                if (LastName.TrimStart() == "")
                {
                    EveryThingValid = false;
                    LastName = "";
                    LastNameRequiredErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    LastNameRequiredErrorVisiblity = Visibility.Collapsed;
                }
                if (!DataValidationService.HasMaximumCharacters(LastName.TrimStart().TrimEnd(), 49))
                {
                    LastNameTooLongErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    LastNameTooLongErrorVisiblity = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(LastName.TrimStart().TrimEnd(), 2)
                    && LastName.TrimStart() != "")
                {
                    LastNameTooShortErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    LastNameTooShortErrorVisiblity = Visibility.Collapsed;
                }

                if (!DataValidationService.IsArabic(LastName.TrimStart().TrimEnd())
                    && LastName.TrimStart() != "")
                {
                    LastNameIsNotArabicErrorVisibility = Visibility.Visible;
                }
                else
                {
                    LastNameIsNotArabicErrorVisibility = Visibility.Collapsed;
                }

                if (DataValidationService.ContainsNumber(LastName.TrimStart().TrimEnd())
                    && LastName.TrimStart() != "")
                {
                    LastNameContainsNumberErrorVisibility = Visibility.Visible;
                }
                else
                {
                    LastNameContainsNumberErrorVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                LastNameTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                LastName = LastName.TrimStart().TrimEnd();
                LastNameRequiredErrorVisiblity = Visibility.Collapsed;
                LastNameTooLongErrorVisiblity = Visibility.Collapsed;
                LastNameTooShortErrorVisiblity = Visibility.Collapsed;
                LastNameIsNotArabicErrorVisibility = Visibility.Collapsed;
                LastNameContainsNumberErrorVisibility = Visibility.Collapsed;
            }

            if (FirstName.TrimStart() == ""
                || !DataValidationService.HasMaximumCharacters(FirstName.TrimStart().TrimEnd(), 49)
                || !DataValidationService.HasMinimumCharacters(FirstName.TrimStart().TrimEnd(), 2)
                || !DataValidationService.IsArabic(FirstName.TrimStart().TrimEnd())
                || DataValidationService.ContainsNumber(FirstName.TrimStart().TrimEnd()))
            {
                EveryThingValid = false;
                FirstNameTextBoxBackground = ThemeSelectorService.SetBrush("Critical");
                if (FirstName.TrimStart() == "")
                {
                    FirstName = "";
                    FirstNameRequiredErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    FirstNameRequiredErrorVisiblity = Visibility.Collapsed;
                }
                if (!DataValidationService.HasMaximumCharacters(FirstName.TrimStart().TrimEnd(), 49))
                {
                    FirstNameTooLongErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    FirstNameTooLongErrorVisiblity = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(FirstName.TrimStart().TrimEnd(), 2)
                    && FirstName.TrimStart() != "")
                {
                    FirstNameTooShortErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    FirstNameTooShortErrorVisiblity = Visibility.Collapsed;
                }

                if (!DataValidationService.IsArabic(FirstName.TrimStart().TrimEnd())
                    && FirstName.TrimStart() != "")
                {
                    FirstNameIsNotArabicErrorVisibility = Visibility.Visible;
                }
                else
                {
                    FirstNameIsNotArabicErrorVisibility = Visibility.Collapsed;
                }

                if (DataValidationService.ContainsNumber(FirstName.TrimStart().TrimEnd())
                    && FirstName.TrimStart() != "")
                {
                    FirstNameContainsNumberErrorVisibility = Visibility.Visible;
                }
                else
                {
                    FirstNameContainsNumberErrorVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                FirstNameTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                FirstName = FirstName.TrimStart().TrimEnd();
                FirstNameRequiredErrorVisiblity = Visibility.Collapsed;
                FirstNameTooLongErrorVisiblity = Visibility.Collapsed;
                FirstNameTooShortErrorVisiblity = Visibility.Collapsed;
                FirstNameIsNotArabicErrorVisibility = Visibility.Collapsed;
                FirstNameContainsNumberErrorVisibility = Visibility.Collapsed;

            }

            if (Gender == "")
            {
                EveryThingValid = false;
                GenderRequiredErrorVisibilty = Visibility.Visible;
            }
            else
            {
                GenderRequiredErrorVisibilty = Visibility.Collapsed;
            }


            if (Accreditation != 0 && Accreditation != 1)
            {
                EveryThingValid = false;
                AccreditationComboBoxBackground = ThemeSelectorService.SetBrush("Critical");
                AccreditationRequiredErrorVisibilty = Visibility.Visible;
            }
            else
            {
                AccreditationComboBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                AccreditationRequiredErrorVisibilty = Visibility.Collapsed;
            }

            if (StartingDate > DateTimeOffset.Now)
            {
                EveryThingValid = false;
                StartingDateDatePickerBackground = ThemeSelectorService.SetBrush("Critical");
                StartingDateErrorVisibilty = Visibility.Visible;
            }
            else
            {
                StartingDateDatePickerBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                StartingDateErrorVisibilty = Visibility.Collapsed;
            }

            if (Email.TrimStart() == ""
                || !DataValidationService.IsValidEmail(Email.TrimStart().TrimStart())
                || emailExistBefor)
            {
                EveryThingValid = false;
                EmailTextBoxBackground = ThemeSelectorService.SetBrush("Critical");
                if (Email.TrimStart() == "")
                {
                    Email = "";
                    EmailRequiredErrorVisibilty = Visibility.Visible;
                }
                else
                {
                    Email = Email.TrimStart().TrimEnd();
                    EmailRequiredErrorVisibilty = Visibility.Collapsed;
                }
                if (!DataValidationService.IsValidEmail(Email.TrimStart().TrimStart()) && Email.TrimStart() != "")
                {
                    EmailIsNotValidErrorVisibilty = Visibility.Visible;
                }
                else
                {
                    EmailIsNotValidErrorVisibilty = Visibility.Collapsed;
                }
                if (emailExistBefor)
                {
                    EmailAllreadyExistsErrorVisibilty = Visibility.Visible;
                }
                else
                {
                    EmailAllreadyExistsErrorVisibilty = Visibility.Collapsed;
                }
            }
            else
            {
                Email = Email.TrimStart().TrimEnd();
                EmailTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                EmailRequiredErrorVisibilty = Visibility.Collapsed;
                EmailIsNotValidErrorVisibilty = Visibility.Collapsed;
                EmailAllreadyExistsErrorVisibilty = Visibility.Collapsed;
            }

            if (Phone.TrimStart() == ""
                || !DataValidationService.IsNumber(Phone)
                || !DataValidationService.HasMaximumCharacters(Phone.Replace(" ", ""), 10)
                || !DataValidationService.HasMinimumCharacters(Phone.Replace(" ", ""), 9)
                || phoneExistBefor)
            {
                EveryThingValid = false;
                PhoneTextBoxBackground = ThemeSelectorService.SetBrush("Critical");

                if (Phone.TrimStart() == "")
                {
                    Phone = "";
                    PhoneRequiredErrorVisibilty = Visibility.Visible;
                }
                else
                {
                    PhoneRequiredErrorVisibilty = Visibility.Collapsed;
                }

                if (!DataValidationService.IsNumber(Phone) && Phone.TrimStart() != "")
                {
                    PhoneIsNotValidErrorVisibilty = Visibility.Visible;
                }
                else
                {
                    PhoneIsNotValidErrorVisibilty = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMaximumCharacters(Phone.Replace(" ", ""), 10))
                {
                    PhoneTooLongErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    PhoneTooLongErrorVisiblity = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(Phone.Replace(" ", ""), 9) && Phone.TrimStart() != "")
                {
                    PhoneTooShortErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    PhoneTooShortErrorVisiblity = Visibility.Collapsed;
                }

                if (phoneExistBefor)
                {
                    PhoneAllreadyExistsErrorVisibilty = Visibility.Visible;
                }
                else
                {
                    PhoneAllreadyExistsErrorVisibilty = Visibility.Collapsed;
                }
            }
            else
            {
                Phone = Phone.Replace(" ", "");
                PhoneTextBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                PhoneRequiredErrorVisibilty = Visibility.Collapsed;
                PhoneIsNotValidErrorVisibilty = Visibility.Collapsed;
                PhoneTooLongErrorVisiblity = Visibility.Collapsed;
                PhoneTooShortErrorVisiblity = Visibility.Collapsed;
                PhoneAllreadyExistsErrorVisibilty = Visibility.Collapsed;
            }

            if (Password == ""
                || !DataValidationService.HasMinimumCharacters(Password, 8)
                || !DataValidationService.HasMaximumCharacters(Password, 128)
                || !Password.Any(char.IsLetter)
                || !DataValidationService.ContainsSymbol(Password)
                || !DataValidationService.ContainsNumber(Password))
            {
                EveryThingValid = false;
                PasswordBoxBackground = ThemeSelectorService.SetBrush("Critical");
                if (Password == "")
                {
                    PasswordRequiredErrorVisibilty = Visibility.Visible;
                }
                else
                {
                    PasswordRequiredErrorVisibilty = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMinimumCharacters(Password, 8) && Password != "")
                {
                    PasswordTooShortErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    PasswordTooShortErrorVisiblity = Visibility.Collapsed;
                }

                if (!DataValidationService.HasMaximumCharacters(Password, 128))
                {
                    PasswordTooLongErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    PasswordTooLongErrorVisiblity = Visibility.Collapsed;
                }

                if (!Password.Any(char.IsLetter) && Password != "")
                {
                    PasswordMustContainAlpabetErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    PasswordMustContainAlpabetErrorVisiblity = Visibility.Collapsed;
                }
                if (!DataValidationService.ContainsSymbol(Password) && Password != "")
                {
                    PasswordMustContainSymbolErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    PasswordMustContainSymbolErrorVisiblity = Visibility.Collapsed;
                }
                if (!DataValidationService.ContainsNumber(Password) && Password != "")
                {
                    PasswordMustContainNumberErrorVisiblity = Visibility.Visible;
                }
                else
                {
                    PasswordMustContainNumberErrorVisiblity = Visibility.Collapsed;
                }
            }
            else
            {
                PasswordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                PasswordRequiredErrorVisibilty = Visibility.Collapsed;
                PasswordTooShortErrorVisiblity = Visibility.Collapsed;
                PasswordTooLongErrorVisiblity = Visibility.Collapsed;
                PasswordMustContainAlpabetErrorVisiblity = Visibility.Collapsed;
                PasswordMustContainSymbolErrorVisiblity = Visibility.Collapsed;
                PasswordMustContainNumberErrorVisiblity = Visibility.Collapsed;
            }

            if (ConfirmPassword == "" || ConfirmPassword != Password)
            {
                EveryThingValid = false;
                ConfirmPasswordBoxBackground = ThemeSelectorService.SetBrush("Critical");
                if (ConfirmPassword == "")
                {
                    ConfirmPasswoedRequiredErrorVisibilty = Visibility.Visible;
                }
                else
                {
                    ConfirmPasswoedRequiredErrorVisibilty = Visibility.Collapsed;
                }
                if (ConfirmPassword != Password && ConfirmPassword != "")
                {
                    ConfirmPasswoedMatchErrorVisibilty = Visibility.Visible;
                }
                else
                {
                    ConfirmPasswoedMatchErrorVisibilty = Visibility.Collapsed;
                }
            }
            else
            {
                ConfirmPasswordBoxBackground = App.Current.Resources["ControlFillColorDefaultBrush"] as Brush;
                ConfirmPasswoedRequiredErrorVisibilty = Visibility.Collapsed;
                ConfirmPasswoedMatchErrorVisibilty = Visibility.Collapsed;
            }
        }
    }
}
