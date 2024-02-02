using Application.Features.StudentAnnouncements.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentAnnouncements.Constants.StudentAnnouncementsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;
using Application.Features.StudentAnnouncements.Queries.GetList;

namespace Application.Features.StudentAnnouncements.Queries.GetList;

public class GetListStudentAnnouncementQuery : IRequest<GetListResponse<GetListStudentAnnouncementListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => new[] { Admin};

    public bool BypassCache { get; }
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentAnnouncementQueryHandler : IRequestHandler<GetListStudentAnnouncementQuery, GetListResponse<GetListStudentAnnouncementListItemDto>>
    {
        private readonly IStudentAnnouncementRepository _studentAnnouncementRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListStudentAnnouncementQueryHandler(IStudentAnnouncementRepository studentAnnouncementRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentAnnouncementRepository = studentAnnouncementRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentAnnouncementListItemDto>> Handle(GetListStudentAnnouncementQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentAnnouncement> studentAnnouncements = await _studentAnnouncementRepository.GetListAsync(
                include: sa => sa.Include(sa => sa.Student)
                .ThenInclude(s => s.User)
                .Include(ll => ll.Announcement),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );
             
            GetListResponse<GetListStudentAnnouncementListItemDto> response = _mapper.Map<GetListResponse<GetListStudentAnnouncementListItemDto>>(studentAnnouncements);
            return response;
        }
    }
}