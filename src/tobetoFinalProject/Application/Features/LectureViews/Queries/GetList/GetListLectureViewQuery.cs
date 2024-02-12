using Application.Features.LectureViews.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.LectureViews.Constants.LectureViewsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.ClassLectures;

namespace Application.Features.LectureViews.Queries.GetList;

public class GetListLectureViewQuery : IRequest<GetListResponse<GetListLectureViewListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLectureViews({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLectureViews";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLectureViewQueryHandler : IRequestHandler<GetListLectureViewQuery, GetListResponse<GetListLectureViewListItemDto>>
    {
        private readonly ILectureViewRepository _lectureViewRepository;
        private readonly IMapper _mapper;
        private readonly IClassLecturesService _classLecturesService;

        public GetListLectureViewQueryHandler(ILectureViewRepository lectureViewRepository, IMapper mapper, IClassLecturesService classLecturesService)
        {
            _lectureViewRepository = lectureViewRepository;
            _mapper = mapper;
            _classLecturesService = classLecturesService;
        }

        public async Task<GetListResponse<GetListLectureViewListItemDto>> Handle(GetListLectureViewQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LectureView> lectureViews = await _lectureViewRepository.GetListAsync(
                include: lv => lv.Include(lv => lv.Student)
                .ThenInclude(s => s.User)
                .Include(lv => lv.Content)
                .Include(lv => lv.Lecture)
                ,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );
            int allcontentCount = await _classLecturesService.GetAllContentCountForActiveStudent();

            GetListResponse<GetListLectureViewListItemDto> response = _mapper.Map<GetListResponse<GetListLectureViewListItemDto>>(lectureViews);
            return response;
        }
    }
}