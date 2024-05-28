using Nadim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;

namespace Nadim.Models
{
    public class Lawyer : User
    {
        public string accreditation { get; set; }
        public string accreditationString 
        { 
            get
            {
                if (gender == "أنثى")
                    return $"محامية معتمدة {accreditation}";
                else return $"محامي معتمد {accreditation}";
            } 
        }
        public DateTimeOffset? startingDate { get; set; }
        public string startingDateString
        {
            get
            {
                return $"{startingDate: MMM yyyy}";
            }
        }
        public User creator { get; set; }

        public void ClearRest()
        {
            accreditation = null;
            startingDate = null;
            if(creator != null) creator.clear();
            creator = null;
        }

        public Lawyer()
        {

        }

        public Lawyer(User user) 
        {
            firstName = user.firstName;
            lastName = user.lastName;
            birthDate = user.birthDate;
            gender = user.gender;

        }
    }
}

//public string gender { get; set; }
//public byte[] profilePic { get; set; }
//public string email { get; set; }
//public bool? emailVerified { get; set; }
//public string phone { get; set; }
//public bool? phoneVerified { get; set; }
//public string salt { get; set; }
//public string passwordHash { get; set; }
//public bool isUserCreatedPassword { get; set; }
//public DateTimeOffset createdAt { get; set; }
//public DateTimeOffset lastUpdate { get; set; }
//public bool isDeleted { get; set; }
//public DateTimeOffset deletedAt { get; set; }
//public string loger { get; set; }
//public Office office { get; set; }