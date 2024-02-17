using Application.Features.GeneralQuizs.Constants;
using Application.Features.GeneralQuizs.Constants;
using Application.Features.GeneralQuizs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.GeneralQuizs.Constants.GeneralQuizsOperationClaims;

namespace Application.Features.GeneralQuizs.Commands.Delete;

public class DeleteGeneralQuizCommand : IRequest<DeletedGeneralQuizResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, GeneralQuizsOperationClaims.Delete };

    public class DeleteGeneralQuizCommandHandler : IRequestHandler<DeleteGeneralQuizCommand, DeletedGeneralQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGeneralQuizRepository _generalQuizRepository;
        private readonly GeneralQuizBusinessRules _generalQuizBusinessRules;

        public DeleteGeneralQuizCommandHandler(IMapper mapper, IGeneralQuizRepository generalQuizRepository,
                                         GeneralQuizBusinessRules generalQuizBusinessRules)
        {
            _mapper = mapper;
            _generalQuizRepository = generalQuizRepository;
            _generalQuizBusinessRules = generalQuizBusinessRules;
        }

        public async Task<DeletedGeneralQuizResponse> Handle(DeleteGeneralQuizCommand request, CancellationToken cancellationToken)
        {
            GeneralQuiz? generalQuiz = await _generalQuizRepository.GetAsync(predicate: gq => gq.Id == request.Id, cancellationToken: cancellationToken);
            await _generalQuizBusinessRules.GeneralQuizShouldExistWhenSelected(generalQuiz);

            await _generalQuizRepository.DeleteAsync(generalQuiz!);

            DeletedGeneralQuizResponse response = _mapper.Map<DeletedGeneralQuizResponse>(generalQuiz);
            return response;
        }
    }
}