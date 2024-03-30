using Nadim.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Models
{
    public class Token
    {
        public User user { get; }
        public DateTime? expirationDate { get; set; }
        public bool? isActive { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? lastUsedAt { get; set; }
        public string ipAddress { get; set; }
        public string userAgent { get; set; }
        public string purpose { get; set; }
        public string machineName { get; set; }
        public string additionalInfo { get; set; }

        public Token(string EmailOrPhone, string Password, string salt)
        {
            user = new User();
            user.loger = EmailOrPhone.TrimStart().TrimEnd();
            user.passwordHash = CryptographyService.HashPassword(Password + salt);
            ipAddress = MachineInfoService.GetLocalIPAddress();
            userAgent = "Windows";
            machineName = MachineInfoService.GetComputerName();
        }

        public void Clear()
        {
            user.clear();
            expirationDate = null;
            isActive = null;
            createdAt = null;
            lastUsedAt = null;
            ipAddress = null; userAgent = null;
            purpose = null;
            machineName = null;
            additionalInfo = null;
        }
    }
}
