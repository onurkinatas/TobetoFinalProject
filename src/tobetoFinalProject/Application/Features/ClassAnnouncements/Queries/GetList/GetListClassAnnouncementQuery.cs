using Application.Features.ClassAnnouncements.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ClassAnnouncements.Constants.ClassAnnouncementsOperationClaims;
using Application.Services.CacheForMemory;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClassAnnouncements.Queries.GetList;

public class GetListClassAnnouncementQuery : IRequest<GetListResponse<GetListClassAnnouncementListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListClassAnnouncements({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetClassAnnouncements";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListClassAnnouncementQueryHandler : IRequestHandler<GetListClassAnnouncementQuery, GetListResponse<GetListClassAnnouncementListItemDto>>
    {
        private readonly IClassAnnouncementRepository _classAnnouncementRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetListClassAnnouncementQueryHandler(IClassAnnouncementRepository classAnnouncementRepository, IMapper mapper, ICacheMemoryService cacheMemoryService)
        {
            _classAnnouncementRepository = classAnnouncementRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetListResponse<GetListClassAnnouncementListItemDto>> Handle(GetListClassAnnouncementQuery request, CancellationToken cancellationToken)
        {
            ICollection<Guid> getCacheClassIds = _cacheMemoryService.GetStudentClassIdFromCache();

            IPaginate<ClassAnnouncement> classAnnouncements = await _classAnnouncementRepository.GetListAsync(
                predicate: ce => getCacheClassIds.Contains(ce.StudentClassId),
                include: ca => ca.Include(ca => ca.Announcement)
                    .Include(ca => ca.StudentClass),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListClassAnnouncementListItemDto> response = _mapper.Map<GetListResponse<GetListClassAnnouncementListItemDto>>(classAnnouncements);
            return response;
        }
    }
}