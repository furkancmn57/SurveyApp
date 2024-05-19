using Application.Features.Surveys.Dtos;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Surveys.Commands
{
    public class UpdateSurveyCommand : IRequest
    {
        public UpdateSurveyCommand(int id, string question, string createdBy, List<OptionDto> options, Settings settings)
        {
            Id = id;
            Question = question;
            CreatedBy = createdBy;
            Options = options;
            Settings = settings;
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public string CreatedBy { get; set; }
        public List<OptionDto> Options { get; set; }
        public Settings Settings { get; set; }

        public class handler : IRequestHandler<UpdateSurveyCommand>
        {
            private readonly IRepository<Survey> _repository;

            public handler(IRepository<Survey> repository)
            {
                _repository = repository;
            }

            public async Task Handle(UpdateSurveyCommand request, CancellationToken cancellationToken)
            {
                var values = await _repository.GetByIdAsync(request.Id);

                if (values == null)
                {
                    throw new Exception("Survey not found");
                }

                var options = request.Options.Select(option => new Option
                {
                    Description = option.Description,
                    Type = option.Type,
                    Order = option.Order
                }).ToList();

                values.Question = request.Question;
                values.CreatedBy = request.CreatedBy;
                values.Options = options;
                values.Settings = request.Settings;
                await _repository.UpdateAsync(values);
            }
        }
    }
}
