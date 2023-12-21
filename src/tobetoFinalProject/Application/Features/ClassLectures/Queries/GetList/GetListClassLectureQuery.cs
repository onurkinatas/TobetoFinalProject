using Application.Features.ClassLectures.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ClassLectures.Constants.ClassLecturesOperationClaims;

namespace Application.Features.ClassLectures.Queries.GetList;

public class GetListClassLectureQuery : IRequest<GetListResponse<GetListClassLectureListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListClassLectures({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetClassLectures";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListClassLectureQueryHandler : IRequestHandler<GetListClassLectureQuery, GetListResponse<GetListClassLectureListItemDto>>
    {
        private readonly IClassLectureRepository _classLectureRepository;
        private readonly IMapper _mapper;

        public GetListClassLectureQueryHandler(IClassLectureRepository classLectureRepository, IMapper mapper)
        {
            _classLectureRepository = classLectureRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListClassLectureListItemDto>> Handle(GetListClassLectureQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ClassLecture> classLectures = await _classLectureRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListClassLectureListItemDto> response = _mapper.Map<GetListResponse<GetListClassLectureListItemDto>>(classLectures);
            return response;
        }
    }
}