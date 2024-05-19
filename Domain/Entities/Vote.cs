using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string User { get; set; }
        public string IpAddress { get; set; }
        public virtual List<Option> Options { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
