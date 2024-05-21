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
    public class CreateSurveyCommand : IRequest<Survey>
    {
        public CreateSurveyCommand(string question, string createdBy, List<Option> options, Settings settings)
        {
            Question = question;
            CreatedBy = createdBy;
            Options = options;
            Settings = settings;
        }

        public string Question { get; set; }
        public string CreatedBy { get; set; }
        public List<Option> Options { get; set; }
        public Settings Settings { get; set; }

        public class Handler : IRequestHandler<CreateSurveyCommand, Survey>
        {
            private readonly ISurveyDbContext _context;

            public Handler(ISurveyDbContext context)
            {
                _context = context;
            }

            public async Task<Survey> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
            {
                var survey = Survey.Create(request.Question, request.CreatedBy, request.Settings, request.Options);
                _context.Surveys.Add(survey);
                await _context.SaveChangesAsync(cancellationToken);

                return survey;
            }
        }

    }
}
