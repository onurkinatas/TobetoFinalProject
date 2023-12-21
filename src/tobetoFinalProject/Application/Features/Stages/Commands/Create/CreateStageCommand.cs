using Application.Features.Stages.Constants;
using Application.Features.Stages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Stages.Constants.StagesOperationClaims;

namespace Application.Features.Stages.Commands.Create;

public class CreateStageCommand : IRequest<CreatedStageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Description { get; set; }

    public string[] Roles => new[] { Admin, Write, StagesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStages";

    public class CreateStageCommandHandler : IRequestHandler<CreateStageCommand, CreatedStageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStageRepository _stageRepository;
        private readonly StageBusinessRules _stageBusinessRules;

        public CreateStageCommandHandler(IMapper mapper, IStageRepository stageRepository,
                                         StageBusinessRules stageBusinessRules)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _stageBusinessRules = stageBusinessRules;
        }

        public async Task<CreatedStageResponse> Handle(CreateStageCommand request, CancellationToken cancellationToken)
        {
            Stage stage = _mapper.Map<Stage>(request);

            await _stageRepository.AddAsync(stage);

            CreatedStageResponse response = _mapper.Map<CreatedStageResponse>(stage);
            return response;
        }
    }
}