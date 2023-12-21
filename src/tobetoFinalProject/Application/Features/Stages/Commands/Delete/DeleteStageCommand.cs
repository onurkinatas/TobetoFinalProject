using Application.Features.Stages.Constants;
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
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Stages.Commands.Delete;

public class DeleteStageCommand : IRequest<DeletedStageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StagesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStages";

    public class DeleteStageCommandHandler : IRequestHandler<DeleteStageCommand, DeletedStageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStageRepository _stageRepository;
        private readonly StageBusinessRules _stageBusinessRules;

        public DeleteStageCommandHandler(IMapper mapper, IStageRepository stageRepository,
                                         StageBusinessRules stageBusinessRules)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _stageBusinessRules = stageBusinessRules;
        }

        public async Task<DeletedStageResponse> Handle(DeleteStageCommand request, CancellationToken cancellationToken)
        {
            Stage? stage = await _stageRepository.GetAsync(
                predicate: s => s.Id == request.Id,
                include: s => s.Include(s => s.AppealStages)
                    .Include(s => s.StudentStages),
                cancellationToken: cancellationToken);
            await _stageBusinessRules.StageShouldExistWhenSelected(stage);

            await _stageRepository.DeleteAsync(stage!);

            DeletedStageResponse response = _mapper.Map<DeletedStageResponse>(stage);
            return response;
        }
    }
}