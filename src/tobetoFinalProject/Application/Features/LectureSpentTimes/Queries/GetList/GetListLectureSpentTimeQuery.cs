using Application.Features.LectureSpentTimes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.LectureSpentTimes.Constants.LectureSpentTimesOperationClaims;

namespace Application.Features.LectureSpentTimes.Queries.GetList;

public class GetListLectureSpentTimeQuery : IRequest<GetListResponse<GetListLectureSpentTimeListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLectureSpentTimes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLectureSpentTimes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLectureSpentTimeQueryHandler : IRequestHandler<GetListLectureSpentTimeQuery, GetListResponse<GetListLectureSpentTimeListItemDto>>
    {
        private readonly ILectureSpentTimeRepository _lectureSpentTimeRepository;
        private readonly IMapper _mapper;

        public GetListLectureSpentTimeQueryHandler(ILectureSpentTimeRepository lectureSpentTimeRepository, IMapper mapper)
        {
            _lectureSpentTimeRepository = lectureSpentTimeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLectureSpentTimeListItemDto>> Handle(GetListLectureSpentTimeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LectureSpentTime> lectureSpentTimes = await _lectureSpentTimeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLectureSpentTimeListItemDto> response = _mapper.Map<GetListResponse<GetListLectureSpentTimeListItemDto>>(lectureSpentTimes);
            return response;
        }
    }
}