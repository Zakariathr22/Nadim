using System;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;

namespace Nadim.Services
{
    public class EmailVerificationService
    {
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        public static int GenerateRandomOTP()
        {
            byte[] otpBytes = new byte[4]; // Use 4 bytes (32 bits) for the OTP
            Rng.GetBytes(otpBytes);

            // Convert the bytes to an unsigned integer
            uint otpValue = BitConverter.ToUInt32(otpBytes, 0);

            // Calculate the 6-digit OTP
            int otp = (int)(otpValue % 900000) + 100000;
            return otp;
        }

        public static void SendAccountCreationEmailOTP(string email, int otp)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("nadim.dz.lawyer@gmail.com", "rpwv rnor tldj ofiy");

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("nadim@lawyer"),
                    Subject = "رمز التحقق لإنشاء حساب نديم",
                    Body = $"<!DOCTYPE html>\r\n" +
                    $"<html lang=\"ar\" dir=\"rtl\">\r\n" +
                    $"<head>\r\n" +
                    $"<meta charset=\"UTF-8\">\r\n" +
                    $"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n" +
                    $"<title>رمز التحقق لإنشاء حساب نديم</title>\r\n" +
                    $"<style>\r\n    body {{\r\n      font-family: Arial, sans-serif;\r\n      direction: rtl;\r\n    }}\r\n  </style>\r\n" +
                    $"</head>\r\n<body>\r\n" +
                    $"<p>مرحبا!</p>\r\n" +
                    $"<p>لإكمال إجراءات التأكيد تجدون أدناه رمز التحقق الخاص بكم:</p>\r\n" +
                    $"<p style=\"font-weight: bold;\">{otp.ToString()}</p>\r\n" +
                    $"<p>إذا لم تطلبوا رمز التحقق هذا، يمكنكم تجاهل هذا البريد الإلكتروني.</p>\r\n" +
                    $"<p>شكرا لك،</p>\r\n" +
                    $"<p>نديم</p>\r\n" +
                    $"</body>\r\n" +
                    $"</html>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                smtpClient.Send(mailMessage);
                Console.WriteLine("OTP sent successfully!");
            }
        }

        public static bool VerifyOTP(string userInput, int expectedOTP)
        {
            return int.TryParse(userInput, out int userOTP) && userOTP == expectedOTP;
        }
    }
}
