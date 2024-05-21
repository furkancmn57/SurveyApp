using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Survey
    {
        public Survey() 
        {
            // only db operations
        }
        private Survey(string question, string createdBy, Settings settings, List<Option> options)
        {
            if (string.IsNullOrWhiteSpace(createdBy))
            {
                createdBy = "Admin";
            }
            Question = question;
            CreatedBy = createdBy;
            Settings = settings;
            Options = options;
            CreatedDate = DateTime.Now;
            DueDate = DateTime.Now.AddDays(1);
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public Settings Settings { get; set; }
        public List<Option> Options { get; set; }
        public virtual List<Vote> Votes { get; set; }

        public static Survey Create(string question, string createdBy, Settings settings, List<Option> options)
        {
            return new Survey(question, createdBy, settings, options);
        }
        public Survey Update(string question, string createdBy, Settings settings)
        {
            Question = question;
            CreatedBy = createdBy;
            Settings = settings;

            return this;
        }
    }
}
