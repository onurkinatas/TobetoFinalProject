using Application.Features.Appeals.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Appeals.Constants.AppealsOperationClaims;

namespace Application.Features.Appeals.Queries.GetList;

public class GetListAppealQuery : IRequest<GetListResponse<GetListAppealListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListAppeals({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetAppeals";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAppealQueryHandler : IRequestHandler<GetListAppealQuery, GetListResponse<GetListAppealListItemDto>>
    {
        private readonly IAppealRepository _appealRepository;
        private readonly IMapper _mapper;

        public GetListAppealQueryHandler(IAppealRepository appealRepository, IMapper mapper)
        {
            _appealRepository = appealRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAppealListItemDto>> Handle(GetListAppealQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Appeal> appeals = await _appealRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAppealListItemDto> response = _mapper.Map<GetListResponse<GetListAppealListItemDto>>(appeals);
            return response;
        }
    }
}