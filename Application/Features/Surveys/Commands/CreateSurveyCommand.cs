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
        public CreateSurveyCommand(string question, string createdBy, DateTime createdDate, DateTime dueDate, Settings settings, List<Option> options)
        {
            Question = question;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            DueDate = dueDate;
            Settings = settings;
            Options = options;
        }

        public string Question { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public Settings Settings { get; set; }
        public List<Option> Options { get; set; }


        public class Handler : IRequestHandler<CreateSurveyCommand>
        {
            private readonly IRepository<Survey> _repository;

            public Handler(IRepository<Survey> repository)
            {
                _repository = repository;
            }

            public async Task Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
            {
                await _repository.CreateAsync(new Survey
                {
                    Question = request.Question,
                    CreatedBy = request.CreatedBy,
                    CreatedDate = request.CreatedDate,
                    DueDate = request.DueDate,
                    Settings = request.Settings,
                    Options = request.Options
                });
            }
        }
    }
}
