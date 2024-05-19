using Application.Features.Surveys.Dtos;
using Application.Features.Surveys.Model;
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
    public class GetSurveyByIdQuery : IRequest<GetSurveyByIdQueryModel>
    {
        public int Id { get; set; }

        public GetSurveyByIdQuery(int id)
        {
            Id = id;
        }

        public class handler : IRequestHandler<GetSurveyByIdQuery, GetSurveyByIdQueryModel>
        {
            private readonly ISurveyDbContext _context;

            public handler(ISurveyDbContext context)
            {
                _context = context;
            }

            public async Task<GetSurveyByIdQueryModel> Handle(GetSurveyByIdQuery request, CancellationToken cancellationToken)
            {
                var value = await _context.Surveys
                    .Include(x => x.Options)
                     .ThenInclude(x => x.Votes)
                     .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (value == null)
                {
                    throw new Exception("Survey not found");
                }

                return new GetSurveyByIdQueryModel
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
                    Settings = value.Settings,
                };
            }
        }
    }
}
