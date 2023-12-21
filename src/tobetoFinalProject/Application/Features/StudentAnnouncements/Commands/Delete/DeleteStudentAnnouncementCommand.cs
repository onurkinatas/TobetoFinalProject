using Application.Features.StudentAnnouncements.Constants;
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

namespace Application.Features.StudentAnnouncements.Commands.Delete;

public class DeleteStudentAnnouncementCommand : IRequest<DeletedStudentAnnouncementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentAnnouncementsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentAnnouncements";

    public class DeleteStudentAnnouncementCommandHandler : IRequestHandler<DeleteStudentAnnouncementCommand, DeletedStudentAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentAnnouncementRepository _studentAnnouncementRepository;
        private readonly StudentAnnouncementBusinessRules _studentAnnouncementBusinessRules;

        public DeleteStudentAnnouncementCommandHandler(IMapper mapper, IStudentAnnouncementRepository studentAnnouncementRepository,
                                         StudentAnnouncementBusinessRules studentAnnouncementBusinessRules)
        {
            _mapper = mapper;
            _studentAnnouncementRepository = studentAnnouncementRepository;
            _studentAnnouncementBusinessRules = studentAnnouncementBusinessRules;
        }

        public async Task<DeletedStudentAnnouncementResponse> Handle(DeleteStudentAnnouncementCommand request, CancellationToken cancellationToken)
        {
            StudentAnnouncement? studentAnnouncement = await _studentAnnouncementRepository.GetAsync(predicate: sa => sa.Id == request.Id, cancellationToken: cancellationToken);
            await _studentAnnouncementBusinessRules.StudentAnnouncementShouldExistWhenSelected(studentAnnouncement);

            await _studentAnnouncementRepository.DeleteAsync(studentAnnouncement!);

            DeletedStudentAnnouncementResponse response = _mapper.Map<DeletedStudentAnnouncementResponse>(studentAnnouncement);
            return response;
        }
    }
}