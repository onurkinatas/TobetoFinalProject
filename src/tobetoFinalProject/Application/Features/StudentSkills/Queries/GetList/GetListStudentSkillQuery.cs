using Application.Features.StudentSkills.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentSkills.Constants.StudentSkillsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;

namespace Application.Features.StudentSkills.Queries.GetList;

public class GetListStudentSkillQuery : IRequest<GetListResponse<GetListStudentSkillListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentSkills({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentSkills";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentSkillQueryHandler : IRequestHandler<GetListStudentSkillQuery, GetListResponse<GetListStudentSkillListItemDto>>
    {
        private readonly IStudentSkillRepository _studentSkillRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListStudentSkillQueryHandler(IStudentSkillRepository studentSkillRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentSkillRepository = studentSkillRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentSkillListItemDto>> Handle(GetListStudentSkillQuery request, CancellationToken cancellationToken)
        {
            Student student = await _contextOperationService.GetStudentFromContext();

            IPaginate<StudentSkill> studentSkills = await _studentSkillRepository.GetListAsync(
                predicate: ss => ss.StudentId == student.Id,
                include: ss => ss.Include(ss => ss.Skill)
                    .Include(ss => ss.Student),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentSkillListItemDto> response = _mapper.Map<GetListResponse<GetListStudentSkillListItemDto>>(studentSkills);
            return response;
        }
    }
}