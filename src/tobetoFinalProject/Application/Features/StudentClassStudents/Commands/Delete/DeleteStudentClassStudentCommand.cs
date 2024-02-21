using Application.Features.StudentClassStudents.Constants;
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

namespace Application.Features.StudentClassStudents.Commands.Delete;

public class DeleteStudentClassStudentCommand : IRequest<DeletedStudentClassStudentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentClassStudentsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllClassDetails";

    public class DeleteStudentClassStudentCommandHandler : IRequestHandler<DeleteStudentClassStudentCommand, DeletedStudentClassStudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentClassStudentRepository _studentClassStudentRepository;
        private readonly StudentClassStudentBusinessRules _studentClassStudentBusinessRules;

        public DeleteStudentClassStudentCommandHandler(IMapper mapper, IStudentClassStudentRepository studentClassStudentRepository,
                                         StudentClassStudentBusinessRules studentClassStudentBusinessRules)
        {
            _mapper = mapper;
            _studentClassStudentRepository = studentClassStudentRepository;
            _studentClassStudentBusinessRules = studentClassStudentBusinessRules;
        }

        public async Task<DeletedStudentClassStudentResponse> Handle(DeleteStudentClassStudentCommand request, CancellationToken cancellationToken)
        {
            StudentClassStudent? studentClassStudent = await _studentClassStudentRepository.GetAsync(predicate: scs => scs.Id == request.Id, cancellationToken: cancellationToken);
            await _studentClassStudentBusinessRules.StudentClassStudentShouldExistWhenSelected(studentClassStudent);

            await _studentClassStudentRepository.DeleteAsync(studentClassStudent!);

            DeletedStudentClassStudentResponse response = _mapper.Map<DeletedStudentClassStudentResponse>(studentClassStudent);
            return response;
        }
    }
}