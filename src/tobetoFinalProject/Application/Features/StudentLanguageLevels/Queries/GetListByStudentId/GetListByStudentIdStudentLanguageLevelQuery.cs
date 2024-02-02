using Application.Features.StudentLanguageLevels.Queries.GetList;
using Application.Features.StudentLanguageLevels.Queries.GetListByStudentId;
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

namespace Application.Features.StudentLanguageLevels.Queries.GetListByStudentId;
public class GetListByStudentIdStudentLanguageLevelQuery : IRequest<GetListResponse<GetListStudentLanguageLevelListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid StudentId { get; set; }
    public string[] Roles => new[] { "Admin" };

    public class GetListByStudentIdStudentLanguageLevelQueryHandler : IRequestHandler<GetListByStudentIdStudentLanguageLevelQuery, GetListResponse<GetListStudentLanguageLevelListItemDto>>
    {
        private readonly IStudentLanguageLevelRepository _studentLanguageLevelRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListByStudentIdStudentLanguageLevelQueryHandler(IStudentLanguageLevelRepository studentLanguageLevelRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentLanguageLevelRepository = studentLanguageLevelRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentLanguageLevelListItemDto>> Handle(GetListByStudentIdStudentLanguageLevelQuery request, CancellationToken cancellationToken)
        {

            IPaginate<StudentLanguageLevel> studentLanguageLevels = await _studentLanguageLevelRepository.GetListAsync(
                predicate: se => se.StudentId == request.StudentId,
                include: sll => sll.Include(sll => sll.Student)
                    .ThenInclude(s => s.User)
                    .Include(sll => sll.LanguageLevel)
                    .ThenInclude(ll => ll.Language),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentLanguageLevelListItemDto> response = _mapper.Map<GetListResponse<GetListStudentLanguageLevelListItemDto>>(studentLanguageLevels);
            return response;
        }
    }
}

