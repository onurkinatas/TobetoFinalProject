using Application.Features.StudentSocialMedias.Queries.GetList;
using Application.Features.StudentSocialMedias.Queries.GetListByStudentId;
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

namespace Application.Features.StudentSocialMedias.Queries.GetListByStudentId;
public class GetListByStudentIdStudentSocialMediaQuery : IRequest<GetListResponse<GetListStudentSocialMediaListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid StudentId { get; set; }
    public string[] Roles => new[] { "Admin" };

    public class GetListByStudentIdStudentSocialMediaQueryHandler : IRequestHandler<GetListByStudentIdStudentSocialMediaQuery, GetListResponse<GetListStudentSocialMediaListItemDto>>
    {
        private readonly IStudentSocialMediaRepository _studentSocialMediaRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListByStudentIdStudentSocialMediaQueryHandler(IStudentSocialMediaRepository studentSocialMediaRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentSocialMediaRepository = studentSocialMediaRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentSocialMediaListItemDto>> Handle(GetListByStudentIdStudentSocialMediaQuery request, CancellationToken cancellationToken)
        {

            IPaginate<StudentSocialMedia> studentSocialMedias = await _studentSocialMediaRepository.GetListAsync(
                predicate: se => se.StudentId == request.StudentId,
                include: sll => sll.Include(sll => sll.Student)
                    .ThenInclude(s => s.User)
                    .Include(sll => sll.SocialMedia),
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
