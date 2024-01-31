using Application.Features.StudentSkills.Queries.GetList;
using Application.Features.StudentSkills.Queries.GetListByStudentId;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
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

namespace Application.Features.StudentSkills.Queries.GetListByStudentId;
public class GetListByStudentIdStudentSkillQuery : IRequest<GetListResponse<GetListStudentSkillListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid StudentId { get; set; }
    public string[] Roles => new[] { "Admin" };

    public class GetListByStudentIdStudentSkillQueryHandler : IRequestHandler<GetListByStudentIdStudentSkillQuery, GetListResponse<GetListStudentSkillListItemDto>>
    {
        private readonly IStudentSkillRepository _studentSkillRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListByStudentIdStudentSkillQueryHandler(IStudentSkillRepository studentSkillRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentSkillRepository = studentSkillRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentSkillListItemDto>> Handle(GetListByStudentIdStudentSkillQuery request, CancellationToken cancellationToken)
        {

            IPaginate<StudentSkill> studentSkills = await _studentSkillRepository.GetListAsync(
                predicate: se => se.StudentId == request.StudentId,
                include: sll => sll.Include(sll => sll.Student)
                    .ThenInclude(s => s.User)
                    .Include(sll => sll.Skill),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentSkillListItemDto> response = _mapper.Map<GetListResponse<GetListStudentSkillListItemDto>>(studentSkills);
            return response;
        }
    }
}

