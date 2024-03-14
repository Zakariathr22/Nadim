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
        public DateTime? birthDate { get; set; }
        public string gender { get; set; }
        public byte[] profilePic { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string passwordHash { get; set; }
        public string salt { get; set; }
        public Office office { get; set; }
    }
}
