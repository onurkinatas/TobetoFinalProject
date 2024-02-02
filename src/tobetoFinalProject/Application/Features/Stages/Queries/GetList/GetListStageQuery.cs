using Application.Features.Stages.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Stages.Constants.StagesOperationClaims;

namespace Application.Features.Stages.Queries.GetList;

public class GetListStageQuery : IRequest<GetListResponse<GetListStageListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStages({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStages";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStageQueryHandler : IRequestHandler<GetListStageQuery, GetListResponse<GetListStageListItemDto>>
    {
        private readonly IStageRepository _stageRepository;
        private readonly IMapper _mapper;

        public GetListStageQueryHandler(IStageRepository stageRepository, IMapper mapper)
        {
            _stageRepository = stageRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStageListItemDto>> Handle(GetListStageQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Stage> stages = await _stageRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStageListItemDto> response = _mapper.Map<GetListResponse<GetListStageListItemDto>>(stages);
            return response;
        }
    }
}