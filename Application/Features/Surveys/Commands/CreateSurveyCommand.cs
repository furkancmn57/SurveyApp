using Application.Features.Surveys.Dtos;
using Application.Features.Surveys.Rules;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Surveys.Commands
{
    public class CreateSurveyCommand : IRequest
    {
        public CreateSurveyCommand(string question, string createdBy, List<CreateOptionDto> options, Settings settings)
        {
            Question = question;
            CreatedBy = createdBy;
            Options = options;
            Settings = settings;
        }

        public string Question { get; set; }
        public string CreatedBy { get; set; }
        public List<CreateOptionDto> Options { get; set; }
        public Settings Settings { get; set; }

        public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand>
        {
            private readonly IRepository<Survey> _repository;
            private readonly SurveyBusinessClass _surveyBusiness;

            public CreateSurveyCommandHandler(IRepository<Survey> repository, SurveyBusinessClass surveyBusiness)
            {
                _repository = repository;
                _surveyBusiness = surveyBusiness;
            }

            public async Task Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
            {
                var options = request.Options.Select(option => new Option
                {
                    Description = option.Description,
                    Type = option.Type,
                    Order = option.Order
                }).ToList();

                _surveyBusiness.AddOption(options.Count);

                await _repository.CreateAsync(new Survey
                {
                    Question = request.Question,
                    CreatedBy = request.CreatedBy,
                    Options = options,
                    CreatedDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(1),
                    Settings = request.Settings,
                });
            }
        }

    }
}
