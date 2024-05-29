using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Models
{
    public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName
        {
            get
            {
                return $"{lastName} {firstName}";
            }
        }

        public string profitionalFullName
        {
            get
            {
                if (gender == "أنثى")
                {
                    return $"الأستاذة {lastName} {firstName}";
                }
                return $"الأستاذ {lastName} {firstName}";
            }
        }

        public DateTimeOffset? birthDate { get; set; }
        public string birthDateString
        {
            get
            {
                return $"{birthDate:d MMM yyyy}";
            }
        }
        public string gender { get; set; }
        public byte[] profilePic { get; set; }
        public string email { get; set; }
        public bool? emailVerified { get; set; }
        public string phone { get; set; }
        public bool? phoneVerified { get; set; }
        public string salt { get; set; }
        public string passwordHash { get; set; }
        public bool isUserCreatedPassword { get; set; }
        public DateTimeOffset createdAt { get; set; }
        public string createdAtString
        {
            get
            {
                return $"{createdAt:F}";
            }
        }
        public DateTimeOffset lastUpdate { get; set; }
        public string lastUpdateString
        {
            get
            {
                return $"{lastUpdate:F}";
            }
        }
        public bool isDeleted { get; set; }
        public DateTimeOffset deletedAt { get; set; }
        public string loger { get; set; }
        public Office office { get; set; }

        public void clear()
        {
            firstName = null;
            lastName = null;
            birthDate = null;
            gender = null;
            profilePic = null;
            email = null;
            emailVerified = null;
            phone = null;
            phoneVerified = null;
            salt = null;
            passwordHash = null;
            loger = null;
            if(office != null)
            office.clear();
        }
    }
}
