using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Models
{
    public class Lawyer : User
    {
        public string accreditation { get; set; }
        public DateTime? startingDate { get; set; }
        public User creator { get; set; }
    }
}
