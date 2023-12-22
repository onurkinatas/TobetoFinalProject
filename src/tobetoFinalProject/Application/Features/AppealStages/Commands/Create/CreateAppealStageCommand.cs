using Application.Features.AppealStages.Constants;
using Application.Features.AppealStages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.AppealStages.Constants.AppealStagesOperationClaims;

namespace Application.Features.AppealStages.Commands.Create;

public class CreateAppealStageCommand : IRequest<CreatedAppealStageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid AppealId { get; set; }
    public Guid StageId { get; set; }
    public string[] Roles => new[] { Admin, Write, AppealStagesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAppealStages";

    public class CreateAppealStageCommandHandler : IRequestHandler<CreateAppealStageCommand, CreatedAppealStageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppealStageRepository _appealStageRepository;
        private readonly AppealStageBusinessRules _appealStageBusinessRules;

        public CreateAppealStageCommandHandler(IMapper mapper, IAppealStageRepository appealStageRepository,
                                         AppealStageBusinessRules appealStageBusinessRules)
        {
            _mapper = mapper;
            _appealStageRepository = appealStageRepository;
            _appealStageBusinessRules = appealStageBusinessRules;
        }

        public async Task<CreatedAppealStageResponse> Handle(CreateAppealStageCommand request, CancellationToken cancellationToken)
        {
            AppealStage appealStage = _mapper.Map<AppealStage>(request);

            await _appealStageRepository.AddAsync(appealStage);

            CreatedAppealStageResponse response = _mapper.Map<CreatedAppealStageResponse>(appealStage);
            return response;
        }
    }
}