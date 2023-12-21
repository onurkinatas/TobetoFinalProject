using Application.Features.LectureCourses.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.LectureCourses.Constants.LectureCoursesOperationClaims;

namespace Application.Features.LectureCourses.Queries.GetList;

public class GetListLectureCourseQuery : IRequest<GetListResponse<GetListLectureCourseListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLectureCourses({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLectureCourses";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLectureCourseQueryHandler : IRequestHandler<GetListLectureCourseQuery, GetListResponse<GetListLectureCourseListItemDto>>
    {
        private readonly ILectureCourseRepository _lectureCourseRepository;
        private readonly IMapper _mapper;

        public GetListLectureCourseQueryHandler(ILectureCourseRepository lectureCourseRepository, IMapper mapper)
        {
            _lectureCourseRepository = lectureCourseRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLectureCourseListItemDto>> Handle(GetListLectureCourseQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LectureCourse> lectureCourses = await _lectureCourseRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLectureCourseListItemDto> response = _mapper.Map<GetListResponse<GetListLectureCourseListItemDto>>(lectureCourses);
            return response;
        }
    }
}