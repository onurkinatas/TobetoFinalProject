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

namespace Application.Features.StudentClassStudents.Commands.Update;

public class UpdateStudentClassStudentCommand : IRequest<UpdatedStudentClassStudentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid StudentClassId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentClassStudentsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentClassStudents";

    public class UpdateStudentClassStudentCommandHandler : IRequestHandler<UpdateStudentClassStudentCommand, UpdatedStudentClassStudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentClassStudentRepository _studentClassStudentRepository;
        private readonly StudentClassStudentBusinessRules _studentClassStudentBusinessRules;

        public UpdateStudentClassStudentCommandHandler(IMapper mapper, IStudentClassStudentRepository studentClassStudentRepository,
                                         StudentClassStudentBusinessRules studentClassStudentBusinessRules)
        {
            _mapper = mapper;
            _studentClassStudentRepository = studentClassStudentRepository;
            _studentClassStudentBusinessRules = studentClassStudentBusinessRules;
        }

        public async Task<UpdatedStudentClassStudentResponse> Handle(UpdateStudentClassStudentCommand request, CancellationToken cancellationToken)
        {
            StudentClassStudent? studentClassStudent = await _studentClassStudentRepository.GetAsync(predicate: scs => scs.Id == request.Id, cancellationToken: cancellationToken);
            await _studentClassStudentBusinessRules.StudentClassStudentShouldExistWhenSelected(studentClassStudent);
            studentClassStudent = _mapper.Map(request, studentClassStudent);
            await _studentClassStudentBusinessRules.StudentClassStudentShouldNotExistsWhenUpdate(studentClassStudent.StudentClassId, studentClassStudent.StudentId);
            await _studentClassStudentRepository.UpdateAsync(studentClassStudent!);

            UpdatedStudentClassStudentResponse response = _mapper.Map<UpdatedStudentClassStudentResponse>(studentClassStudent);
            return response;
        }
    }
}