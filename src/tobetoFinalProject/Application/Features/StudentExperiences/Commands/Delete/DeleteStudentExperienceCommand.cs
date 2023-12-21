using Application.Features.StudentExperiences.Constants;
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

namespace Application.Features.StudentExperiences.Commands.Delete;

public class DeleteStudentExperienceCommand : IRequest<DeletedStudentExperienceResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentExperiencesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentExperiences";

    public class DeleteStudentExperienceCommandHandler : IRequestHandler<DeleteStudentExperienceCommand, DeletedStudentExperienceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentExperienceRepository _studentExperienceRepository;
        private readonly StudentExperienceBusinessRules _studentExperienceBusinessRules;

        public DeleteStudentExperienceCommandHandler(IMapper mapper, IStudentExperienceRepository studentExperienceRepository,
                                         StudentExperienceBusinessRules studentExperienceBusinessRules)
        {
            _mapper = mapper;
            _studentExperienceRepository = studentExperienceRepository;
            _studentExperienceBusinessRules = studentExperienceBusinessRules;
        }

        public async Task<DeletedStudentExperienceResponse> Handle(DeleteStudentExperienceCommand request, CancellationToken cancellationToken)
        {
            StudentExperience? studentExperience = await _studentExperienceRepository.GetAsync(predicate: se => se.Id == request.Id, cancellationToken: cancellationToken);
            await _studentExperienceBusinessRules.StudentExperienceShouldExistWhenSelected(studentExperience);

            await _studentExperienceRepository.DeleteAsync(studentExperience!);

            DeletedStudentExperienceResponse response = _mapper.Map<DeletedStudentExperienceResponse>(studentExperience);
            return response;
        }
    }
}