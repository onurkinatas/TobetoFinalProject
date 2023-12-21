using Application.Features.LectureCompletionConditions.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.LectureCompletionConditions.Constants.LectureCompletionConditionsOperationClaims;

namespace Application.Features.LectureCompletionConditions.Queries.GetList;

public class GetListLectureCompletionConditionQuery : IRequest<GetListResponse<GetListLectureCompletionConditionListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLectureCompletionConditions({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetLectureCompletionConditions";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLectureCompletionConditionQueryHandler : IRequestHandler<GetListLectureCompletionConditionQuery, GetListResponse<GetListLectureCompletionConditionListItemDto>>
    {
        private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;
        private readonly IMapper _mapper;

        public GetListLectureCompletionConditionQueryHandler(ILectureCompletionConditionRepository lectureCompletionConditionRepository, IMapper mapper)
        {
            _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLectureCompletionConditionListItemDto>> Handle(GetListLectureCompletionConditionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LectureCompletionCondition> lectureCompletionConditions = await _lectureCompletionConditionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListLectureCompletionConditionListItemDto> response = _mapper.Map<GetListResponse<GetListLectureCompletionConditionListItemDto>>(lectureCompletionConditions);
            return response;
        }
    }
}