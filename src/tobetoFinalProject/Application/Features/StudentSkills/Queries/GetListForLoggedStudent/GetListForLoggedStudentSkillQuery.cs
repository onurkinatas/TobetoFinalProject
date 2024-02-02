using Application.Features.StudentSkills.Queries.GetList;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentSkills.Queries.GetListForLoggedStudent;
public class GetListForLoggedStudentSkillQuery : IRequest<GetListResponse<GetListStudentSkillListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { "Student" };

    public class GetListForLoggedStudentSkillQueryHandler : IRequestHandler<GetListForLoggedStudentSkillQuery, GetListResponse<GetListStudentSkillListItemDto>>
    {
        private readonly IStudentSkillRepository _studentSkillRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListForLoggedStudentSkillQueryHandler(IStudentSkillRepository studentSkillRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentSkillRepository = studentSkillRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentSkillListItemDto>> Handle(GetListForLoggedStudentSkillQuery request, CancellationToken cancellationToken)
        {
            Student student = await _contextOperationService.GetStudentFromContext();

            IPaginate<StudentSkill> studentSkills = await _studentSkillRepository.GetListAsync(
                predicate: ss => ss.StudentId == student.Id,
                include: ss => ss.Include(ss => ss.Skill)
                    .Include(ss => ss.Student)
                    .ThenInclude(s => s.User),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentSkillListItemDto> response = _mapper.Map<GetListResponse<GetListStudentSkillListItemDto>>(studentSkills);
            return response;
        }
    }
}
