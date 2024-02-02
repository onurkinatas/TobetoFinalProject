using Application.Features.StudentSocialMedias.Queries.GetList;
using Application.Features.StudentSocialMedias.Queries.GetListForLoggedStudent;
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

namespace Application.Features.StudentSocialMedias.Queries.GetListForLoggedStudent;
public class GetListForLoggedStudentSocialMediaQuery : IRequest<GetListResponse<GetListStudentSocialMediaListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { "Student" };

    public class GetListForLoggedStudentSocialMediaQueryHandler : IRequestHandler<GetListForLoggedStudentSocialMediaQuery, GetListResponse<GetListStudentSocialMediaListItemDto>>
    {
        private readonly IStudentSocialMediaRepository _studentSocialMediaRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListForLoggedStudentSocialMediaQueryHandler(IStudentSocialMediaRepository studentSocialMediaRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentSocialMediaRepository = studentSocialMediaRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentSocialMediaListItemDto>> Handle(GetListForLoggedStudentSocialMediaQuery request, CancellationToken cancellationToken)
        {
            Student student = await _contextOperationService.GetStudentFromContext();

            IPaginate<StudentSocialMedia> studentSocialMedias = await _studentSocialMediaRepository.GetListAsync(
                predicate: ss => ss.StudentId == student.Id,
                include: ss => ss.Include(ss => ss.SocialMedia)
                    .Include(ss => ss.Student)
                    .ThenInclude(s => s.User),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentSocialMediaListItemDto> response = _mapper.Map<GetListResponse<GetListStudentSocialMediaListItemDto>>(studentSocialMedias);
            return response;
        }
    }
}
