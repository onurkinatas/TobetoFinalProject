using Application.Features.StudentExperiences.Constants;
using Application.Features.StudentExperiences.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentExperiences.Constants.StudentExperiencesOperationClaims;

namespace Application.Features.StudentExperiences.Queries.GetById;

public class GetByIdStudentExperienceQuery : IRequest<GetByIdStudentExperienceResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStudentExperienceQueryHandler : IRequestHandler<GetByIdStudentExperienceQuery, GetByIdStudentExperienceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentExperienceRepository _studentExperienceRepository;
        private readonly StudentExperienceBusinessRules _studentExperienceBusinessRules;

        public GetByIdStudentExperienceQueryHandler(IMapper mapper, IStudentExperienceRepository studentExperienceRepository, StudentExperienceBusinessRules studentExperienceBusinessRules)
        {
            _mapper = mapper;
            _studentExperienceRepository = studentExperienceRepository;
            _studentExperienceBusinessRules = studentExperienceBusinessRules;
        }

        public async Task<GetByIdStudentExperienceResponse> Handle(GetByIdStudentExperienceQuery request, CancellationToken cancellationToken)
        {
            StudentExperience? studentExperience = await _studentExperienceRepository.GetAsync(predicate: se => se.Id == request.Id, cancellationToken: cancellationToken);
            await _studentExperienceBusinessRules.StudentExperienceShouldExistWhenSelected(studentExperience);

            GetByIdStudentExperienceResponse response = _mapper.Map<GetByIdStudentExperienceResponse>(studentExperience);
            return response;
        }
    }
}