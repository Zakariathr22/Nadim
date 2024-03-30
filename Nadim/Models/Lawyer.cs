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
        public DateTimeOffset? startingDate { get; set; }
        public User creator { get; set; }

        public void ClearRest()
        {
            accreditation = null;
            startingDate = null;
            creator.clear();
            creator = null;
        }
    }
}
