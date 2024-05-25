using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Models
{
    public class OfficeActivation
    {
        public DateTimeOffset activationDate { get; set; }
        public DateTimeOffset expiryDate { get; set; }
        public decimal paymentAmount { get; set; }
        public DateTimeOffset paymentDate { get; set; }
        public string paymentMethod { get; set; }
        public string paymentStatus { get; set; }
        public DateTimeOffset? createdAt { get; set; }
        public Office office { get; set; }
        public User user { get; set; }

    }

}