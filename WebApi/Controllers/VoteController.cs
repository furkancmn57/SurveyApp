using Application.Features.Votes.Commands;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVote(CreateVoteCommand command)
        {
            string IpAddress = HttpContext.GetIpAddress();
            command.IpAddress = IpAddress;
            
            await _mediator.Send(command);
            return Ok("Oy Başarıyla Kaydedildi.");
        }
    }
}
