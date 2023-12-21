using Application.Features.ContentLikes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentLikes.Constants.ContentLikesOperationClaims;

namespace Application.Features.ContentLikes.Queries.GetList;

public class GetListContentLikeQuery : IRequest<GetListResponse<GetListContentLikeListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentLikes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentLikes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentLikeQueryHandler : IRequestHandler<GetListContentLikeQuery, GetListResponse<GetListContentLikeListItemDto>>
    {
        private readonly IContentLikeRepository _contentLikeRepository;
        private readonly IMapper _mapper;

        public GetListContentLikeQueryHandler(IContentLikeRepository contentLikeRepository, IMapper mapper)
        {
            _contentLikeRepository = contentLikeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentLikeListItemDto>> Handle(GetListContentLikeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentLike> contentLikes = await _contentLikeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentLikeListItemDto> response = _mapper.Map<GetListResponse<GetListContentLikeListItemDto>>(contentLikes);
            return response;
        }
    }
}