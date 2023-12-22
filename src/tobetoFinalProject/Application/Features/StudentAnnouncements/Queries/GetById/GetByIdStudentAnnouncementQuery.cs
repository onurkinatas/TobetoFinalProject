using Application.Features.StudentAnnouncements.Constants;
using Application.Features.StudentAnnouncements.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentAnnouncements.Constants.StudentAnnouncementsOperationClaims;

namespace Application.Features.StudentAnnouncements.Queries.GetById;

public class GetByIdStudentAnnouncementQuery : IRequest<GetByIdStudentAnnouncementResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetByIdStudentAnnouncementQueryHandler : IRequestHandler<GetByIdStudentAnnouncementQuery, GetByIdStudentAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentAnnouncementRepository _studentAnnouncementRepository;
        private readonly StudentAnnouncementBusinessRules _studentAnnouncementBusinessRules;

        public GetByIdStudentAnnouncementQueryHandler(IMapper mapper, IStudentAnnouncementRepository studentAnnouncementRepository, StudentAnnouncementBusinessRules studentAnnouncementBusinessRules)
        {
            _mapper = mapper;
            _studentAnnouncementRepository = studentAnnouncementRepository;
            _studentAnnouncementBusinessRules = studentAnnouncementBusinessRules;
        }

        public async Task<GetByIdStudentAnnouncementResponse> Handle(GetByIdStudentAnnouncementQuery request, CancellationToken cancellationToken)
        {
            StudentAnnouncement? studentAnnouncement = await _studentAnnouncementRepository.GetAsync(predicate: sa => sa.Id == request.Id, cancellationToken: cancellationToken);
            await _studentAnnouncementBusinessRules.StudentAnnouncementShouldExistWhenSelected(studentAnnouncement);

            GetByIdStudentAnnouncementResponse response = _mapper.Map<GetByIdStudentAnnouncementResponse>(studentAnnouncement);
            return response;
        }
    }
}