using Application.Features.ContentTags.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentTags.Constants.ContentTagsOperationClaims;

namespace Application.Features.ContentTags.Queries.GetList;

public class GetListContentTagQuery : IRequest<GetListResponse<GetListContentTagListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentTags({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentTags";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentTagQueryHandler : IRequestHandler<GetListContentTagQuery, GetListResponse<GetListContentTagListItemDto>>
    {
        private readonly IContentTagRepository _contentTagRepository;
        private readonly IMapper _mapper;

        public GetListContentTagQueryHandler(IContentTagRepository contentTagRepository, IMapper mapper)
        {
            _contentTagRepository = contentTagRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentTagListItemDto>> Handle(GetListContentTagQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentTag> contentTags = await _contentTagRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentTagListItemDto> response = _mapper.Map<GetListResponse<GetListContentTagListItemDto>>(contentTags);
            return response;
        }
    }
}