using Application.Features.Students.Constants;
using Application.Features.Students.Constants;
using Application.Features.Students.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Students.Constants.StudentsOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Students.Commands.Delete;

public class DeleteStudentCommand : IRequest<DeletedStudentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudents";

    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, DeletedStudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly StudentBusinessRules _studentBusinessRules;

        public DeleteStudentCommandHandler(IMapper mapper, IStudentRepository studentRepository,
                                         StudentBusinessRules studentBusinessRules)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _studentBusinessRules = studentBusinessRules;
        }

        public async Task<DeletedStudentResponse> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            Student? student = await _studentRepository.GetAsync(
                predicate: s => s.Id == request.Id,
                include: s => s.Include(s => s.StudentEducations)
                    .Include(s => s.StudentExperiences)
                    .Include(s => s.StudentLanguageLevels)
                    .Include(s => s.StudentSkills)
                    .Include(s => s.StudentSocialMedias)
                    .Include(s => s.StudentClassStudentes)
                    .Include(s => s.StudentPrivateCertificates)
                    .Include(s => s.StudentAppeal)
                    .Include(s => s.StudentStages),
                cancellationToken: cancellationToken);

            await _studentBusinessRules.StudentShouldExistWhenSelected(student);

            await _studentRepository.DeleteAsync(student!);

            DeletedStudentResponse response = _mapper.Map<DeletedStudentResponse>(student);
            return response;
        }
    }
}