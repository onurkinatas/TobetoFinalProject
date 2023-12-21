using Application.Features.Contents.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Contents.Constants.ContentsOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Contents.Queries.GetList;

public class GetListContentQuery : IRequest<GetListResponse<GetListContentListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContents({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContents";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentQueryHandler : IRequestHandler<GetListContentQuery, GetListResponse<GetListContentListItemDto>>
    {
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        public GetListContentQueryHandler(IContentRepository contentRepository, IMapper mapper)
        {
            _contentRepository = contentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentListItemDto>> Handle(GetListContentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Content> contents = await _contentRepository.GetListAsync(
                include: c => c.Include(c => c.Manufacturer)
                    .Include(c => c.ContentCategory)
                    .Include(c => c.Language)
                    .Include(c => c.SubType)
                    .Include(c => c.ContentInstructors)
                    .ThenInclude(c => c.Instructor),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentListItemDto> response = _mapper.Map<GetListResponse<GetListContentListItemDto>>(contents);
            return response;
        }
    }
}