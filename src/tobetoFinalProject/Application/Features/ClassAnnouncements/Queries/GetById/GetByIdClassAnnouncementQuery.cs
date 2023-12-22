using Application.Features.ClassAnnouncements.Constants;
using Application.Features.ClassAnnouncements.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ClassAnnouncements.Constants.ClassAnnouncementsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.CacheForMemory;

namespace Application.Features.ClassAnnouncements.Queries.GetById;

public class GetByIdClassAnnouncementQuery : IRequest<GetByIdClassAnnouncementResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetByIdClassAnnouncementQueryHandler : IRequestHandler<GetByIdClassAnnouncementQuery, GetByIdClassAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassAnnouncementRepository _classAnnouncementRepository;
        private readonly ClassAnnouncementBusinessRules _classAnnouncementBusinessRules;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetByIdClassAnnouncementQueryHandler(IMapper mapper, IClassAnnouncementRepository classAnnouncementRepository, ClassAnnouncementBusinessRules classAnnouncementBusinessRules, ICacheMemoryService cacheMemoryService)
        {
            _mapper = mapper;
            _classAnnouncementRepository = classAnnouncementRepository;
            _classAnnouncementBusinessRules = classAnnouncementBusinessRules;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetByIdClassAnnouncementResponse> Handle(GetByIdClassAnnouncementQuery request, CancellationToken cancellationToken)
        {
            ICollection<Guid> getCacheClassIds = _cacheMemoryService.GetStudentClassIdFromCache();

            ClassAnnouncement? classAnnouncement = await _classAnnouncementRepository.GetAsync(
                predicate: ce => getCacheClassIds.Contains(ce.StudentClassId),
                include: ca => ca.Include(ca => ca.Announcement)
                    .Include(ca => ca.StudentClass),
                cancellationToken: cancellationToken);
            await _classAnnouncementBusinessRules.ClassAnnouncementShouldExistWhenSelected(classAnnouncement);

            GetByIdClassAnnouncementResponse response = _mapper.Map<GetByIdClassAnnouncementResponse>(classAnnouncement);
            return response;
        }
    }
}
