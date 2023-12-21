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

namespace Application.Features.Stages.Commands.Update;

public class UpdateStageCommand : IRequest<UpdatedStageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Description { get; set; }

    public string[] Roles => new[] { Admin, Write, StagesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStages";

    public class UpdateStageCommandHandler : IRequestHandler<UpdateStageCommand, UpdatedStageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStageRepository _stageRepository;
        private readonly StageBusinessRules _stageBusinessRules;

        public UpdateStageCommandHandler(IMapper mapper, IStageRepository stageRepository,
                                         StageBusinessRules stageBusinessRules)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _stageBusinessRules = stageBusinessRules;
        }

        public async Task<UpdatedStageResponse> Handle(UpdateStageCommand request, CancellationToken cancellationToken)
        {
            Stage? stage = await _stageRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _stageBusinessRules.StageShouldExistWhenSelected(stage);
            stage = _mapper.Map(request, stage);

            await _stageRepository.UpdateAsync(stage!);

            UpdatedStageResponse response = _mapper.Map<UpdatedStageResponse>(stage);
            return response;
        }
    }
}