using Application.Features.StudentExperiences.Queries.GetList;
using Application.Features.StudentExperiences.Queries.GetListByStudentId;
using Application.Features.StudentExperiences.Queries.GetList;
using Application.Features.StudentExperiences.Queries.GetListByStudentId;
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

namespace Application.Features.StudentExperiences.Queries.GetListByStudentId;
public class GetListByStudentIdStudentExperienceQuery : IRequest<GetListResponse<GetListStudentExperienceListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid StudentId { get; set; }
    public string[] Roles => new[] { "Admin" };
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByStudentIdStudentExperienceQueryHandler : IRequestHandler<GetListByStudentIdStudentExperienceQuery, GetListResponse<GetListStudentExperienceListItemDto>>
    {
        private readonly IStudentExperienceRepository _studentExperienceRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListByStudentIdStudentExperienceQueryHandler(IStudentExperienceRepository studentExperienceRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentExperienceRepository = studentExperienceRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentExperienceListItemDto>> Handle(GetListByStudentIdStudentExperienceQuery request, CancellationToken cancellationToken)
        {

            IPaginate<StudentExperience> studentExperiences = await _studentExperienceRepository.GetListAsync(
                predicate: se => se.StudentId == request.StudentId,
                include: s => s.Include(s => s.Student).ThenInclude(s => s.User),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentExperienceListItemDto> response = _mapper.Map<GetListResponse<GetListStudentExperienceListItemDto>>(studentExperiences);
            return response;
        }
    }
}