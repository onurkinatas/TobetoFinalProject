using Application.Features.ClassAnnouncements.Constants;
using Application.Features.ClassAnnouncements.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ClassAnnouncements.Constants.ClassAnnouncementsOperationClaims;

namespace Application.Features.ClassAnnouncements.Commands.Create;

public class CreateClassAnnouncementCommand : IRequest<CreatedClassAnnouncementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid AnnouncementId { get; set; }
    public Guid StudentClassId { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassAnnouncementsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetClassAnnouncements";

    public class CreateClassAnnouncementCommandHandler : IRequestHandler<CreateClassAnnouncementCommand, CreatedClassAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassAnnouncementRepository _classAnnouncementRepository;
        private readonly ClassAnnouncementBusinessRules _classAnnouncementBusinessRules;

        public CreateClassAnnouncementCommandHandler(IMapper mapper, IClassAnnouncementRepository classAnnouncementRepository,
                                         ClassAnnouncementBusinessRules classAnnouncementBusinessRules)
        {
            _mapper = mapper;
            _classAnnouncementRepository = classAnnouncementRepository;
            _classAnnouncementBusinessRules = classAnnouncementBusinessRules;
        }

        public async Task<CreatedClassAnnouncementResponse> Handle(CreateClassAnnouncementCommand request, CancellationToken cancellationToken)
        {
            ClassAnnouncement classAnnouncement = _mapper.Map<ClassAnnouncement>(request);
            await _classAnnouncementBusinessRules.ClassAnnouncementShouldNotExistsWhenInsert(classAnnouncement.StudentClassId, classAnnouncement.AnnouncementId);
            await _classAnnouncementRepository.AddAsync(classAnnouncement);

            CreatedClassAnnouncementResponse response = _mapper.Map<CreatedClassAnnouncementResponse>(classAnnouncement);
            return response;
        }
    }
}