using System;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Windows.Security.Credentials;

namespace Nadim.Services
{
    public class EmailVerificationService
    {
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
        public static PasswordVault vault = new PasswordVault();
        public static int GenerateRandomOTP()
        {
            byte[] otpBytes = new byte[4]; // Use 4 bytes (32 bits) for the OTP
            Rng.GetBytes(otpBytes);

            // Convert the bytes to an unsigned integer
            uint otpValue = BitConverter.ToUInt32(otpBytes, 0);

            // Calculate the 8-digit OTP
            int otp = (int)(otpValue % 90000000) + 10000000;
            return otp;
        }

        public static void SendAccountCreationEmailOTP(string email, PasswordCredential credential)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.mailersend.net"))
            {
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(ConfigurationService.GetCoGetConnectionString("SMTP_USER"), ConfigurationService.GetCoGetConnectionString("SMTP_PASS"));

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(ConfigurationService.GetCoGetConnectionString("SMTP_USER")),
                    Subject = "رمز التحقق لإنشاء حساب نديم",
                    Body = $"<!DOCTYPE html>" +
                    $"<html>" +
                    $"<head>" +
                    $"    <title>رمز التحقق</title>" +
                    $"</head>" +
                    $"<body dir=\"rtl\" style=\"font-family: Segoe UI; background-color: #E5E5E5; padding: 20px;\">" +
                    $"    <div" +
                    $"        style=\"max-width: 600px; margin: auto; background-color: white; padding: 20px; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1);\">" +
                    $"        <h2 style=\"text-align: center; color: #0078D4;\">مرحبا بكم في برنامج نديم</h2>" +
                    $"        <p>عزيزي المستخدم</p>" +
                    $"        <p>شكرا لكم على إنشاء حساب لدى نديم، يرجى التحقق من عنوان بريدك الإلكتروني عن طريق إدخال رمز التحقق التالي:</p>" +
                    $"        <h3 style=\"text-align: center; color: #0078D4;\">{credential.Password}</h3>" +
                    $"        <p>إذا لم تطلبوا هذا الرمز، فيرجى تجاهل هذا البريد الإلكتروني.</p>" +
                    $"        <p>أطيب التحيات،</p>" +
                    $"        <p>فريق عمل نديم</p>" +
                    $"    </div>" +
                    $"</body>" +
                    $"</html>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                smtpClient.Send(mailMessage);
                Console.WriteLine("OTP sent successfully!");
            }
        }

        public static void SendAccountRecoveryCode(string email, PasswordCredential credential)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.mailersend.net"))
            {
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("MS_kv2WNQ@trial-yzkq340kpn2gd796.mlsender.net", "OOVVA0OrhASOzhd6");

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("MS_kv2WNQ@trial-yzkq340kpn2gd796.mlsender.net"),
                    Subject = "رمز الأمان لإعادة تعيين كلمة المرور",
                    Body = $"<!DOCTYPE html>" +
                    $"<html>" +
                    $"<head>" +
                    $"    <title>رمز الأمان</title>" +
                    $"</head>" +
                    $"<body dir=\"rtl\" style=\"font-family: Segoe UI; background-color: #E5E5E5; padding: 20px;\">" +
                    $"    <div" +
                    $"        style=\"max-width: 600px; margin: auto; background-color: white; padding: 20px; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1);\">" +
                    $"        <h2 style=\"text-align: center; color: #0078D4;\">مرحبا بكم في برنامج نديم</h2>" +
                    $"        <p>عزيزي المستخدم</p>" +
                    $"        <p>لقد تلقينا طلبا لإسترجاع حساب نديم وإعادة تعيين كلمة المرور، يرجى التأكيد عبر إدخال رمز الأمان التالي:</p>" +
                    $"        <h3 style=\"text-align: center; color: #0078D4;\">{credential.Password}</h3>" +
                    $"        <p>إذا لم تطلبوا هذا الرمز، فيرجى الإتصال بفريق الدعم عبر أحد الأرقام التالية:</p>" +
                    $"        <ul dir=\"ltr\">" +
                    $"             <li>+2136--------</li>" +
                    $"             <li>+2135--------</li>" +
                    $"             <li>+2137--------</li>" +
                    $"        </ul>" +
                    $"        <p>أطيب التحيات،</p>" +
                    $"        <p>فريق عمل نديم</p>" +
                    $"    </div>" +
                    $"</body>" +
                    $"</html>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                smtpClient.Send(mailMessage);
                Console.WriteLine("OTP sent successfully!");
            }
        }

        public static void SendPasswordResetNotification(string email)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.mailersend.net"))
            {
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(ConfigurationService.GetCoGetConnectionString("SMTP_USER"), ConfigurationService.GetCoGetConnectionString("SMTP_PASS"));

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(ConfigurationService.GetCoGetConnectionString("SMTP_USER")),
                    Subject = "تم إعادة تعيين كلمة المرور بنجاح",
                    Body = $"<!DOCTYPE html>" +
                    $"<html>" +
                    $"<head>" +
                    $"    <title>تم إعادة تعيين كلمة المرور بنجاح</title>" +
                    $"</head>" +
                    $"<body dir=\"rtl\" style=\"font-family: Segoe UI; background-color: #E5E5E5; padding: 20px;\">" +
                    $"    <div" +
                    $"        style=\"max-width: 600px; margin: auto; background-color: white; padding: 20px; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1);\">" +
                    $"        <h2 style=\"text-align: center; color: #0078D4;\">مرحبا بكم في برنامج نديم</h2>" +
                    $"        <p>عزيزي المستخدم</p>" +
                    $"        <p>نود أن نعلمك أنه تم إعادة تعيين كلمة المرور الخاصة بك بنجاح. يرجى التأكد من تخزين كلمة المرور الجديدة في مكان آمن.</p>" +
                    $"        <p>إذا لم تقم بطلب إعادة تعيين كلمة المرور، يرجى الاتصال بنا على الفور:</p>" +
                    $"        <ul dir=\"ltr\">" +
                    $"             <li>+2136--------</li>" +
                    $"             <li>+2135--------</li>" +
                    $"             <li>+2137--------</li>" +
                    $"        </ul>" +
                    $"        <p>أطيب التحيات،</p>" +
                    $"        <p>فريق عمل نديم</p>" +
                    $"    </div>" +
                    $"</body>" +
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
