using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Settings
    {
        public int MinChoice { get; set; }
        public int MaxChoice { get; set; }
        public bool MultipleChoice { get; set; }
        public bool IpLimit { get; set; }
    }
}
