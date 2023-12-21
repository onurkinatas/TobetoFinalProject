using Application.Features.StudentAnnouncements.Constants;
using Application.Features.StudentAnnouncements.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentAnnouncements.Constants.StudentAnnouncementsOperationClaims;

namespace Application.Features.StudentAnnouncements.Commands.Update;

public class UpdateStudentAnnouncementCommand : IRequest<UpdatedStudentAnnouncementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid AnnouncementId { get; set; }
    public Guid StudentId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentAnnouncementsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentAnnouncements";

    public class UpdateStudentAnnouncementCommandHandler : IRequestHandler<UpdateStudentAnnouncementCommand, UpdatedStudentAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentAnnouncementRepository _studentAnnouncementRepository;
        private readonly StudentAnnouncementBusinessRules _studentAnnouncementBusinessRules;

        public UpdateStudentAnnouncementCommandHandler(IMapper mapper, IStudentAnnouncementRepository studentAnnouncementRepository,
                                         StudentAnnouncementBusinessRules studentAnnouncementBusinessRules)
        {
            _mapper = mapper;
            _studentAnnouncementRepository = studentAnnouncementRepository;
            _studentAnnouncementBusinessRules = studentAnnouncementBusinessRules;
        }

        public async Task<UpdatedStudentAnnouncementResponse> Handle(UpdateStudentAnnouncementCommand request, CancellationToken cancellationToken)
        {
            StudentAnnouncement? studentAnnouncement = await _studentAnnouncementRepository.GetAsync(predicate: sa => sa.Id == request.Id, cancellationToken: cancellationToken);
            await _studentAnnouncementBusinessRules.StudentAnnouncementShouldExistWhenSelected(studentAnnouncement);
            studentAnnouncement = _mapper.Map(request, studentAnnouncement);

            await _studentAnnouncementRepository.UpdateAsync(studentAnnouncement!);

            UpdatedStudentAnnouncementResponse response = _mapper.Map<UpdatedStudentAnnouncementResponse>(studentAnnouncement);
            return response;
        }
    }
}