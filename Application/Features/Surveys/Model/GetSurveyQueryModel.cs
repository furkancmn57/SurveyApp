using Application.Features.Surveys.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Surveys.Model
{
    public class GetSurveyQueryModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string CreatedBy { get; set; }
        public List<OptionDto> Options { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public Settings Settings { get; set; }

    }
}
