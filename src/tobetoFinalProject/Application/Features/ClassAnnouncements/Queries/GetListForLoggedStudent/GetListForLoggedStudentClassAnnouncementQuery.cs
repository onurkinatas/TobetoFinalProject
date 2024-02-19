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
using Application.Services.ContextOperations;
using Application.Services.StudentAnnouncements;

namespace Application.Features.ClassAnnouncements.Queries.GetListForLoggedStudent;

public class GetListForLoggedStudentClassAnnouncementQuery : IRequest<GetListResponse<GetListForLoggedStudentClassAnnouncementListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetListForLoggedStudentClassAnnouncementQueryHandler : IRequestHandler<GetListForLoggedStudentClassAnnouncementQuery, GetListResponse<GetListForLoggedStudentClassAnnouncementListItemDto>>
    {
        private readonly IClassAnnouncementRepository _classAnnouncementRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        private readonly IStudentAnnouncementsService _studentAnnouncementsService;
        public GetListForLoggedStudentClassAnnouncementQueryHandler(IClassAnnouncementRepository classAnnouncementRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService, IStudentAnnouncementsService studentAnnouncementsService)
        {
            _classAnnouncementRepository = classAnnouncementRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
            _studentAnnouncementsService = studentAnnouncementsService;
        }

        public async Task<GetListResponse<GetListForLoggedStudentClassAnnouncementListItemDto>> Handle(GetListForLoggedStudentClassAnnouncementQuery request, CancellationToken cancellationToken)
        {
            ICollection<Guid> getClassIds = await _contextOperationService.GetStudentClassesFromContext();
            Student getStudents = await _contextOperationService.GetStudentFromContext();


            IPaginate<ClassAnnouncement> classAnnouncements = await _classAnnouncementRepository.GetListAsync(
                predicate: ce => getClassIds.Contains(ce.StudentClassId),
                include: ca => ca.Include(ca => ca.Announcement)
                    .Include(ca => ca.StudentClass),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            List<StudentAnnouncement> studentAnnouncements = await _studentAnnouncementsService.GetAllAsync(sa=>sa.StudentId==getStudents.Id); 


            GetListResponse<GetListForLoggedStudentClassAnnouncementListItemDto> response = _mapper.Map<GetListResponse<GetListForLoggedStudentClassAnnouncementListItemDto>>(classAnnouncements);
            var updatedItems = response.Items.Select(item =>
            {
                var isRead = studentAnnouncements.Any(sa => sa.AnnouncementId == item.AnnouncementId);
                item.IsRead = isRead;
                return item;
            }).ToList();
            return response;
        }
    }
}