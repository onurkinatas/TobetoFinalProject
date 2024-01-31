using Application.Features.StudentEducations.Queries.GetList;
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

namespace Application.Features.StudentEducations.Queries.GetListByStudentId;
public class GetListByStudentIdStudentEducationQuery : IRequest<GetListResponse<GetListStudentEducationListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid StudentId { get; set; }
    public string[] Roles => new[] { "Admin" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentEducations({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentEducations";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByStudentIdStudentEducationQueryHandler : IRequestHandler<GetListByStudentIdStudentEducationQuery, GetListResponse<GetListStudentEducationListItemDto>>
    {
        private readonly IStudentEducationRepository _studentEducationRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListByStudentIdStudentEducationQueryHandler(IStudentEducationRepository studentEducationRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentEducationRepository = studentEducationRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentEducationListItemDto>> Handle(GetListByStudentIdStudentEducationQuery request, CancellationToken cancellationToken)
        {

            IPaginate<StudentEducation> studentEducations = await _studentEducationRepository.GetListAsync(
                predicate:se=>se.StudentId==request.StudentId,
                include: s => s.Include(s => s.Student).ThenInclude(s => s.User),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentEducationListItemDto> response = _mapper.Map<GetListResponse<GetListStudentEducationListItemDto>>(studentEducations);
            return response;
        }
    }
}