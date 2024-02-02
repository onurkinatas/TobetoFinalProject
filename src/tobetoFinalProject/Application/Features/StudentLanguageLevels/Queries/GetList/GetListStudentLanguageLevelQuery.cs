using Application.Features.StudentLanguageLevels.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentLanguageLevels.Constants.StudentLanguageLevelsOperationClaims;
using Application.Services.CacheForMemory;
using Microsoft.EntityFrameworkCore;
using Application.Services.ContextOperations;

namespace Application.Features.StudentLanguageLevels.Queries.GetList;

public class GetListStudentLanguageLevelQuery : IRequest<GetListResponse<GetListStudentLanguageLevelListItemDto>> ,ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin };
    public class GetListStudentLanguageLevelQueryHandler : IRequestHandler<GetListStudentLanguageLevelQuery, GetListResponse<GetListStudentLanguageLevelListItemDto>>
    {
        private readonly IStudentLanguageLevelRepository _studentLanguageLevelRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListStudentLanguageLevelQueryHandler(IStudentLanguageLevelRepository studentLanguageLevelRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentLanguageLevelRepository = studentLanguageLevelRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentLanguageLevelListItemDto>> Handle(GetListStudentLanguageLevelQuery request, CancellationToken cancellationToken)
        {
          

            IPaginate<StudentLanguageLevel> studentLanguageLevels = await _studentLanguageLevelRepository.GetListAsync(
                include: sll => sll.Include(sll => sll.Student)
                    .ThenInclude(s=>s.User)
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