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
            return text.Any(char.IsDigit);
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

        public static bool HasMaximumCharacters(string text, int minimumLength)
        {
            return text.Length <= minimumLength;
        }

        public static bool ContainsWhitespace(string text)
        {
            return Regex.IsMatch(text, @"\s");
        }
 
        public static string RemoveLastWhitespace(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            int length = text.Length;
            if (char.IsWhiteSpace(text[length - 1]))
            {
                return text.Substring(0, length - 1);
            }

            return text;
        }
    }
}
