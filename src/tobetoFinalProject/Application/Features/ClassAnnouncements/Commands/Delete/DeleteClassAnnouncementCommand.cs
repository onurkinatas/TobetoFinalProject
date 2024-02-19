using Application.Features.ClassAnnouncements.Constants;
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

namespace Application.Features.ClassAnnouncements.Commands.Delete;

public class DeleteClassAnnouncementCommand : IRequest<DeletedClassAnnouncementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassAnnouncementsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllClassDetails";

    public class DeleteClassAnnouncementCommandHandler : IRequestHandler<DeleteClassAnnouncementCommand, DeletedClassAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassAnnouncementRepository _classAnnouncementRepository;
        private readonly ClassAnnouncementBusinessRules _classAnnouncementBusinessRules;

        public DeleteClassAnnouncementCommandHandler(IMapper mapper, IClassAnnouncementRepository classAnnouncementRepository,
                                         ClassAnnouncementBusinessRules classAnnouncementBusinessRules)
        {
            _mapper = mapper;
            _classAnnouncementRepository = classAnnouncementRepository;
            _classAnnouncementBusinessRules = classAnnouncementBusinessRules;
        }

        public async Task<DeletedClassAnnouncementResponse> Handle(DeleteClassAnnouncementCommand request, CancellationToken cancellationToken)
        {
            ClassAnnouncement? classAnnouncement = await _classAnnouncementRepository.GetAsync(predicate: ca => ca.Id == request.Id, cancellationToken: cancellationToken);
            await _classAnnouncementBusinessRules.ClassAnnouncementShouldExistWhenSelected(classAnnouncement);

            await _classAnnouncementRepository.DeleteAsync(classAnnouncement!);

            DeletedClassAnnouncementResponse response = _mapper.Map<DeletedClassAnnouncementResponse>(classAnnouncement);
            return response;
        }
    }
}