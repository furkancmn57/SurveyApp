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

namespace Application.Features.Votes.Commands
{
    public class UseVoteCommand : IRequest
    {
        public UseVoteCommand(int surveyId, string user, string ipAddress,List<int> optionIds)
        {
            SurveyId = surveyId;
            User = user;
            OptionIds = optionIds;
            IpAddress = ipAddress;
        }

        public int SurveyId { get; set; }
        public string User { get; set; }
        public string IpAddress { get; set; }
        public List<int> OptionIds { get; set; }

        public class Handler : IRequestHandler<UseVoteCommand>
        {
            private readonly ISurveyDbContext _context;

            public Handler(ISurveyDbContext context)
            {
                _context = context;
            }

            public async Task Handle(UseVoteCommand request, CancellationToken cancellationToken)
            {
                var survey = await _context.Surveys.Where(x => x.Id == request.SurveyId).FirstOrDefaultAsync(cancellationToken);

                if (survey is null)
                {
                    throw new BusinessException("Oy kullanmak istediğiniz anket bulunamadı.",404);
                }

                var options = await _context.Options.Where(x => request.OptionIds.Contains(x.Id)).ToListAsync(cancellationToken);

                RunRule(request, options, survey);

                var vote = Vote.Create(request.User, request.IpAddress, options, survey);

                await _context.Votes.AddAsync(vote, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            private void RunRule(UseVoteCommand request, List<Option> options, Survey survey)
            {
                if (request.OptionIds.Count != options.Count)
                {
                    throw new BusinessException("Oy kullandığınız seçeneklerden bir kaçı bulunamadı. Lütfen tekrar deneyiniz",400);
                }

                if (!survey.Settings.MultipleChoice && request.OptionIds.Count > 1)
                {
                    throw new BusinessException("Çoklu oy kullanımı kapalıdır.Lütfen tek oy kullanınız",400);
                }

                if (survey.Settings.MinChoice > request.OptionIds.Count)
                {
                    throw new BusinessException($"Minimum {survey.Settings.MinChoice} adet seçim yapmalısınız.",400);
                }

                if (survey.Settings.MaxChoice < request.OptionIds.Count)
                {
                    throw new BusinessException($"Maximum {survey.Settings.MinChoice} adet seçim yapmalısınız.",400);
                }

                if (survey.Settings.IpLimit == true)
                {
                    if (_context.Votes.Any(v => v.IpAddress == request.IpAddress))
                    {
                        throw new BusinessException("Bu Anket için oy kullanmışsınız.",400);
                    }
                }
                if (survey.DueDate < DateTime.Now)
                {
                    throw new BusinessException("Anketin Süresi Dolmuş.", 400);
                }
            }
        }
    }
}
