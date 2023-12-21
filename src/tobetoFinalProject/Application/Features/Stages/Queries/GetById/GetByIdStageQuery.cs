using Application.Features.Stages.Constants;
using Application.Features.Stages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Stages.Constants.StagesOperationClaims;

namespace Application.Features.Stages.Queries.GetById;

public class GetByIdStageQuery : IRequest<GetByIdStageResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStageQueryHandler : IRequestHandler<GetByIdStageQuery, GetByIdStageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStageRepository _stageRepository;
        private readonly StageBusinessRules _stageBusinessRules;

        public GetByIdStageQueryHandler(IMapper mapper, IStageRepository stageRepository, StageBusinessRules stageBusinessRules)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _stageBusinessRules = stageBusinessRules;
        }

        public async Task<GetByIdStageResponse> Handle(GetByIdStageQuery request, CancellationToken cancellationToken)
        {
            Stage? stage = await _stageRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _stageBusinessRules.StageShouldExistWhenSelected(stage);

            GetByIdStageResponse response = _mapper.Map<GetByIdStageResponse>(stage);
            return response;
        }
    }
}