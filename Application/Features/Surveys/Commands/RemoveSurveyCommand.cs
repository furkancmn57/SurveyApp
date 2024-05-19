using Application.Features.Surveys.Dtos;
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

        public class RemoveSurveyCommandHandler : IRequestHandler<RemoveSurveyCommand>
        {
            private readonly IRepository<Survey> _repository;

            public RemoveSurveyCommandHandler(IRepository<Survey> repository)
            {
                _repository = repository;
            }

            public async Task Handle(RemoveSurveyCommand request, CancellationToken cancellationToken)
            {
                var value = await _repository.GetByIdAsync(request.Id);

                if (value == null)
                {
                    throw new Exception("Survey not found");
                }

                await _repository.DeleteAsync(value);
            }
        }

    }
}
