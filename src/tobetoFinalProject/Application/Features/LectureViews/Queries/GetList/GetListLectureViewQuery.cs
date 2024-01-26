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

namespace Application.Features.LectureViews.Queries.GetList;

public class GetListLectureViewQuery : IRequest<GetListResponse<GetListLectureViewListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLectureViews({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLectureViews";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLectureViewQueryHandler : IRequestHandler<GetListLectureViewQuery, GetListResponse<GetListLectureViewListItemDto>>
    {
        private readonly ILectureViewRepository _lectureViewRepository;
        private readonly IMapper _mapper;

        public GetListLectureViewQueryHandler(ILectureViewRepository lectureViewRepository, IMapper mapper)
        {
            _lectureViewRepository = lectureViewRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLectureViewListItemDto>> Handle(GetListLectureViewQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LectureView> lectureViews = await _lectureViewRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLectureViewListItemDto> response = _mapper.Map<GetListResponse<GetListLectureViewListItemDto>>(lectureViews);
            return response;
        }
    }
}