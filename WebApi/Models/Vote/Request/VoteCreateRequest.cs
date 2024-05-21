using Application.Features.Votes.Commands;

namespace WebApi.Models.Vote.Request
{
    public class VoteCreateRequest
    {
        public string User { get; set; }
        public List<int> OptionsIds { get; set; }
    }
}
