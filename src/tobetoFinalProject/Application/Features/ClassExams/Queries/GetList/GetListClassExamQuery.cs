using Application.Features.ClassExams.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ClassExams.Constants.ClassExamsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;

namespace Application.Features.ClassExams.Queries.GetList;

public class GetListClassExamQuery : IRequest<GetListResponse<GetListClassExamListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListClassExams({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetClassExams";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListClassExamQueryHandler : IRequestHandler<GetListClassExamQuery, GetListResponse<GetListClassExamListItemDto>>
    {
        private readonly IClassExamRepository _classExamRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListClassExamQueryHandler(IClassExamRepository classExamRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _classExamRepository = classExamRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListClassExamListItemDto>> Handle(GetListClassExamQuery request, CancellationToken cancellationToken)
        {
            ICollection<Guid> getClassIds = await _contextOperationService.GetStudentClassesFromContext();
            IPaginate<ClassExam> classExams = await _classExamRepository.GetListAsync(
                predicate: ce => getClassIds.Contains(ce.StudentClassId) && ce.Exam.IsActive != false,
                include: ce => ce.Include(ce => ce.Exam)
                    .Include(ce => ce.StudentClass),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListClassExamListItemDto> response = _mapper.Map<GetListResponse<GetListClassExamListItemDto>>(classExams);
            return response;
        }
    }
}