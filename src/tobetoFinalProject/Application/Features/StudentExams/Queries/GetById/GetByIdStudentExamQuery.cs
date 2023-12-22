using Application.Features.StudentExams.Constants;
using Application.Features.StudentExams.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentExams.Constants.StudentExamsOperationClaims;
using Application.Services.CacheForMemory;

namespace Application.Features.StudentExams.Queries.GetById;

public class GetByIdStudentExamQuery : IRequest<GetByIdStudentExamResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetByIdStudentExamQueryHandler : IRequestHandler<GetByIdStudentExamQuery, GetByIdStudentExamResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly StudentExamBusinessRules _studentExamBusinessRules;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetByIdStudentExamQueryHandler(IMapper mapper, IStudentExamRepository studentExamRepository, StudentExamBusinessRules studentExamBusinessRules, ICacheMemoryService cacheMemoryService)
        {
            _mapper = mapper;
            _studentExamRepository = studentExamRepository;
            _studentExamBusinessRules = studentExamBusinessRules;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetByIdStudentExamResponse> Handle(GetByIdStudentExamQuery request, CancellationToken cancellationToken)
        {
            var cacheMemoryStudentId = _cacheMemoryService.GetStudentIdFromCache();

            StudentExam? studentExam = await _studentExamRepository.GetAsync(
                predicate: se => se.Id == request.Id && se.StudentId == cacheMemoryStudentId,
                cancellationToken: cancellationToken);
            await _studentExamBusinessRules.StudentExamShouldExistWhenSelected(studentExam);

            GetByIdStudentExamResponse response = _mapper.Map<GetByIdStudentExamResponse>(studentExam);
            return response;
        }
    }
}