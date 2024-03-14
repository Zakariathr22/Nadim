using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Models
{
    public class OfficeActivation
    {
        public int id { get; set; }
        public DateTime activationDate { get; set; }
        public DateTime expiryDate { get; set; }
        public decimal? paymentAmount { get; set; }
        public DateTime? paymentDate { get; set; }
        public string paymentMethod { get; set; }
        public string paymentStatus { get; set; }
        public Office office { get; set; }
        public User user { get; set; }
    }

}