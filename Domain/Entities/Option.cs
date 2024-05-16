using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual List<Vote> Votes { get; set; }
    }
}
