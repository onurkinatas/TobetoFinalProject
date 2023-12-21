using Application.Features.Manufacturers.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Manufacturers.Constants.ManufacturersOperationClaims;

namespace Application.Features.Manufacturers.Queries.GetList;

public class GetListManufacturerQuery : IRequest<GetListResponse<GetListManufacturerListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListManufacturers({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetManufacturers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListManufacturerQueryHandler : IRequestHandler<GetListManufacturerQuery, GetListResponse<GetListManufacturerListItemDto>>
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IMapper _mapper;

        public GetListManufacturerQueryHandler(IManufacturerRepository manufacturerRepository, IMapper mapper)
        {
            _manufacturerRepository = manufacturerRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListManufacturerListItemDto>> Handle(GetListManufacturerQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Manufacturer> manufacturers = await _manufacturerRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListManufacturerListItemDto> response = _mapper.Map<GetListResponse<GetListManufacturerListItemDto>>(manufacturers);
            return response;
        }
    }
}