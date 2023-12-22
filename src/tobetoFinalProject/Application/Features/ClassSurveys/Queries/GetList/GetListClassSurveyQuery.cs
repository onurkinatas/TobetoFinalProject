using Application.Features.ClassSurveys.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ClassSurveys.Constants.ClassSurveysOperationClaims;
using Application.Services.CacheForMemory;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClassSurveys.Queries.GetList;

public class GetListClassSurveyQuery : IRequest<GetListResponse<GetListClassSurveyListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListClassSurveys({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetClassSurveys";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListClassSurveyQueryHandler : IRequestHandler<GetListClassSurveyQuery, GetListResponse<GetListClassSurveyListItemDto>>
    {
        private readonly IClassSurveyRepository _classSurveyRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetListClassSurveyQueryHandler(IClassSurveyRepository classSurveyRepository, IMapper mapper, ICacheMemoryService cacheMemoryService)
        {
            _classSurveyRepository = classSurveyRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetListResponse<GetListClassSurveyListItemDto>> Handle(GetListClassSurveyQuery request, CancellationToken cancellationToken)
        {
            IList<Guid> getCacheClassIds = _cacheMemoryService.GetStudentClassIdFromCache();

            IPaginate<ClassSurvey> classSurveys = await _classSurveyRepository.GetListAsync(
                predicate: c => getCacheClassIds.Contains(c.StudentClassId),
                include: c => c.Include(c => c.StudentClass)
                    .Include(c => c.Survey),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListClassSurveyListItemDto> response = _mapper.Map<GetListResponse<GetListClassSurveyListItemDto>>(classSurveys);
            return response;
        }
    }
}