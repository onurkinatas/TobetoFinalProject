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

namespace Application.Features.AppealStages.Commands.Update;

public class UpdateAppealStageCommand : IRequest<UpdatedAppealStageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid AppealId { get; set; }
    public Guid StageId { get; set; }
    public Appeal? Appeal { get; set; }
    public Stage? Stage { get; set; }

    public string[] Roles => new[] { Admin, Write, AppealStagesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAppealStages";

    public class UpdateAppealStageCommandHandler : IRequestHandler<UpdateAppealStageCommand, UpdatedAppealStageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppealStageRepository _appealStageRepository;
        private readonly AppealStageBusinessRules _appealStageBusinessRules;

        public UpdateAppealStageCommandHandler(IMapper mapper, IAppealStageRepository appealStageRepository,
                                         AppealStageBusinessRules appealStageBusinessRules)
        {
            _mapper = mapper;
            _appealStageRepository = appealStageRepository;
            _appealStageBusinessRules = appealStageBusinessRules;
        }

        public async Task<UpdatedAppealStageResponse> Handle(UpdateAppealStageCommand request, CancellationToken cancellationToken)
        {
            AppealStage? appealStage = await _appealStageRepository.GetAsync(predicate: asd => asd.Id == request.Id, cancellationToken: cancellationToken);
            await _appealStageBusinessRules.AppealStageShouldExistWhenSelected(appealStage);
            appealStage = _mapper.Map(request, appealStage);

            await _appealStageRepository.UpdateAsync(appealStage!);

            UpdatedAppealStageResponse response = _mapper.Map<UpdatedAppealStageResponse>(appealStage);
            return response;
        }
    }
}