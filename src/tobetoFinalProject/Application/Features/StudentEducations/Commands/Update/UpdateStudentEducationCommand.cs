using Application.Features.StudentEducations.Constants;
using Application.Features.StudentEducations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentEducations.Constants.StudentEducationsOperationClaims;
using Application.Services.ContextOperations;

namespace Application.Features.StudentEducations.Commands.Update;

public class UpdateStudentEducationCommand : IRequest<UpdatedStudentEducationResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid? StudentId { get; set; }
    public string EducationStatus { get; set; }
    public string SchoolName { get; set; }
    public string Branch { get; set; }
    public bool IsContinued { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? GraduationDate { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentEducationsOperationClaims.Update, "Student" };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentEducations";

    public class UpdateStudentEducationCommandHandler : IRequestHandler<UpdateStudentEducationCommand, UpdatedStudentEducationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentEducationRepository _studentEducationRepository;
        private readonly StudentEducationBusinessRules _studentEducationBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        public UpdateStudentEducationCommandHandler(IMapper mapper, IStudentEducationRepository studentEducationRepository,
                                         StudentEducationBusinessRules studentEducationBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentEducationRepository = studentEducationRepository;
            _studentEducationBusinessRules = studentEducationBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<UpdatedStudentEducationResponse> Handle(UpdateStudentEducationCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            StudentEducation? studentEducation = await _studentEducationRepository.GetAsync(predicate: se => se.Id == request.Id, cancellationToken: cancellationToken);
            await _studentEducationBusinessRules.StudentEducationShouldExistWhenSelected(studentEducation);
            request.StudentId = getStudent.Id;
            studentEducation = _mapper.Map(request, studentEducation);
            await _studentEducationBusinessRules.StudentEducationShouldNotExistsWhenUpdate(studentEducation);
            await _studentEducationRepository.UpdateAsync(studentEducation!);

            UpdatedStudentEducationResponse response = _mapper.Map<UpdatedStudentEducationResponse>(studentEducation);
            return response;
        }
    }
}