using Application.Features.SubTypes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.SubTypes.Constants.SubTypesOperationClaims;

namespace Application.Features.SubTypes.Queries.GetList;

public class GetListSubTypeQuery : IRequest<GetListResponse<GetListSubTypeListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListSubTypes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetSubTypes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListSubTypeQueryHandler : IRequestHandler<GetListSubTypeQuery, GetListResponse<GetListSubTypeListItemDto>>
    {
        private readonly ISubTypeRepository _subTypeRepository;
        private readonly IMapper _mapper;

        public GetListSubTypeQueryHandler(ISubTypeRepository subTypeRepository, IMapper mapper)
        {
            _subTypeRepository = subTypeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSubTypeListItemDto>> Handle(GetListSubTypeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<SubType> subTypes = await _subTypeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSubTypeListItemDto> response = _mapper.Map<GetListResponse<GetListSubTypeListItemDto>>(subTypes);
            return response;
        }
    }
}