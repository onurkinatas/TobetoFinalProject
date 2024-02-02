using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.CourseContents.Queries.GetList;

public class GetListCourseContentQuery : IRequest<GetListResponse<GetListCourseContentListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }
    public string CacheKey => $"GetListCourseContents({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetCourseContents";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCourseContentQueryHandler : IRequestHandler<GetListCourseContentQuery, GetListResponse<GetListCourseContentListItemDto>>
    {
        private readonly ICourseContentRepository _courseContentRepository;
        private readonly IMapper _mapper;

        public GetListCourseContentQueryHandler(ICourseContentRepository courseContentRepository, IMapper mapper)
        {
            _courseContentRepository = courseContentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCourseContentListItemDto>> Handle(GetListCourseContentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<CourseContent> courseContents = await _courseContentRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCourseContentListItemDto> response = _mapper.Map<GetListResponse<GetListCourseContentListItemDto>>(courseContents);
            return response;
        }
    }
}