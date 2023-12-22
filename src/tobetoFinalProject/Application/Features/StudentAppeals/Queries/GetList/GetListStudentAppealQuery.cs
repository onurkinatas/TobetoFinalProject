using Application.Features.StudentAppeals.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentAppeals.Constants.StudentAppealsOperationClaims;
using Application.Services.CacheForMemory;

namespace Application.Features.StudentAppeals.Queries.GetList;

public class GetListStudentAppealQuery : IRequest<GetListResponse<GetListStudentAppealListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentAppeals({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentAppeals";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentAppealQueryHandler : IRequestHandler<GetListStudentAppealQuery, GetListResponse<GetListStudentAppealListItemDto>>
    {
        private readonly IStudentAppealRepository _studentAppealRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetListStudentAppealQueryHandler(IStudentAppealRepository studentAppealRepository, IMapper mapper, ICacheMemoryService cacheMemoryService)
        {
            _studentAppealRepository = studentAppealRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetListResponse<GetListStudentAppealListItemDto>> Handle(GetListStudentAppealQuery request, CancellationToken cancellationToken)
        {
            var cacheMemoryStudentId = _cacheMemoryService.GetStudentIdFromCache();

            IPaginate<StudentAppeal> studentAppeals = await _studentAppealRepository.GetListAsync(
                predicate: s => s.StudentId == cacheMemoryStudentId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentAppealListItemDto> response = _mapper.Map<GetListResponse<GetListStudentAppealListItemDto>>(studentAppeals);
            return response;
        }
    }
}