using Application.Features.Votes.Commands;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Vote.Request;

namespace WebApi.Controllers
{
    [Route("api/votes")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{surveyId}")]
        public async Task<IActionResult> UseVote([FromRoute] int surveyId, VoteCreateRequest request, CancellationToken token)
        {
            string IpAddress = HttpContext.GetIpAddress();

            var command = new UseVoteCommand(surveyId, request.User, IpAddress, request.OptionsIds);

            await _mediator.Send(command,token);
            return Ok("Oy Başarıyla Kaydedildi.");
        }
    }
}
