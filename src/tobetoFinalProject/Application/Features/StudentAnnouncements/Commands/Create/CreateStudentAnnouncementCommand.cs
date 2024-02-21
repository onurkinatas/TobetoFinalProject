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
using Application.Services.ContextOperations;

namespace Application.Features.StudentAnnouncements.Commands.Create;

public class CreateStudentAnnouncementCommand : IRequest<CreatedStudentAnnouncementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid AnnouncementId { get; set; }
    public Guid? StudentId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentAnnouncementsOperationClaims.Create,"Student" };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "xx";

    public class CreateStudentAnnouncementCommandHandler : IRequestHandler<CreateStudentAnnouncementCommand, CreatedStudentAnnouncementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentAnnouncementRepository _studentAnnouncementRepository;
        private readonly IContextOperationService _contextOperationService;
        private readonly StudentAnnouncementBusinessRules _studentAnnouncementBusinessRules;

        public CreateStudentAnnouncementCommandHandler(IMapper mapper, IStudentAnnouncementRepository studentAnnouncementRepository,
                                         StudentAnnouncementBusinessRules studentAnnouncementBusinessRules, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentAnnouncementRepository = studentAnnouncementRepository;
            _studentAnnouncementBusinessRules = studentAnnouncementBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<CreatedStudentAnnouncementResponse> Handle(CreateStudentAnnouncementCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            StudentAnnouncement studentAnnouncement = _mapper.Map<StudentAnnouncement>(request);
            studentAnnouncement.StudentId = getStudent.Id;

            bool doesExist = await _studentAnnouncementRepository.AnyAsync(predicate:sa=>sa.StudentId== studentAnnouncement.StudentId &&sa.AnnouncementId==studentAnnouncement.AnnouncementId);
            
            if(!doesExist)
                await _studentAnnouncementRepository.AddAsync(studentAnnouncement);


            CreatedStudentAnnouncementResponse response = _mapper.Map<CreatedStudentAnnouncementResponse>(studentAnnouncement);
            return response;
        }
    }
}