using Application.Features.AppealStages.Constants;
using Application.Features.AppealStages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.AppealStages.Constants.AppealStagesOperationClaims;

namespace Application.Features.AppealStages.Queries.GetById;

public class GetByIdAppealStageQuery : IRequest<GetByIdAppealStageResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdAppealStageQueryHandler : IRequestHandler<GetByIdAppealStageQuery, GetByIdAppealStageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppealStageRepository _appealStageRepository;
        private readonly AppealStageBusinessRules _appealStageBusinessRules;

        public GetByIdAppealStageQueryHandler(IMapper mapper, IAppealStageRepository appealStageRepository, AppealStageBusinessRules appealStageBusinessRules)
        {
            _mapper = mapper;
            _appealStageRepository = appealStageRepository;
            _appealStageBusinessRules = appealStageBusinessRules;
        }

        public async Task<GetByIdAppealStageResponse> Handle(GetByIdAppealStageQuery request, CancellationToken cancellationToken)
        {
            AppealStage? appealStage = await _appealStageRepository.GetAsync(predicate: asd => asd.Id == request.Id, cancellationToken: cancellationToken);
            await _appealStageBusinessRules.AppealStageShouldExistWhenSelected(appealStage);

            GetByIdAppealStageResponse response = _mapper.Map<GetByIdAppealStageResponse>(appealStage);
            return response;
        }
    }
}