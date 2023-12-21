using Application.Features.AppealStages.Constants;
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

namespace Application.Features.AppealStages.Commands.Delete;

public class DeleteAppealStageCommand : IRequest<DeletedAppealStageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, AppealStagesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAppealStages";

    public class DeleteAppealStageCommandHandler : IRequestHandler<DeleteAppealStageCommand, DeletedAppealStageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppealStageRepository _appealStageRepository;
        private readonly AppealStageBusinessRules _appealStageBusinessRules;

        public DeleteAppealStageCommandHandler(IMapper mapper, IAppealStageRepository appealStageRepository,
                                         AppealStageBusinessRules appealStageBusinessRules)
        {
            _mapper = mapper;
            _appealStageRepository = appealStageRepository;
            _appealStageBusinessRules = appealStageBusinessRules;
        }

        public async Task<DeletedAppealStageResponse> Handle(DeleteAppealStageCommand request, CancellationToken cancellationToken)
        {
            AppealStage? appealStage = await _appealStageRepository.GetAsync(predicate: asd => asd.Id == request.Id, cancellationToken: cancellationToken);
            await _appealStageBusinessRules.AppealStageShouldExistWhenSelected(appealStage);

            await _appealStageRepository.DeleteAsync(appealStage!);

            DeletedAppealStageResponse response = _mapper.Map<DeletedAppealStageResponse>(appealStage);
            return response;
        }
    }
}