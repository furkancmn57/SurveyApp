using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vote
    {
        public Vote() 
        { 
            // only ef db operations
        }
        public Vote(string user, string ipAddress, List<Option> options, Survey survey)
        {
            User = user;
            IpAddress = ipAddress;
            Options = options;
            Survey = survey;
            SurveyId = survey.Id;
        }
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string User { get; set; }
        public string IpAddress { get; set; }
        public virtual List<Option> Options { get; set; }
        public virtual Survey Survey { get; set; }

        public static Vote Create(string user, string ipAddress, List<Option> options, Survey survey)
        {
            return new Vote(user, ipAddress, options, survey);
        }
    }
}
