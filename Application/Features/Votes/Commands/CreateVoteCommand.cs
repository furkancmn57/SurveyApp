using Application.Features.Surveys.Dtos;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Votes.Commands
{
    public class CreateVoteCommand : IRequest
    {
        public CreateVoteCommand(int surveyId, string user, List<int> optionIds)
        {
            SurveyId = surveyId;
            User = user;
            OptionIds = optionIds;
        }

        public int SurveyId { get; set; }
        public string User { get; set; }
        public string IpAddress { get; set; }
        public List<int> OptionIds { get; set; }

        public class Handler : IRequestHandler<CreateVoteCommand>
        {
            private readonly ISurveyDbContext _context;

            public Handler(ISurveyDbContext context)
            {
                _context = context;
            }

            public async Task Handle(CreateVoteCommand request, CancellationToken cancellationToken)
            {
                var options = await _context.Options.Where(x => request.OptionIds.Contains(x.Id)).ToListAsync();
                var survey = await _context.Surveys.FindAsync(request.SurveyId);

                var vote = new Vote
                {
                    Survey = survey,
                    User = request.User,
                    Options = options,
                    IpAddress = request.IpAddress
                };

                if (options.Count != request.OptionIds.Count)
                {
                    throw new Exception("Invalid option");
                }
                if (survey.Settings.IpLimit == true)
                {
                    if (_context.Votes.Any(v => v.IpAddress == vote.IpAddress))
                    {
                        throw new Exception("You have already voted");
                    }
                }
                if (survey.DueDate < DateTime.Now)
                {
                    throw new Exception("Survey is expired");
                }


                await _context.Votes.AddAsync(vote);
                await _context.SaveChangesAsync();
            }
        }
    }
}
