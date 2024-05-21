using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Surveys.Queries
{
    public class GetSurveyQuery : IRequest<List<Survey>>
    {
        public class Handler : IRequestHandler<GetSurveyQuery, List<Survey>>
        {
            private readonly ISurveyDbContext _context;

            public Handler(ISurveyDbContext context)
            {
                _context = context;
            }

            public async Task<List<Survey>> Handle(GetSurveyQuery request, CancellationToken cancellationToken)
            {
                var result = await _context.Surveys.Include(i => i.Options).ThenInclude(i => i.Votes).ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
