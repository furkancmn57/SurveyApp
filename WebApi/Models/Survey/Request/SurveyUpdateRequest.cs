using Domain.Entities;
using WebApi.Models.Option.Request;

namespace WebApi.Models.Survey.Request
{
    public class SurveyUpdateRequest
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string CreatedBy { get; set; }
        public Settings Settings { get; set; }
    }
}
