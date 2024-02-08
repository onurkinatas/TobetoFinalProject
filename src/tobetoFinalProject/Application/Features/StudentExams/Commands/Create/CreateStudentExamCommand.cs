using Application.Features.StudentExams.Constants;
using Application.Features.StudentExams.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentExams.Constants.StudentExamsOperationClaims;
using Application.Services.ContextOperations;

namespace Application.Features.StudentExams.Commands.Create;

public class CreateStudentExamCommand : IRequest<CreatedStudentExamResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid ExamId { get; set; }
    public Guid? StudentId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentExamsOperationClaims.Create, "Student" };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentExams";

    public class CreateStudentExamCommandHandler : IRequestHandler<CreateStudentExamCommand, CreatedStudentExamResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly IContextOperationService _contextOperationService;
        private readonly StudentExamBusinessRules _studentExamBusinessRules;

        public CreateStudentExamCommandHandler(IMapper mapper, IStudentExamRepository studentExamRepository,
                                         StudentExamBusinessRules studentExamBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentExamRepository = studentExamRepository;
            _studentExamBusinessRules = studentExamBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<CreatedStudentExamResponse> Handle(CreateStudentExamCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            StudentExam studentExam = _mapper.Map<StudentExam>(request);
            studentExam.StudentId = getStudent.Id;

            bool doesExist = await _studentExamRepository.AnyAsync(predicate: sa => sa.StudentId == studentExam.StudentId && sa.ExamId == studentExam.ExamId);

            if (!doesExist)
                await _studentExamRepository.AddAsync(studentExam);


            CreatedStudentExamResponse response = _mapper.Map<CreatedStudentExamResponse>(studentExam);
            return response;
        }
    }
}