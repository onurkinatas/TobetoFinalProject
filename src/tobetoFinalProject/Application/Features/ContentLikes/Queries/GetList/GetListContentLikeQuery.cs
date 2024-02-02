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
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ContentLikes.Queries.GetList;

public class GetListContentLikeQuery : IRequest<GetListResponse<GetListContentLikeListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin };

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
                include: cl => cl.Include(cl => cl.Student)
                .ThenInclude(s => s.User)
                .Include(cl => cl.Content),
                predicate: cl => cl.IsLiked == true,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentLikeListItemDto> response = _mapper.Map<GetListResponse<GetListContentLikeListItemDto>>(contentLikes);
            return response;
        }
    }
}