using Application.Features.ContentLikes.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContentLikes.Queries.GetListByContentId;
public class GetListByContentIdContentLikeQuery : IRequest<GetListResponse<GetListContentLikeListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid ContentId { get; set; }

    public string[] Roles => new[] { "Admin" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentLikes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentLikes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByContentIdContentLikeQueryHandler : IRequestHandler<GetListByContentIdContentLikeQuery, GetListResponse<GetListContentLikeListItemDto>>
    {
        private readonly IContentLikeRepository _contentLikeRepository;
        private readonly IMapper _mapper;

        public GetListByContentIdContentLikeQueryHandler(IContentLikeRepository contentLikeRepository, IMapper mapper)
        {
            _contentLikeRepository = contentLikeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentLikeListItemDto>> Handle(GetListByContentIdContentLikeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentLike> contentLikes = await _contentLikeRepository.GetListAsync(
                include: cl => cl.Include(cl => cl.Student)
                .ThenInclude(s => s.User)
                .Include(cl => cl.Content),
                predicate: cl => cl.IsLiked == true &&cl.ContentId==request.ContentId,
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

