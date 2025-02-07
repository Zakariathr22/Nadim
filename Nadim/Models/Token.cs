﻿using Nadim.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Nadim.Models
{
    public class Token
    {
        public User user { get; }
        public string tokenValue { get; set; }
        public DateTime? expirationDate { get; set; }
        public bool? isActive { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? lastUsedAt { get; set; }
        public string ipAddress { get; set; }
        public string userAgent { get; set; }
        public string purpose { get; set; }
        public string machineName { get; set; }
        public string additionalInfo { get; set; }

        public Token(PasswordCredential tokenCredential)
        {
            this.tokenValue = tokenCredential.Password;
            try
            {
                ipAddress = MachineInfoService.GetExternalIpAddress().ToString();
            }
            catch
            {
                ipAddress = "";
            }
            userAgent = "Windows";
            machineName = MachineInfoService.GetComputerName();
        }
        public Token(string EmailOrPhone, string Password, string salt)
        {
            user = new User();
            user.loger = EmailOrPhone.TrimStart().TrimEnd();
            user.passwordHash = CryptographyService.HashPassword(Password + salt);
            try
            {
                ipAddress = MachineInfoService.GetExternalIpAddress().ToString();
            }
            catch 
            {
                ipAddress = "";
            }
            userAgent = "Windows";
            machineName = MachineInfoService.GetComputerName();
        }

        public Token(string EmailOrPassword)
        {
            user = new User();
            user.loger = EmailOrPassword.TrimStart().TrimEnd();
            try
            {
                ipAddress = MachineInfoService.GetExternalIpAddress().ToString();
            }
            catch
            {
                ipAddress = "";
            }
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
