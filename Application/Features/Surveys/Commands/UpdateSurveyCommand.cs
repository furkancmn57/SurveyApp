using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Surveys.Commands
{
    public class UpdateSurveyCommand : IRequest<Survey>
    {
        public UpdateSurveyCommand(int id, string question, string createdBy, Settings settings)
        {
            Id = id;
            Question = question;
            CreatedBy = createdBy;
            Settings = settings;
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public string CreatedBy { get; set; }
        public Settings Settings { get; set; }

        public class Handler : IRequestHandler<UpdateSurveyCommand, Survey>
        {
            private readonly ISurveyDbContext _context;

            public Handler(ISurveyDbContext context)
            {
                _context = context;
            }

            public async Task<Survey> Handle(UpdateSurveyCommand request, CancellationToken cancellationToken)
            {
                var survey = await _context.Surveys.FindAsync(request.Id);

                if (survey is null) 
                {
                    throw new BusinessException("Anket Bulunamadı.",404);
                }

                survey.Update(request.Question, request.CreatedBy, request.Settings);

                _context.Surveys.Update(survey);
                await _context.SaveChangesAsync(cancellationToken);

                return survey;
            }
        }
    }
}
