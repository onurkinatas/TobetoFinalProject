using Application.Features.StudentAnnouncements.Queries.GetList;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentAnnouncements.Queries.GetListForLoggedStudent;
public class GetListForLoggedStudentQuery : IRequest<ICollection<StudentAnnouncement>>, ISecuredRequest
{
    public string[] Roles => new[] { "Student" };

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
