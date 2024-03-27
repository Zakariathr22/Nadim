using CommunityToolkit.Mvvm.ComponentModel;
using Nadim.Models;
using Nadim.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Nadim.ViewModels
{
    public partial class SignUpViewModel : ObservableObject
    {
        [ObservableProperty] private Lawyer lawyer;
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
            this.Lawyer.salt = GenerateSalt();
            this.Lawyer.passwordHash = HashPassword(SignUpWindow.signUpLawyerInfoViewModel.Password + Lawyer.salt);

            if (SignUpWindow.signUpLawyerInfoViewModel.Accreditation == 0)
                this.Lawyer.accreditation = "لدى المحكمة العليا ومجلس الدولة";
            else if(SignUpWindow.signUpLawyerInfoViewModel.Accreditation == 1)
                this.Lawyer.accreditation = "لدى المجلس القضائي";
            this.Lawyer.startingDate = SignUpWindow.signUpLawyerInfoViewModel.StartingDate;

            this.Lawyer.office.naming = SignUpWindow.signUpOfficeInfoViewModel.Naming;
            this.Lawyer.office.headquarters = SignUpWindow.signUpOfficeInfoViewModel.Headquarters;
            this.Lawyer.office.wilaya = "بوسعادة";
            this.lawyer.office.municipality = "";
            this.lawyer.office.isCompany = SignUpWindow.signUpOfficeInfoViewModel.IsCompany;
            this.lawyer.office.phone1 = SignUpWindow.signUpOfficeInfoViewModel.Phone1;
            this.lawyer.office.phone2 = SignUpWindow.signUpOfficeInfoViewModel.Phone2;
            this.lawyer.office.email = SignUpWindow.signUpOfficeInfoViewModel.Email;
            this.lawyer.office.fax = SignUpWindow.signUpOfficeInfoViewModel.Fax;
        }

        private string GenerateSalt()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            return new string(Enumerable.Repeat(chars, 100)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void SignUp()
        {

        }
    }
}
