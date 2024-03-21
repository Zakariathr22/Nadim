using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nadim.Services
{
    public static class DataValidationService
    {
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

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            try
            {
                MailAddress address = new MailAddress(email);
                return true;
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
            if (email == "") return false;

            string sql = "CALL `CountUserByEmail`(@email);";
            object countObj = App.dataAccess.ExecuteScalar(sql, new MySqlParameter("@email", email));
            Int64 count = (Int64)countObj;
            return count > 0;
        }

        public static bool DoesPhoneExist(string phone)
        {
            if (phone == "") return false;

            string sql = "CALL `CountUserByPhone`(@phone);";
            object countObj = App.dataAccess.ExecuteScalar(sql, new MySqlParameter("@phone", phone));
            Int64 count = (Int64)countObj;
            return count > 0;
        }

    }
}
