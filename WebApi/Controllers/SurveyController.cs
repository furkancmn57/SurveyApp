using Application.Features.Surveys.Commands;
using Application.Features.Surveys.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Survey.Request;

namespace WebApi.Controllers
{
    [Route("api/surveys")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SurveyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> SurveyList(CancellationToken token)
        {
            var query = new GetSurveyQuery();
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSurveyById(int id,CancellationToken token)
        {
            var query = new GetSurveyByIdQuery(id);
            var result = await _mediator.Send(query,token);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurvey([FromBody] SurveyCreateRequest request, CancellationToken token)
        {
            var options = request.Options.Select(option => new Option
            {
                Description = option.Description,
                Type = option.Type,
                Order = option.Order
            }).ToList();

            var command = new CreateSurveyCommand(request.Question, request.CreatedBy, options, request.Settings);
            await _mediator.Send(command,token);
            return Ok("Anket Başarıyla Oluşturuldu.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSurvey([FromBody] SurveyUpdateRequest request,CancellationToken token)
        {
            var command = new UpdateSurveyCommand(request.Id, request.Question, request.CreatedBy,request.Settings);
            await _mediator.Send(command,token);
            return Ok("Anket Başarıyla Güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSurvey(int id, CancellationToken token)
        {
            var command = new RemoveSurveyCommand(id);
            await _mediator.Send(command, token);
            return Ok("Anket Başarıyla Silindi.");
        }


  
    }
}
