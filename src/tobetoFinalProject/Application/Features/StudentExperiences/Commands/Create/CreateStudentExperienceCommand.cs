using Application.Features.StudentExperiences.Constants;
using Application.Features.StudentExperiences.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentExperiences.Constants.StudentExperiencesOperationClaims;
using Application.Services.ContextOperations;

namespace Application.Features.StudentExperiences.Commands.Create;

public class CreateStudentExperienceCommand : IRequest<CreatedStudentExperienceResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid? StudentId { get; set; }
    public string CompanyName { get; set; }
    public string Sector { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public Guid CityId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentExperiencesOperationClaims.Create, "Student" };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentExperiences";

    public class CreateStudentExperienceCommandHandler : IRequestHandler<CreateStudentExperienceCommand, CreatedStudentExperienceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentExperienceRepository _studentExperienceRepository;
        private readonly StudentExperienceBusinessRules _studentExperienceBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        public CreateStudentExperienceCommandHandler(IMapper mapper, IStudentExperienceRepository studentExperienceRepository,
                                         StudentExperienceBusinessRules studentExperienceBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentExperienceRepository = studentExperienceRepository;
            _studentExperienceBusinessRules = studentExperienceBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<CreatedStudentExperienceResponse> Handle(CreateStudentExperienceCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            request.StudentId = getStudent.Id;
            StudentExperience studentExperience = _mapper.Map<StudentExperience>(request);
            
            await _studentExperienceRepository.AddAsync(studentExperience);

            CreatedStudentExperienceResponse response = _mapper.Map<CreatedStudentExperienceResponse>(studentExperience);
            return response;
        }
    }
}