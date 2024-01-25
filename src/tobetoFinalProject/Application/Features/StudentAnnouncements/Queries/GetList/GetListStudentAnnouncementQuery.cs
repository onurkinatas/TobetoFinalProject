using Application.Features.StudentAnnouncements.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentAnnouncements.Constants.StudentAnnouncementsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;

namespace Application.Features.StudentAnnouncements.Queries.GetList;

public class GetListStudentAnnouncementQuery : IRequest<ICollection<StudentAnnouncement>>, ISecuredRequest
{

    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentAnnouncementQueryHandler : IRequestHandler<GetListStudentAnnouncementQuery, ICollection<StudentAnnouncement>>
    {
        private readonly IStudentAnnouncementRepository _studentAnnouncementRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListStudentAnnouncementQueryHandler(IStudentAnnouncementRepository studentAnnouncementRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentAnnouncementRepository = studentAnnouncementRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<ICollection<StudentAnnouncement>> Handle(GetListStudentAnnouncementQuery request, CancellationToken cancellationToken)
        {

            Student getStudent = await _contextOperationService.GetStudentFromContext();
          
            var studentAnnouncements = _studentAnnouncementRepository.GetAllWithoutPaginate(sa => sa.StudentId == getStudent.Id);

            
            return studentAnnouncements;
        }
    }
}