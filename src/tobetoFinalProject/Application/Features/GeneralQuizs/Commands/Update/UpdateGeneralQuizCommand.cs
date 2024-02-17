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

namespace Application.Features.GeneralQuizs.Commands.Update;

public class UpdateGeneralQuizCommand : IRequest<UpdatedGeneralQuizResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int QuizId { get; set; }

    public string[] Roles => new[] { Admin, Write, GeneralQuizsOperationClaims.Update };

    public class UpdateGeneralQuizCommandHandler : IRequestHandler<UpdateGeneralQuizCommand, UpdatedGeneralQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGeneralQuizRepository _generalQuizRepository;
        private readonly GeneralQuizBusinessRules _generalQuizBusinessRules;

        public UpdateGeneralQuizCommandHandler(IMapper mapper, IGeneralQuizRepository generalQuizRepository,
                                         GeneralQuizBusinessRules generalQuizBusinessRules)
        {
            _mapper = mapper;
            _generalQuizRepository = generalQuizRepository;
            _generalQuizBusinessRules = generalQuizBusinessRules;
        }

        public async Task<UpdatedGeneralQuizResponse> Handle(UpdateGeneralQuizCommand request, CancellationToken cancellationToken)
        {
            GeneralQuiz? generalQuiz = await _generalQuizRepository.GetAsync(predicate: gq => gq.Id == request.Id, cancellationToken: cancellationToken);
            await _generalQuizBusinessRules.GeneralQuizShouldExistWhenSelected(generalQuiz);
            generalQuiz = _mapper.Map(request, generalQuiz);

            await _generalQuizRepository.UpdateAsync(generalQuiz!);

            UpdatedGeneralQuizResponse response = _mapper.Map<UpdatedGeneralQuizResponse>(generalQuiz);
            return response;
        }
    }
}