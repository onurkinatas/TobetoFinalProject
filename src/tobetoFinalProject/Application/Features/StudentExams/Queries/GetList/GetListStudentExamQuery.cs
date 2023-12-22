using Application.Features.StudentExams.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentExams.Constants.StudentExamsOperationClaims;
using Application.Services.CacheForMemory;

namespace Application.Features.StudentExams.Queries.GetList;

public class GetListStudentExamQuery : IRequest<GetListResponse<GetListStudentExamListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentExams({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentExams";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentExamQueryHandler : IRequestHandler<GetListStudentExamQuery, GetListResponse<GetListStudentExamListItemDto>>
    {
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetListStudentExamQueryHandler(IStudentExamRepository studentExamRepository, IMapper mapper, ICacheMemoryService cacheMemoryService)
        {
            _studentExamRepository = studentExamRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetListResponse<GetListStudentExamListItemDto>> Handle(GetListStudentExamQuery request, CancellationToken cancellationToken)
        {
            var cacheMemoryStudentId = _cacheMemoryService.GetStudentIdFromCache();

            IPaginate<StudentExam> studentExams = await _studentExamRepository.GetListAsync(
                predicate: se => se.StudentId == cacheMemoryStudentId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentExamListItemDto> response = _mapper.Map<GetListResponse<GetListStudentExamListItemDto>>(studentExams);
            return response;
        }
    }
}