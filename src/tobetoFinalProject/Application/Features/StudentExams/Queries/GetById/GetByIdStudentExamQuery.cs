using Application.Features.StudentExams.Constants;
using Application.Features.StudentExams.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentExams.Constants.StudentExamsOperationClaims;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;

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
        private readonly IContextOperationService _contextOperationService;

        public GetByIdStudentExamQueryHandler(IMapper mapper, IStudentExamRepository studentExamRepository, StudentExamBusinessRules studentExamBusinessRules, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentExamRepository = studentExamRepository;
            _studentExamBusinessRules = studentExamBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetByIdStudentExamResponse> Handle(GetByIdStudentExamQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();

            StudentExam? studentExam = await _studentExamRepository.GetAsync(
                predicate: se => se.Id == request.Id,
                cancellationToken: cancellationToken);
            await _studentExamBusinessRules.StudentExamShouldExistWhenSelected(studentExam);

            GetByIdStudentExamResponse response = _mapper.Map<GetByIdStudentExamResponse>(studentExam);
            return response;
        }
    }
}