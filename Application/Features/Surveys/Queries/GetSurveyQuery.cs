using Application.Features.Surveys.Dtos;
using Application.Features.Surveys.Model;
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
    public class GetSurveyQuery : IRequest<List<GetSurveyQueryModel>>
    {
        public class Handler : IRequestHandler<GetSurveyQuery, List<GetSurveyQueryModel>>
        {
            private readonly ISurveyDbContext _context;

            public Handler(ISurveyDbContext context)
            {
                _context = context;
            }

            public async Task<List<GetSurveyQueryModel>> Handle(GetSurveyQuery request, CancellationToken cancellationToken)
            {

                var values = await _context.Surveys
                    .Include(x => x.Options)
                    .ThenInclude(x => x.Votes)
                    .ToListAsync();


                var result = values.Select(value => new GetSurveyQueryModel
                {
                    Id = value.Id,
                    Question = value.Question,
                    CreatedBy = value.CreatedBy,
                    Options = value.Options.Select(option => new OptionDto
                    {
                        Id = option.Id,
                        Description = option.Description,
                        Type = option.Type,
                        Order = option.Order,
                        Votes = option.Votes.Select(vote => new VoteDto
                        {
                            Id = vote.Id,
                            User = vote.User
                        }).ToList()
                    }).ToList(),
                    CreatedDate = value.CreatedDate,
                    DueDate = value.DueDate,
                    Settings = value.Settings
                }).ToList();

                return result;
            }
        }
    }
}
