using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Models
{
    internal class Clerk
    {
        public User creator { get; set; }
        public bool blocked { get; set; }
        public User blocker { get; set; }
    }
}
