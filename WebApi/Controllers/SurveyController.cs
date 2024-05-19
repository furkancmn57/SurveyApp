using Application.Features.Surveys.Commands;
using Application.Features.Surveys.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SurveyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> SurveyList()
        {
            var query = new GetSurveyQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSurveyById(int id)
        {
            var query = new GetSurveyByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurvey(CreateSurveyCommand command)
        {
            await _mediator.Send(command);
            return Ok("Anket Başarıyla Oluşturuldu.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSurvey(UpdateSurveyCommand command)
        {
            await _mediator.Send(command);
            return Ok("Anket Başarıyla Güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSurvey(int id)
        {
            var command = new RemoveSurveyCommand(id);
            await _mediator.Send(command);
            return Ok("Anket Başarıyla Silindi.");
        }


  
    }
}
