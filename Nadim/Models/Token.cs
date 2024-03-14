using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Models
{
    public class Token
    {
        public string tokenValue { get; }
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
    }

}
