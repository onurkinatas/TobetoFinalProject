using Application.Features.AppealStages.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.AppealStages.Constants.AppealStagesOperationClaims;

namespace Application.Features.AppealStages.Queries.GetList;

public class GetListAppealStageQuery : IRequest<GetListResponse<GetListAppealStageListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListAppealStages({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetAppealStages";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAppealStageQueryHandler : IRequestHandler<GetListAppealStageQuery, GetListResponse<GetListAppealStageListItemDto>>
    {
        private readonly IAppealStageRepository _appealStageRepository;
        private readonly IMapper _mapper;

        public GetListAppealStageQueryHandler(IAppealStageRepository appealStageRepository, IMapper mapper)
        {
            _appealStageRepository = appealStageRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAppealStageListItemDto>> Handle(GetListAppealStageQuery request, CancellationToken cancellationToken)
        {
            IPaginate<AppealStage> appealStages = await _appealStageRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAppealStageListItemDto> response = _mapper.Map<GetListResponse<GetListAppealStageListItemDto>>(appealStages);
            return response;
        }
    }
}