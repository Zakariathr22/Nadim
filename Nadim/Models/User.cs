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
        public DateTimeOffset? birthDate { get; set; }
        public string gender { get; set; }
        public byte[] profilePic { get; set; }
        public string email { get; set; }
        public bool? emailVerified { get; set; }
        public string phone { get; set; }
        public bool? phoneVerified { get; set; }
        public string salt { get; set; }
        public string passwordHash { get; set; }
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
