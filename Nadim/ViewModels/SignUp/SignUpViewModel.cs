using CommunityToolkit.Mvvm.ComponentModel;
using Nadim.Models;
using Nadim.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Windows.Networking;
using CommunityToolkit.Mvvm.Input;
using Nadim.Services;

namespace Nadim.ViewModels
{
    public partial class SignUpViewModel : ObservableObject
    {
        [ObservableProperty] private Lawyer lawyer = new Lawyer();
        public SignUpViewModel()
        {
            this.Lawyer.firstName = SignUpWindow.signUpLawyerInfoViewModel.FirstName;
            this.Lawyer.lastName = SignUpWindow.signUpLawyerInfoViewModel.LastName;
            this.Lawyer.birthDate = SignUpWindow.signUpLawyerInfoViewModel.BirthDate;
            this.Lawyer.gender = SignUpWindow.signUpLawyerInfoViewModel.Gender;
            this.Lawyer.email = SignUpWindow.signUpLawyerInfoViewModel.Email;
            this.Lawyer.emailVerified = SignUpWindow.signUpEmailVerViewModel.EmailCodeIsValid;
            this.Lawyer.phone = SignUpWindow.signUpLawyerInfoViewModel.Phone;
            this.Lawyer.phoneVerified = SignUpWindow.signUpPhoneVerViewModel.PhoneCodeIsValid;
            this.Lawyer.salt = CryptographyService.GenerateSalt();
            this.Lawyer.passwordHash = CryptographyService.HashPassword(SignUpWindow.signUpLawyerInfoViewModel.Password + Lawyer.salt);

            if (SignUpWindow.signUpLawyerInfoViewModel.Accreditation == 0)
                this.Lawyer.accreditation = "لدى المحكمة العليا ومجلس الدولة";
            else if(SignUpWindow.signUpLawyerInfoViewModel.Accreditation == 1)
                this.Lawyer.accreditation = "لدى المجلس القضائي";
            this.Lawyer.startingDate = SignUpWindow.signUpLawyerInfoViewModel.StartingDate;
            this.Lawyer.office = new Office();
            this.Lawyer.office.naming = SignUpWindow.signUpOfficeInfoViewModel.Naming;
            this.Lawyer.office.headquarters = SignUpWindow.signUpOfficeInfoViewModel.Headquarters;
            this.Lawyer.office.wilaya = "بوسعادة";
            this.lawyer.office.municipality = "";
            this.lawyer.office.accreditation = this.lawyer.accreditation;
            this.lawyer.office.isCompany = SignUpWindow.signUpOfficeInfoViewModel.IsCompany;
            this.lawyer.office.phone1 = SignUpWindow.signUpOfficeInfoViewModel.Phone1;
            this.lawyer.office.phone2 = SignUpWindow.signUpOfficeInfoViewModel.Phone2;
            this.lawyer.office.email = SignUpWindow.signUpOfficeInfoViewModel.Email;
            this.lawyer.office.fax = SignUpWindow.signUpOfficeInfoViewModel.Fax;
        }

        [RelayCommand]
        private void LawyerSignUp()
        {
            if (!App.dataAccess.ConnectionStatIsOpened()) App.dataAccess.OpenConnection();
            string query = "CALL LawyerSignUp(@p_firstname, @p_lastname, @p_birthdate, @p_gender, @p_email, @p_emailVerified, @p_phone, @p_phoneVerified, @p_salt, @p_passwordHash, @p_accreditation, @p_starting_date, @p_naming, @p_wilaya, @p_municipality, @p_headquarters, @p_phone1, @p_phone2, @p_fax, @p_office_email, @p_isCompany)";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@p_firstname", this.Lawyer.firstName),
                new MySqlParameter("@p_lastname", this.Lawyer.lastName),
                new MySqlParameter("@p_birthdate", this.Lawyer.birthDate),
                new MySqlParameter("@p_gender", this.Lawyer.gender),
                new MySqlParameter("@p_email", this.Lawyer.email),
                new MySqlParameter("@p_emailVerified", this.Lawyer.emailVerified),
                new MySqlParameter("@p_phone", this.Lawyer.phone),
                new MySqlParameter("@p_phoneVerified", this.Lawyer.phoneVerified),
                new MySqlParameter("@p_salt", this.Lawyer.salt),
                new MySqlParameter("@p_passwordHash", this.Lawyer.passwordHash),
                new MySqlParameter("@p_accreditation", this.Lawyer.accreditation),
                new MySqlParameter("@p_starting_date", this.Lawyer.startingDate),
                new MySqlParameter("@p_naming", this.Lawyer.office.naming),
                new MySqlParameter("@p_wilaya", this.Lawyer.office.wilaya),
                new MySqlParameter("@p_municipality", this.Lawyer.office.municipality),
                new MySqlParameter("@p_headquarters", this.Lawyer.office.headquarters),
                new MySqlParameter("@p_phone1", this.Lawyer.office.phone1),
                new MySqlParameter("@p_phone2", this.Lawyer.office.phone2),
                new MySqlParameter("@p_fax", this.Lawyer.office.fax),
                new MySqlParameter("@p_office_email", this.Lawyer.office.email),
                new MySqlParameter("@p_isCompany", this.Lawyer.office.isCompany)                
            };
            App.dataAccess.ExecuteNonQuery(query, parameters);

            Lawyer.clear();
            Lawyer.ClearRest();
            Lawyer = null;
        }
    }
}
