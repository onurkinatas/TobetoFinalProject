using Application.Features.StudentClassStudents.Constants;
using Application.Features.StudentClassStudents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentClassStudents.Constants.StudentClassStudentsOperationClaims;

namespace Application.Features.StudentClassStudents.Commands.Create;

public class CreateStudentClassStudentCommand : IRequest<CreatedStudentClassStudentResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest, ICacheRemoverRequest
{
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentClassStudents";
    public Guid StudentId { get; set; }
    public Guid StudentClassId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentClassStudentsOperationClaims.Create };


    public class CreateStudentClassStudentCommandHandler : IRequestHandler<CreateStudentClassStudentCommand, CreatedStudentClassStudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentClassStudentRepository _studentClassStudentRepository;
        private readonly StudentClassStudentBusinessRules _studentClassStudentBusinessRules;

        public CreateStudentClassStudentCommandHandler(IMapper mapper, IStudentClassStudentRepository studentClassStudentRepository,
                                         StudentClassStudentBusinessRules studentClassStudentBusinessRules)
        {
            _mapper = mapper;
            _studentClassStudentRepository = studentClassStudentRepository;
            _studentClassStudentBusinessRules = studentClassStudentBusinessRules;
        }

        public async Task<CreatedStudentClassStudentResponse> Handle(CreateStudentClassStudentCommand request, CancellationToken cancellationToken)
        {
            StudentClassStudent studentClassStudent = _mapper.Map<StudentClassStudent>(request);
            await _studentClassStudentBusinessRules.StudentClassStudentShouldNotExistsWhenInsert(studentClassStudent.StudentClassId, studentClassStudent.StudentId);
            await _studentClassStudentRepository.AddAsync(studentClassStudent);

            CreatedStudentClassStudentResponse response = _mapper.Map<CreatedStudentClassStudentResponse>(studentClassStudent);
            return response;
        }
    }
}