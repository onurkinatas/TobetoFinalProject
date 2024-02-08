using Application.Features.StudentExams.Queries.GetList;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentExams.Queries.GetListForLoggedStudent;
public class GetListStudentExamForLoggedStudentQuery : IRequest<GetListResponse<GetListStudentExamForLoggedStudentListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentExams({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentExams";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentExamForLoggedStudentQueryHandler : IRequestHandler<GetListStudentExamForLoggedStudentQuery, GetListResponse<GetListStudentExamForLoggedStudentListItemDto>>
    {
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;

        public GetListStudentExamForLoggedStudentQueryHandler(IStudentExamRepository studentExamRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentExamRepository = studentExamRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentExamForLoggedStudentListItemDto>> Handle(GetListStudentExamForLoggedStudentQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();

            IPaginate<StudentExam> studentExams = await _studentExamRepository.GetListAsync(
                predicate:se=>se.StudentId==getStudent.Id,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentExamForLoggedStudentListItemDto> response = _mapper.Map<GetListResponse<GetListStudentExamForLoggedStudentListItemDto>>(studentExams);
            return response;
        }
    }
}
