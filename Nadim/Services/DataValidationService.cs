using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nadim.Services
{
    public static class DataValidationService
    {
        public static List<string> existedEmails = new List<string>();
        public static List<string> existedPhones = new List<string>();
        
        public static bool IsArabic(string text)
        {
            // Use a regular expression to match Arabic characters efficiently
            return Regex.IsMatch(text, @"\p{IsArabic}+");
        }

        public static bool ContainsNumber(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            return text.Any(c => Char.IsDigit(c));
        }

        public static bool IsWebHost(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            // Split the input by the dot (.) to separate domain and TLD
            var parts = input.Split('.');

            // Check if there are at least two parts (domain and TLD)
            if (parts.Length < 2)
            {
                return false;
            }

            // Consider common TLDs (can be extended for more)
            var commonTlds = new string[] { "com", "net", "org", "co.uk", "io",
                                                "info", "biz", "edu", "gov", "mil", "int",
                                                "ac", "ad", "ae", "af", "ag", "ai", "al",
                                                "am", "ao", "aq", "ar", "as", "at", "co", "dz"
                                                };

            // Check if the last part is a known TLD
            return commonTlds.Contains(parts.Last());
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || email.Length >= 200)
            {
                return false;
            }

            try
            {
                MailAddress address = new MailAddress(email);
                return IsWebHost(address.Host) == true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsNumber(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            // Remove all whitespace characters
            string withoutSpaces = text.Replace(" ", "");

            // Check if all characters are digits (0-9)
            return withoutSpaces.All(char.IsDigit);
        }

        public static bool HasMinimumCharacters(string text, int minimumLength)
        {
            return text.Length >= minimumLength;
        }

        public static bool HasMaximumCharacters(string text, int maximumLength)
        {
            return text.Length <= maximumLength;
        }

        public static bool ContainsWhitespace(string text)
        {
            return Regex.IsMatch(text, @"\s");
        }

        public static bool ContainsSymbol(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false; // Empty or null strings don't contain symbols
            }

            foreach (char c in text)
            {
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool DoesEmailExist(string email)
        {
            for (int i=0; i < existedEmails.Count; i++)
                if (existedEmails[i] == email) return true;            
            
            if (email == "") return false;

            if (!App.dataAccess.ConnectionStatIsOpened()) App.dataAccess.OpenConnection();

            string sql = "CALL `CountUserByEmail`(@email);";
            object countObj = App.dataAccess.ExecuteScalar(sql, new MySqlParameter("@email", email));
            Int64 count = (Int64)countObj;
            if (count > 0)
            {
                existedEmails.Add(email);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DoesPhoneExist(string phone)
        {
            for (int i = 0; i < existedPhones.Count; i++)
                if (existedPhones[i] == phone) return true;

            if (phone == "") return false;

            if (!App.dataAccess.ConnectionStatIsOpened()) App.dataAccess.OpenConnection();

            string sql = "CALL `CountUserByPhone`(@phone);";
            object countObj = App.dataAccess.ExecuteScalar(sql, new MySqlParameter("@phone", phone));

            Int64 count = (Int64)countObj;
            if (count > 0)
            {
                existedPhones.Add(phone);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}