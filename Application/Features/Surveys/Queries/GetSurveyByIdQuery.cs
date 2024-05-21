using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Surveys.Queries
{
    public class GetSurveyByIdQuery : IRequest<Survey>
    {
        public int Id { get; set; }

        public GetSurveyByIdQuery(int id)
        {
            Id = id;
        }

        public class Handler : IRequestHandler<GetSurveyByIdQuery, Survey>
        {
            private readonly ISurveyDbContext _context;

            public Handler(ISurveyDbContext context)
            {
                _context = context;
            }

            public async Task<Survey> Handle(GetSurveyByIdQuery request, CancellationToken cancellationToken)
            {
                var dbQuery = _context.Surveys.AsQueryable();
                var survey = await dbQuery
                    .Include(i => i.Options)
                    .ThenInclude(i => i.Votes)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (survey is null)
                {
                    throw new BusinessException("Anket Bulunamadı.", 404);
                }

                return survey;
            }
        }
    }
}
