using Application.Features.StudentExperiences.Constants;
using Application.Features.StudentExperiences.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentExperiences.Constants.StudentExperiencesOperationClaims;
using Application.Services.CacheForMemory;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudentExperiences.Queries.GetById;

public class GetByIdStudentExperienceQuery : IRequest<GetByIdStudentExperienceResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetByIdStudentExperienceQueryHandler : IRequestHandler<GetByIdStudentExperienceQuery, GetByIdStudentExperienceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentExperienceRepository _studentExperienceRepository;
        private readonly StudentExperienceBusinessRules _studentExperienceBusinessRules;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetByIdStudentExperienceQueryHandler(IMapper mapper, IStudentExperienceRepository studentExperienceRepository, StudentExperienceBusinessRules studentExperienceBusinessRules, ICacheMemoryService cacheMemoryService)
        {
            _mapper = mapper;
            _studentExperienceRepository = studentExperienceRepository;
            _studentExperienceBusinessRules = studentExperienceBusinessRules;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetByIdStudentExperienceResponse> Handle(GetByIdStudentExperienceQuery request, CancellationToken cancellationToken)
        {

            StudentExperience? studentExperience = await _studentExperienceRepository.GetAsync(
                predicate: se => se.Id == request.Id,
                include: se => se.Include(se => se.City),
                cancellationToken: cancellationToken);
            await _studentExperienceBusinessRules.StudentExperienceShouldExistWhenSelected(studentExperience);

            GetByIdStudentExperienceResponse response = _mapper.Map<GetByIdStudentExperienceResponse>(studentExperience);
            return response;
        }
    }
}