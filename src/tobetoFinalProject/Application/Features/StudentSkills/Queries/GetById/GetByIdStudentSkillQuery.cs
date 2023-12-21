using Application.Features.StudentSkills.Constants;
using Application.Features.StudentSkills.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentSkills.Constants.StudentSkillsOperationClaims;
using Application.Services.CacheForMemory;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudentSkills.Queries.GetById;

public class GetByIdStudentSkillQuery : IRequest<GetByIdStudentSkillResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetByIdStudentSkillQueryHandler : IRequestHandler<GetByIdStudentSkillQuery, GetByIdStudentSkillResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentSkillRepository _studentSkillRepository;
        private readonly StudentSkillBusinessRules _studentSkillBusinessRules;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetByIdStudentSkillQueryHandler(IMapper mapper, IStudentSkillRepository studentSkillRepository, StudentSkillBusinessRules studentSkillBusinessRules, ICacheMemoryService cacheMemoryService)
        {
            _mapper = mapper;
            _studentSkillRepository = studentSkillRepository;
            _studentSkillBusinessRules = studentSkillBusinessRules;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetByIdStudentSkillResponse> Handle(GetByIdStudentSkillQuery request, CancellationToken cancellationToken)
        {
            var cacheMemoryStudentId = _cacheMemoryService.GetStudentIdFromCache();

            StudentSkill? studentSkill = await _studentSkillRepository.GetAsync(
                predicate: ss => ss.Id == request.Id && ss.StudentId == cacheMemoryStudentId,
                include: ss => ss.Include(ss => ss.Skill)
                    .Include(ss => ss.Student),
                cancellationToken: cancellationToken);
            await _studentSkillBusinessRules.StudentSkillShouldExistWhenSelected(studentSkill);

            GetByIdStudentSkillResponse response = _mapper.Map<GetByIdStudentSkillResponse>(studentSkill);
            return response;
        }
    }
}