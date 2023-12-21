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

namespace Application.Features.StudentExams.Commands.Update;

public class UpdateStudentExamCommand : IRequest<UpdatedStudentExamResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentExamsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentExams";

    public class UpdateStudentExamCommandHandler : IRequestHandler<UpdateStudentExamCommand, UpdatedStudentExamResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly StudentExamBusinessRules _studentExamBusinessRules;

        public UpdateStudentExamCommandHandler(IMapper mapper, IStudentExamRepository studentExamRepository,
                                         StudentExamBusinessRules studentExamBusinessRules)
        {
            _mapper = mapper;
            _studentExamRepository = studentExamRepository;
            _studentExamBusinessRules = studentExamBusinessRules;
        }

        public async Task<UpdatedStudentExamResponse> Handle(UpdateStudentExamCommand request, CancellationToken cancellationToken)
        {
            StudentExam? studentExam = await _studentExamRepository.GetAsync(predicate: se => se.Id == request.Id, cancellationToken: cancellationToken);
            await _studentExamBusinessRules.StudentExamShouldExistWhenSelected(studentExam);
            studentExam = _mapper.Map(request, studentExam);

            await _studentExamRepository.UpdateAsync(studentExam!);

            UpdatedStudentExamResponse response = _mapper.Map<UpdatedStudentExamResponse>(studentExam);
            return response;
        }
    }
}