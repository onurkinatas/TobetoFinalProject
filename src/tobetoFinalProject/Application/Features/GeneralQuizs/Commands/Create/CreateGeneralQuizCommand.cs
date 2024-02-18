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
using Core.Application.Pipelines.Caching;

namespace Application.Features.GeneralQuizs.Commands.Create;

public class CreateGeneralQuizCommand : IRequest<CreatedGeneralQuizResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest,ICacheRemoverRequest
{
    public int QuizId { get; set; }

    public string[] Roles => new[] { Admin, Write, GeneralQuizsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetGeneralQuizs";

    public class CreateGeneralQuizCommandHandler : IRequestHandler<CreateGeneralQuizCommand, CreatedGeneralQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGeneralQuizRepository _generalQuizRepository;
        private readonly GeneralQuizBusinessRules _generalQuizBusinessRules;

        public CreateGeneralQuizCommandHandler(IMapper mapper, IGeneralQuizRepository generalQuizRepository,
                                         GeneralQuizBusinessRules generalQuizBusinessRules)
        {
            _mapper = mapper;
            _generalQuizRepository = generalQuizRepository;
            _generalQuizBusinessRules = generalQuizBusinessRules;
        }

        public async Task<CreatedGeneralQuizResponse> Handle(CreateGeneralQuizCommand request, CancellationToken cancellationToken)
        {
            GeneralQuiz generalQuiz = _mapper.Map<GeneralQuiz>(request);

            await _generalQuizRepository.AddAsync(generalQuiz);

            CreatedGeneralQuizResponse response = _mapper.Map<CreatedGeneralQuizResponse>(generalQuiz);
            return response;
        }
    }
}