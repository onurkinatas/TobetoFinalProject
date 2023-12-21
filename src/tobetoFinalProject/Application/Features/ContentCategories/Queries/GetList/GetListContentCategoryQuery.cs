using Application.Features.ContentCategories.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentCategories.Constants.ContentCategoriesOperationClaims;

namespace Application.Features.ContentCategories.Queries.GetList;

public class GetListContentCategoryQuery : IRequest<GetListResponse<GetListContentCategoryListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentCategories({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentCategories";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentCategoryQueryHandler : IRequestHandler<GetListContentCategoryQuery, GetListResponse<GetListContentCategoryListItemDto>>
    {
        private readonly IContentCategoryRepository _contentCategoryRepository;
        private readonly IMapper _mapper;

        public GetListContentCategoryQueryHandler(IContentCategoryRepository contentCategoryRepository, IMapper mapper)
        {
            _contentCategoryRepository = contentCategoryRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentCategoryListItemDto>> Handle(GetListContentCategoryQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentCategory> contentCategories = await _contentCategoryRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentCategoryListItemDto> response = _mapper.Map<GetListResponse<GetListContentCategoryListItemDto>>(contentCategories);
            return response;
        }
    }
}