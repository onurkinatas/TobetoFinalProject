using Application.Features.Pools.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Pools.Constants.PoolsOperationClaims;

namespace Application.Features.Pools.Queries.GetList;

public class GetListPoolQuery : IRequest<GetListResponse<GetListPoolListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListPoolQueryHandler : IRequestHandler<GetListPoolQuery, GetListResponse<GetListPoolListItemDto>>
    {
        private readonly IPoolRepository _poolRepository;
        private readonly IMapper _mapper;

        public GetListPoolQueryHandler(IPoolRepository poolRepository, IMapper mapper)
        {
            _poolRepository = poolRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPoolListItemDto>> Handle(GetListPoolQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Pool> pools = await _poolRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPoolListItemDto> response = _mapper.Map<GetListResponse<GetListPoolListItemDto>>(pools);
            return response;
        }
    }
}