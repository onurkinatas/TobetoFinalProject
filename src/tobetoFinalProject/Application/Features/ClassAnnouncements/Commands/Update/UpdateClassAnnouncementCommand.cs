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

namespace Application.Features.ClassAnnouncements.Commands.Update;

public class UpdateClassAnnouncementCommand : IRequest<UpdatedClassAnnouncementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentClassId { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassAnnouncementsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetClassAnnouncements";

    public class UpdateClassAnnouncementCommandHandler : IRequestHandler<UpdateClassAnnouncementCommand, UpdatedClassAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassAnnouncementRepository _classAnnouncementRepository;
        private readonly ClassAnnouncementBusinessRules _classAnnouncementBusinessRules;

        public UpdateClassAnnouncementCommandHandler(IMapper mapper, IClassAnnouncementRepository classAnnouncementRepository,
                                         ClassAnnouncementBusinessRules classAnnouncementBusinessRules)
        {
            _mapper = mapper;
            _classAnnouncementRepository = classAnnouncementRepository;
            _classAnnouncementBusinessRules = classAnnouncementBusinessRules;
        }

        public async Task<UpdatedClassAnnouncementResponse> Handle(UpdateClassAnnouncementCommand request, CancellationToken cancellationToken)
        {
            ClassAnnouncement? classAnnouncement = await _classAnnouncementRepository.GetAsync(predicate: ca => ca.Id == request.Id, cancellationToken: cancellationToken);
            await _classAnnouncementBusinessRules.ClassAnnouncementShouldExistWhenSelected(classAnnouncement);
            classAnnouncement = _mapper.Map(request, classAnnouncement);
            await _classAnnouncementBusinessRules.ClassAnnouncementShouldNotExistsWhenUpdate(classAnnouncement.StudentClassId,classAnnouncement.AnnouncementId);
            await _classAnnouncementRepository.UpdateAsync(classAnnouncement!);

            UpdatedClassAnnouncementResponse response = _mapper.Map<UpdatedClassAnnouncementResponse>(classAnnouncement);
            return response;
        }
    }
}