using Application.Exceptions;
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
    public class RemoveSurveyCommand : IRequest
    {
        public RemoveSurveyCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public class Handler : IRequestHandler<RemoveSurveyCommand>
        {
            private readonly ISurveyDbContext _context;

            public Handler(ISurveyDbContext context)
            {
                _context = context;
            }

            public async Task Handle(RemoveSurveyCommand request, CancellationToken cancellationToken)
            {
                var value = await _context.Surveys.FindAsync(request.Id);

                if (value == null)
                {
                    throw new BusinessException("Anket Bulunamadı.",404);
                }

                _context.Surveys.Remove(value);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

    }
}
