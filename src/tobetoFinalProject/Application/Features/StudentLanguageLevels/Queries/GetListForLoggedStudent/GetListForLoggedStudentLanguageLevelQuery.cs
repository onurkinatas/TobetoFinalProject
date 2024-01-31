using Application.Features.StudentLanguageLevels.Queries.GetList;
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

namespace Application.Features.StudentLanguageLevels.Queries.GetListForLoggedStudent;
public class GetListForLoggedStudentLanguageLevelQuery : IRequest<GetListResponse<GetListStudentLanguageLevelListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { "Student" };
    public class GetListForLoggedStudentLanguageLevelQueryHandler : IRequestHandler<GetListForLoggedStudentLanguageLevelQuery, GetListResponse<GetListStudentLanguageLevelListItemDto>>
    {
        private readonly IStudentLanguageLevelRepository _studentLanguageLevelRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListForLoggedStudentLanguageLevelQueryHandler(IStudentLanguageLevelRepository studentLanguageLevelRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentLanguageLevelRepository = studentLanguageLevelRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentLanguageLevelListItemDto>> Handle(GetListForLoggedStudentLanguageLevelQuery request, CancellationToken cancellationToken)
        {
            Student student = await _contextOperationService.GetStudentFromContext();

            IPaginate<StudentLanguageLevel> studentLanguageLevels = await _studentLanguageLevelRepository.GetListAsync(
                predicate: cc => cc.StudentId == student.Id,
                include: sll => sll.Include(sll => sll.Student)
                    .ThenInclude(s => s.User)
                    .Include(sll => sll.LanguageLevel)
                    .ThenInclude(ll => ll.Language),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentLanguageLevelListItemDto> response = _mapper.Map<GetListResponse<GetListStudentLanguageLevelListItemDto>>(studentLanguageLevels);
            return response;
        }
    }
}
