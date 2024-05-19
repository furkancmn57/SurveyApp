using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Surveys.Rules
{
    public class SurveyBusinessClass 
    {

        public bool CheckIfSurveyIsClosed(DateTime DueDate)
        {
            return DueDate < DateTime.Now;
        }

        public bool CheckIfSurveyIsOpen(DateTime DueDate)
        {
            return DueDate > DateTime.Now;
        }

        public void AddOption(int number)
        {
           if (number < 2)
            {
                throw new Exception("You must have at least two options");
            }
        }

    }
}
